using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

public class Network {
    public string localIP { get; private set; }
    public Dictionary<string, (string Name, int Status)> dict { get; private set; } = new Dictionary<string, (string, int)>();
    public DateTime responseTime { get; private set; }
    public string systemMessage { get; private set; } = null;
    public int playerStatus { get; private set; } = 0;
    public bool isGuest { get; private set; }
    public int finalDifficulty { get; set; } = -1;
    public string challengerIP { get; private set; } = null;
    public bool isModeReceive { get; set; } = false;
    public GameMode challengerMode { get; private set; } = null;
    public bool isCodeReceive { get; set; } = false;
    public Code challengerCode { get; private set; } = null;
    public bool isFoodReceive { get; set; } = false;
    public int[] challengerFood { get; private set; } = new int[10];

    private UdpClient receivingClient;
    private UdpClient sendingClient;
    private Thread receivingThread;
    private bool isNetworkRunning = true;
    private readonly string playerName;
    private readonly int playerRank;
    private const int port = 8880;

    public Network(string name, int rank)    //建構子
    {
        localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.ToList().Where(p => p.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault().ToString();
        playerName = name;
        playerRank = rank;
        InitSender();
        InitReceiver();
    }

    private void InitSender()   //初始化傳送用的UDP
    {
        sendingClient = new UdpClient {
            EnableBroadcast = true
        };
    }

    private void InitReceiver() //初始化接收用的UDP和thread
    {
        receivingClient = new UdpClient(port);
        receivingThread = new Thread(Receiver) {
            IsBackground = true
        };
        receivingThread.Start();
    }

    private void Receiver() //接收資料
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
        string responseIP;

        try {
            while (isNetworkRunning) {
                byte[] bytes = receivingClient.Receive(ref endPoint);
                responseIP = endPoint.Address.ToString();
                if (responseIP == localIP) continue;    //過濾廣播後傳給自己的封包
                //if (challengerIP != null && responseIP != challengerIP) continue;   //進入對戰後過濾非對手的封包
                Data receiveData = JsonSerializer.Deserialize<Data>(Encoding.UTF8.GetString(bytes));

                switch (receiveData.Type) {
                    case MSG.REQUEST:
                        SendResponse(responseIP);
                        break;

                    case MSG.RESPONSE:
                        if (dict.ContainsKey(responseIP)) {
                            //throw new NullReferenceException();
                            var (Name, Status) = dict[responseIP];
                            Name = receiveData.Name;
                            Status = receiveData.Status;
                            
                            dict[responseIP] = (Name, Status);
                        }
                        else {
                            dict.Add(responseIP, (receiveData.Name, receiveData.Status));
                        }
                        break;

                    case MSG.STATUS:
                        SendResponse(responseIP);
                        break;

                    case MSG.CHALLENGE:
                        systemMessage = SYS.CHALLENGE;
                        playerStatus = 1;
                        challengerIP = responseIP;
                        break;

                    case MSG.ACCEPT:
                        systemMessage = SYS.ACCEPT;
                        break;

                    case MSG.DENY:
                        systemMessage = SYS.DENY;
                        playerStatus = 0;
                        break;

                    case MSG.CONNECT:
                        responseTime = receiveData.Time;
                        break;

                    case MSG.MODE:
                        isModeReceive = true;
                        challengerMode = receiveData.Mode;
                        break;

                    case MSG.DIFFICULTY:
                        finalDifficulty = receiveData.FinalDifficulty;
                        if (finalDifficulty == -1)
                            systemMessage = null;
                        else
                            systemMessage = SYS.READY;
                        break;

                    case MSG.GAME:
                        isCodeReceive = true;
                        challengerCode = receiveData.Code;
                        systemMessage ??= SYS.GAME;
                        playerStatus = 2;
                        break;

                    case MSG.FOOD:
                        isFoodReceive = true;
                        challengerFood = receiveData.Food;
                        systemMessage ??= SYS.GAME;
                        playerStatus = 2;
                        break;
                }
            }
        }
        catch (Exception ex) {
            throw ex;
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

    public void Quit()  //關閉網路功能
    {
        try {
            if (sendingClient != null)
                sendingClient.Close();
            if (receivingClient != null)
                receivingClient.Close();
            isNetworkRunning = false;
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void SearchUser()  //搜尋線上使用者
    {
        try {
            dict.Clear();
            //dict.Add("192.168.1.101", ("hahaha", 0));
            Data sendData = new Data {
                Type = MSG.REQUEST,
                Name = playerName
            };
            SendData("255.255.255.255", sendData);
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void IntoGame()
    {
        systemMessage = SYS.GAME;
        playerStatus = 2;
    }

    public void ClearSystemMessage()
    {
        systemMessage = null;
        playerStatus = 0;
    }

    private void SendResponse(string ip)
    {
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

    public void SendStatus(string ip)
    {
        try {
            Data sendData = new Data {
                Type = MSG.STATUS,
                Name = playerName
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
                Name = playerName
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
                Name = playerName
            };
            SendData(challengerIP, sendData);
            systemMessage = SYS.ACCEPT;
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void DenyChallenge() //接受挑戰
    {
        try {
            Data sendData = new Data {
                Type = MSG.DENY,
                Name = playerName
            };
            SendData(challengerIP, sendData);
            systemMessage = null;
            challengerIP = null;
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void SendModeSetting(GameMode mode)  //傳送戰鬥前設定
    {
        try {
            Data sendData = new Data {
                Type = MSG.MODE,
                Name = playerName,
                Mode = mode
            };
            SendData(challengerIP, sendData);
            systemMessage = SYS.READY;
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void SendFinalDifficulty()
    {
        try {
            Data sendData = new Data {
                Type = MSG.DIFFICULTY,
                Name = playerName,
                FinalDifficulty = finalDifficulty
            };
            SendData(challengerIP, sendData);
            if (finalDifficulty == -1)
                systemMessage = null;
            else
                systemMessage = SYS.READY;
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
                Name = playerName,
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
                Name = playerName,
                Code = code,
            };
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void SendGameFood(int[] food)
    {
        try {
            Data sendData = new Data {
                Type = MSG.FOOD,
                Name = playerName,
                Food = food
            };
        }
        catch (Exception ex) {
            throw ex;
        }
    }
}