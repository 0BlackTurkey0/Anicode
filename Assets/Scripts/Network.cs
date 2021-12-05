using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

class Network
{
	UdpClient receivingClient;
	UdpClient sendingClient;
	Thread receivingThread;
	private bool status = true;

	public string localIP { get; private set; }
	public Dictionary<string, string> dict { get; private set; } = new Dictionary<string, string>();
	public DateTime responseTime { get; private set; }
	public string systemMessage { get; private set; } = null;
	public string challengerIP { get; private set; } = null;
	public GameMode challengerMode { get; private set; } = null;
	public string challengerCode { get; private set; } = null;

	private Data sendData = new Data();
	private Data receiveData = new Data();
	private readonly string playerName;
	private const int port = 8888;
	private string responseIP;

	public Network(string name)    //建構子
	{
		localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.ToList().Where(p => p.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault().ToString();
		playerName = name;
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

		try {
			while (status) {
				byte[] bytes = receivingClient.Receive(ref endPoint);
				responseIP = endPoint.Address.ToString();
				if (responseIP == localIP) continue;    //過濾廣播後傳給自己的封包
				if (challengerIP != null && responseIP != challengerIP) continue;   //進入對戰後過濾非對手的封包
				receiveData = JsonSerializer.Deserialize<Data>(Encoding.UTF8.GetString(bytes));

				switch (receiveData.Type) {
					case MSG.REQUEST:
						sendData.Type = MSG.RESPONSE;
						sendData.Name = playerName;
						SendData(responseIP, sendData);
						break;

					case MSG.RESPONSE:
						if (dict.ContainsKey(responseIP))
							dict[responseIP] = receiveData.Name;
						else
							dict.Add(responseIP, receiveData.Name);
						break;

					case MSG.CHALLENGE:
						systemMessage = SYS.CHALLENGE;
						challengerIP = responseIP;
						break;

					case MSG.ACCEPT:
						systemMessage = SYS.ACCEPT;
						break;

					case MSG.DENY:
						systemMessage = SYS.DENY;
						break;

					case MSG.CONNECT:
						responseTime = receiveData.Time;
						break;

					case MSG.MODE:
						challengerMode = receiveData.Mode;
						break;

					case MSG.GAME:
						challengerCode = receiveData.Code;
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
			var json = JsonSerializer.Serialize(data);
			byte[] bytes = Encoding.UTF8.GetBytes(json);
			sendingClient.Send(bytes, bytes.Length, ipep);
		}
		catch (Exception ex) {
			throw ex;
		}
	}

	public void Quit()  //關閉網路功能
	{
		sendingClient.Close();
		receivingClient.Close();
		status = false;
	}

	public void SearchUser()  //搜尋線上使用者
	{
		try {
			dict.Clear();
			sendData.Type = MSG.REQUEST;
			sendData.Name = playerName;
			SendData("255.255.255.255", sendData);
		}
		catch (Exception ex) {
			throw ex;
		}
	}

	public void SendChallenge(string ip)    //對指定ip位置的使用者發起挑戰
	{
		try {
			sendData.Type = MSG.CHALLENGE;
			sendData.Name = playerName;
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
			sendData.Type = MSG.ACCEPT;
			sendData.Name = playerName;
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
			sendData.Type = MSG.DENY;
			sendData.Name = playerName;
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
			sendData.Type = MSG.MODE;
			sendData.Name = playerName;
			sendData.Mode = mode;
			SendData(challengerIP, sendData);
			systemMessage = SYS.GAME;
		}
		catch (Exception ex) {
			throw ex;
		}
	}

	public void SendConnection()    //確認連線狀態
	{
		try {
			sendData.Type = MSG.CONNECT;
			sendData.Name = playerName;
			sendData.Time = DateTime.Now;
			SendData(challengerIP, sendData);
		}
		catch (Exception ex) {
			throw ex;
		}
	}

	public void SendGameData(string code)   //傳送遊戲數據
	{
		try {
			sendData.Type = MSG.GAME;
			sendData.Name = playerName;
			sendData.Code = code;
		}
		catch (Exception ex) {
			throw ex;
		}
	}
}