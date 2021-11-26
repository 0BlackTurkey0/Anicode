using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Network : MonoBehaviour
{
	private NetworkThread thread;
	private Dictionary<string, string> userList = new Dictionary<string, string>();

	private const string SYS_CHALLENGE = "CHALLENGE";
	private const string SYS_ACCEPT = "ACCEPT";
	private const string SYS_DENY = "DENY";
	private const string SYS_START = "START";

	public Text send;
	public Text IP;
	public Text user;
	public Text recieve;
	private string userName = "Boris";
	private string localIP;
	int count = 0, loop = 0;

	// Start is called before the first frame update
	void Start()
	{
		IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
		localIP = hostEntry.AddressList.ToList().Where(p => p.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault().ToString();

		thread = new NetworkThread(localIP, userName);

		send.text = count.ToString();
		IP.text = "IP: " + localIP;
		user.text = null;
		recieve.text = null;
	}

	// Update is called once per frame
	void Update()
	{
		if (loop++ == 100)
		{
			count += 1;
			loop = 0;
			send.text = count.ToString();
			
			userList = thread.SearchUser();

			string temp = null;
			foreach (KeyValuePair<string, string> item in userList)
			{
				temp += item.Value;
				temp += "\n";
			}
			user.text = temp;

			CheckMessage();
		}
	}

	private void OnApplicationQuit()
	{
		
	}

	private void CheckMessage()
	{
		switch (thread.systemMessage)
		{
			case SYS_CHALLENGE:
				recieve.text = "Challenge from " + userList[thread.challengerIP];
				break;

			case SYS_START:
				//---
				//開始進行對戰
				//---
				break;

			case SYS_DENY:
				//---
				//對手拒絕對戰後的動作
				//---
				break;
		}
	}

	private void Challenge()	//發起挑戰
	{
		//---
		//這部分為使用者選擇的對手
		//並以ip位置為參數呼叫SendChallenge
		string ip = "192.168.2.101";
		//---
		thread.SendChallenge(ip);
	}

	private void Accept()	//接受挑戰
	{
		//---
		//這部分為當使用者點擊確認按鈕時
		//---
		thread.AcceptChallenge();
	}

	private void Deny() //拒絕挑戰
	{
		//---
		//這部分為當使用者點擊拒絕按鈕時
		//---
		thread.DenyChallenge();
	}
}
