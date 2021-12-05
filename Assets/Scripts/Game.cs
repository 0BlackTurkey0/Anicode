using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	private Network network;
	private string playerName = "Boris";
	private Dictionary<string, string> playerList { get { return network.dict; } }
	private DateTime LocalTime { get { return DateTime.Now; } }
	private DateTime time;

	public Text send;
	public Text IP;
	public Text user;
	public Text recieve;

	// Start is called before the first frame update
	void Start()
	{
		network = new Network(playerName);
		time = LocalTime;

		send.text = "";
		IP.text = "IP: " + network.localIP;
		user.text = null;
	}

	// Update is called once per frame
	void Update()
	{
		recieve.text = LocalTime.ToString("T", CultureInfo.CreateSpecificCulture("es-ES"));

		if (DateTime.Compare(LocalTime, time.AddSeconds(1)) == 1) {
			time = LocalTime;
			send.text = time.ToString();

			UpdateNetwork();
		}
	}

	private void OnApplicationQuit()
	{
		network.Quit();
	}

	private void UpdateNetwork()    //藉由systemMessage值來確認狀態
	{
		switch (network.systemMessage) {
			case SYS.CHALLENGE:
				//---
				//"Challenge from " + playerList[network.challengerIP];
				//玩家選擇接受或拒絕
				//---
				break;

			case SYS.ACCEPT:
				//---
				//進入對戰畫面
				//---
				//開始進行對戰前設定
				//---
				break;

			case SYS.DENY:
				//---
				//對手拒絕對戰後的動作
				//---
				break;

			case SYS.GAME:
				Connection();
				//---
				//對戰進行中的動作
				//---
				break;
		}
		//---
		//遍例playerList的方法
		/*if (playerList.Count > 0)
			foreach (KeyValuePair<string, string> item in playerList)
				user.text = item.Value;
		else
			user.text = null;*/
		//---
	}

	private void Search()   //搜尋使用者
	{
		//---
		//當玩家進入雙人大廳或按下搜尋按鈕
		//---
		network.SearchUser();
	}

	private void Challenge()   //發起挑戰
	{
		//---
		//這部分為使用者選擇的對手
		//並以ip位置為參數呼叫SendChallenge
		string ip = "192.168.2.101";
		//---
		network.SendChallenge(ip);
		//---
		//等待對手回應
		//---
	}

	private void Accept()  //接受挑戰
	{
		//---
		//這部分為當使用者按下確認按鈕時
		//---
		network.AcceptChallenge();
	}

	private void Deny() //拒絕挑戰
	{
		//---
		//這部分為當使用者按下拒絕按鈕時
		//---
		network.DenyChallenge();
	}

	private void SetMode()  //戰鬥前設定
	{
		GameMode mode = new GameMode();
		//---
		//使用者的戰鬥前設定
		//---
		network.SendModeSetting(mode);
	}

	private void Connection()
	{
		network.SendConnection();
		if (LocalTime.Second % 5 == 0) {    //每五秒鐘確認對手是否斷線

			DateTime tempTime = network.responseTime;
			if (DateTime.Compare(LocalTime, tempTime.AddSeconds(5)) == 1) {
				//---
				//"Connection OFF"
				//對手斷線後的動作
				//---
			}
		}
	}
}