using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Network : MonoBehaviour {
    public Dictionary<string, (string Name, int Rank, int Status)> dict { get; private set; } = new Dictionary<string, (string, int, int)>();   //線上玩家資訊
    public DateTime responseTime { get; private set; }  //對手上次回應的時間
    public string systemMessage { get; private set; } = null;   //系統訊息
    public int playerStatus { get; private set; } = 0;  //玩家的遊玩狀態
    public GameMode playerMode { get; private set; } = null;    //玩家的模式設定
    public bool isGuest { get; private set; }   //玩家是否為挑戰者
    public bool isConnect { get; set; } = true; //對手是否連線中
    public int finalDifficulty { get; set; } = -1;  //最後決定的難度
    public string challengerIP { get; private set; } = null;    //對手的IP
    public bool isModeReceive { get; set; } = false;    //是否接收到對手的模式設定
    public GameMode challengerMode { get; private set; } = null;    //對手的模式設定
    public bool isCodeReceive { get; set; } = false;    //是否接收到對手的程式碼
    public Code challengerCode { get; set; } = null;    //對手的程式碼
    public bool isFoodReceive { get; set; } = false;    //是否接收到對手的食物
    public int[] challengerFood { get; private set; } = new int[10];    //對手的食物

    private ApplicationHandler applicationHandler;
    private UdpClient receivingClient = null;
    private UdpClient sendingClient = null;
    private Thread receivingThread = null;
    private bool isNetworkOn, isNetworkRunning;
    private string localIP;
    private string playerName;
    private int playerRank;
    private const int port = 8888;

    void Awake() {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.ToList().Where(p => p.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault().ToString();
        playerName = applicationHandler.GameData.Name;
        playerRank = (int)applicationHandler.GameData.Rank;
        if (sendingClient == null) {
            sendingClient = new UdpClient {
                EnableBroadcast = true
            };
        }
        if (receivingClient == null)
            receivingClient = new UdpClient(port);
        isNetworkOn = true;
        isNetworkRunning = false;
        if (receivingThread == null) {
            receivingThread = new Thread(Receiver) {
                IsBackground = true
            };
            receivingThread.Start();
        }
        StartCoroutine(UpdateNetwork());
    }

    void OnApplicationQuit() {
        try {
            isNetworkOn = false;
            if (sendingClient != null) {
                sendingClient.Close();
                sendingClient = null;
            }
            if (receivingClient != null) {
                receivingClient.Close();
                receivingClient = null;
            }
            if (receivingThread != null)
                receivingThread = null;
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    private void Receiver() //接收資料
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
        string responseIP;

        try {
            while (isNetworkOn) {
                if (isNetworkRunning) {
                    byte[] bytes = receivingClient.Receive(ref endPoint);
                    responseIP = endPoint.Address.ToString();
                    if (responseIP == localIP) continue;    //過濾廣播後傳給自己的封包
                    Debug.Log(responseIP + " : " + Encoding.UTF8.GetString(bytes));
                    Data receiveData = JsonSerializer.Deserialize<Data>(Encoding.UTF8.GetString(bytes));
                    switch (receiveData.Type) {
                        case MSG.REQUEST:
                            SendResponse(responseIP);
                            break;

                        case MSG.RESPONSE:
                            if (dict.ContainsKey(responseIP)) {
                                var (Name, Rank, Status) = dict[responseIP];
                                Name = receiveData.Name;
                                Rank = receiveData.Rank;
                                Status = receiveData.Status;
                                dict[responseIP] = (Name, Rank, Status);
                            }
                            else {
                                dict.Add(responseIP, (receiveData.Name, receiveData.Rank, receiveData.Status));
                            }
                            break;

                        case MSG.STATUS:
                            SendResponse(responseIP);
                            break;

                        case MSG.CHALLENGE:
                            systemMessage ??= SYS.CHALLENGE;
                            playerStatus = 1;
                            challengerIP = responseIP;
                            challengerMode = receiveData.Mode;
                            break;

                        case MSG.ACCEPT:
                            if (challengerIP != null) {
                                systemMessage ??= SYS.ACCEPT;
                                isModeReceive = true;
                                playerStatus = 2;
                                challengerMode = receiveData.Mode;
                            }
                            break;

                        case MSG.DENY:
                            systemMessage = SYS.DENY;
                            playerStatus = 0;
                            break;

                        case MSG.CONNECT:
                            responseTime = receiveData.Time;
                            break;

                        case MSG.DIFFICULTY:
                            finalDifficulty = receiveData.FinalDifficulty;
                            if (finalDifficulty == -1) {
                                systemMessage = SYS.MODE;
                                challengerIP = null;
                                challengerMode = null;
                                playerStatus = 0;
                            }
                            else {
                                systemMessage = SYS.READY;
                                playerStatus = 1;
                            }
                            break;

                        case MSG.GAME:
                            isCodeReceive = true;
                            challengerCode = new Code(receiveData.Code);
                            systemMessage = SYS.GAME;
                            playerStatus = 2;
                            break;

                        case MSG.FOOD:
                            isFoodReceive = true;
                            Array.Copy(receiveData.Food, challengerFood, 10);
                            systemMessage = SYS.GAME;
                            playerStatus = 2;
                            break;
                    }
                }
            }
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    private IEnumerator UpdateNetwork() {
        while (true) {
            if (SceneManager.GetActiveScene().name == "DualMode" || SceneManager.GetActiveScene().name == "Battle") {
                playerName = applicationHandler.GameData.Name;
                playerRank = (int)applicationHandler.GameData.Rank;
                isNetworkRunning = true;
                yield return new WaitForSeconds(1);
            }
            else {
                isNetworkRunning = false;
                yield return null;
            }
        }
    }

    private void SendData(string ip, Data data)  //傳送資料給指定的ip位置
    {
        try {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);
            byte[] bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));
            sendingClient.Send(bytes, bytes.Length, ipep);
        }
        catch (Exception ex) {
            throw ex;
        }
    }
    public void SearchUser()  //搜尋線上使用者
    {
        try {
            dict.Clear();
            Data sendData = new Data {
                Type = MSG.REQUEST
            };
            SendData("255.255.255.255", sendData);
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void IntoGame() {
        systemMessage = SYS.GAME;
        playerStatus = 2;
    }

    public void FinishGame() {
        systemMessage = null;
        playerStatus = 0;
    }

    public void ClearSystemMessage() {
        systemMessage = null;
        playerStatus = 0;
    }

    private void SendResponse(string ip) {
        try {
            Data sendData = new Data {
                Type = MSG.RESPONSE,
                Name = playerName,
                Rank = playerRank,
                Status = playerStatus
            };
            SendData(ip, sendData);
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void SendStatus(string ip) {
        try {
            Data sendData = new Data {
                Type = MSG.STATUS,
                Status = playerStatus
            };
            SendData(ip, sendData);
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void SendChallenge(string ip)    //對指定ip位置的使用者發起挑戰
    {
        try {
            isGuest = true;
            playerStatus = 1;
            Data sendData = new Data {
                Type = MSG.CHALLENGE,
                Mode = playerMode
            };
            challengerIP = ip;
            SendData(ip, sendData);
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void AcceptChallenge()   //接受挑戰
    {
        try {
            isGuest = false;
            Data sendData = new Data {
                Type = MSG.ACCEPT,
                Mode = playerMode
            };
            SendData(challengerIP, sendData);
            systemMessage = SYS.ACCEPT;
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void DenyChallenge() //拒絕挑戰
    {
        try {
            Data sendData = new Data {
                Type = MSG.DENY
            };
            SendData(challengerIP, sendData);
            systemMessage = SYS.DENY;
            challengerIP = null;
            challengerMode = null;
            playerStatus = 0;
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void SendFinalDifficulty() {
        try {
            Data sendData = new Data {
                Type = MSG.DIFFICULTY,
                FinalDifficulty = finalDifficulty
            };
            SendData(challengerIP, sendData);
            if (finalDifficulty == -1) {
                systemMessage = null;
                challengerIP = null;
                challengerMode = null;
                playerStatus = 0;
            }
            else {
                systemMessage = SYS.READY;
                playerStatus = 1;
            }
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void SendConnection()    //確認連線狀態
    {
        try {
            Data sendData = new Data {
                Type = MSG.CONNECT,
                Time = DateTime.Now
            };
            SendData(challengerIP, sendData);
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void SendGameData(Code code)   //傳送遊戲數據
    {
        try {
            Data sendData = new Data {
                Type = MSG.GAME,
                Code = code,
            };
            SendData(challengerIP, sendData);
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void SendGameFood(int[] food) {
        try {
            Data sendData = new Data {
                Type = MSG.FOOD,
                Food = food
            };
            SendData(challengerIP, sendData);
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void OverTimeDeny() {
        systemMessage = SYS.DENY;
        playerStatus = 0;
        challengerIP = null;
        challengerMode = null;
    }

    public void SetMode(GameMode mode) {
        playerMode = mode;
    }
}