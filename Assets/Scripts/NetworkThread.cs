using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

class NetworkThread
{
	UdpClient receivingClient;
	UdpClient sendingClient;
	Thread receivingThread;

	private Data sendData = new Data();
	private Data receiveData = new Data();
	Dictionary<string, string> dict = new Dictionary<string, string>();

	private const string MSG_REQUEST = "REQ";
	private const string MSG_RESPONSE = "RES";
	private const string MSG_CHALLENGE = "CHA";
	private const string MSG_ACCEPT = "ACC";
	private const string MSG_DENY = "DEN";
	private const string MSG_CONNECT = "CON";
	private const string MSG_MODE = "MOD";
	private const string MSG_GAME = "GAM";
	private const string SYS_CHALLENGE = "CHALLENGE";
	private const string SYS_ACCEPT = "ACCEPT";
	private const string SYS_DENY = "DENY";
	private const string SYS_START = "START";

	private readonly string userName;
	private readonly string localIP;
	private const int port = 8888;
	public string responseIP;
	public string systemMessage = null;
	public string challengerIP = null;
	
	public NetworkThread(string ip, string name)	//建構子
	{
		localIP = ip;
		userName = name;
		InitSender();
		InitReceiver();
	}

	private void InitSender()	//初始化傳送用的UDP
	{
		sendingClient = new UdpClient
		{
			EnableBroadcast = true
		};
	}

	private void InitReceiver()	//初始化接收用的UDP和thread
	{
		receivingClient = new UdpClient(port);

		receivingThread = new Thread(Receiver)
		{
			IsBackground = true
		};
		receivingThread.Start();
	}

	private void Receiver()	//接收資料
	{
		IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);

		try
		{
			while (true)
			{
				byte[] bytes = receivingClient.Receive(ref endPoint);
				responseIP = endPoint.Address.ToString();
				if (responseIP == localIP) continue;
				receiveData = JsonSerializer.Deserialize<Data>(Encoding.UTF8.GetString(bytes));

				switch(receiveData.type)
				{
					case MSG_REQUEST:
						sendData.type = MSG_RESPONSE;
						sendData.name = userName;
						SendData(responseIP, sendData);
						break;
					
					case MSG_RESPONSE:
						if (dict.ContainsKey(responseIP))
							dict[responseIP] = receiveData.name;
						else
							dict.Add(responseIP, receiveData.name);
						break;
					
					case MSG_CHALLENGE:
						systemMessage = SYS_CHALLENGE;
						challengerIP = responseIP;
						break;
					
					case MSG_ACCEPT:
						systemMessage = SYS_START;
						break;

					case MSG_DENY:
						systemMessage = SYS_DENY;
						break;

					case MSG_MODE:
						break;
					
					case MSG_GAME:
						break;
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}
	
	public void SendData(string ip, Data data)	//傳送資料給指定的ip位置
	{
		try
		{
			IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);

			var json = JsonSerializer.Serialize(data);
			byte[] bytes = Encoding.UTF8.GetBytes(json);
			sendingClient.Send(bytes, bytes.Length, ipep);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public Dictionary<string, string> SearchUser()	//搜尋線上使用者
	{
		try
		{
			sendData.type = MSG_REQUEST;
			sendData.name = userName;
			SendData("255.255.255.255", sendData);

			int loop = 10000;
			while (loop-- > 0)
			{

			}
			return dict;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public void SendChallenge(string ip)	//對指定ip位置的使用者發起挑戰
	{
		try
		{
			sendData.type = MSG_CHALLENGE;
			sendData.name = userName;
			challengerIP = ip;
			SendData(ip, sendData);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public void AcceptChallenge()
	{
		try
		{
			sendData.type = MSG_ACCEPT;
			sendData.name = userName;
			SendData(challengerIP, sendData);
			systemMessage = SYS_START;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public void DenyChallenge()
	{
		try
		{
			sendData.type = MSG_DENY;
			sendData.name = userName;
			SendData(challengerIP, sendData);
			systemMessage = null;
			challengerIP = null;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}
}
