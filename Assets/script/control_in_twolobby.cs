using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class control_in_twolobby : MonoBehaviour
{
    [SerializeField] GameObject MotionSetting;
    [SerializeField] GameObject JoinBtn;
    [SerializeField] GameObject ResetPlayerOnLIst;

    private Network network;
    private string playerName;  //TBD 抓取使用者名稱
    private Dictionary<string, string> playerList { get { return network.dict; } }
    private DateTime LocalTime { get { return DateTime.Now; } }
    private DateTime time;


    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        network = new Network(playerName);

        JoinBtn.transform.GetComponent<Button>().enabled = false;
        MotionSetting.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(UpdateNetwork());
    }

    private void OnApplicationQuit()
    {
        network.Quit();
    }

    private IEnumerator UpdateNetwork()    //藉由systemMessage值來確認狀態
    {
        switch (network.systemMessage)
        {
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
                break;

            case SYS.DENY:
                //---
                //對手拒絕對戰後的動作
                //---
                break;

            case SYS.GAME:
                //Connection();
                //---
                //對戰進行中的動作
                //---
                break;
        }
        yield return new WaitForSeconds(1);
        //---
        //遍例playerList的方法
        /*if (playerList.Count > 0)
			foreach (KeyValuePair<string, string> item in playerList)
				user.text = item.Value;
		else
			user.text = null;*/
        //---
    }
    //---------------------------------------------------------------------------------------------------------------

    public void ShowMotionSetting()
    {
        MotionSetting.SetActive(true);
    }
    
    public void unShowMotionSetting()
    {
        MotionSetting.SetActive(false);
        JoinBtn.transform.GetComponent<Button>().enabled = true;
    }
    public void OnClick_Search()
    {
        //---
        //當玩家進入雙人大廳或按下搜尋按鈕
        //---
        network.SearchUser();
        //---
        //在清單中顯示所有玩家
        //---
    }
    public void OnClick_Challenge()   //發起挑戰
    {
        //---
        //這部分為使用者選擇的對手
        //並以ip位置為參數呼叫SendChallenge
        string ip = "192.168.2.100";
        //---
        network.SendChallenge(ip);
        //---
        //等待對手回應
        //---
    }
    public void OnClick_Accept()  //接受挑戰
    {
        //---
        //這部分為當使用者按下確認按鈕時
        //---
        network.AcceptChallenge();
    }

    public void OnClick_Deny() //拒絕挑戰
    {
        //---
        //這部分為當使用者按下拒絕按鈕時
        //---
        network.DenyChallenge();
    }

    public void OnClick_Confirm()
    {
        MotionSetting.SetActive(false);
    }

    /*private void Connection()
    {
        network.SendConnection();
        if (LocalTime.Second % 5 == 0)
        {    //每五秒鐘確認對手是否斷線

            DateTime tempTime = network.responseTime;
            if (DateTime.Compare(LocalTime, tempTime.AddSeconds(5)) == 1)
            {
                //---
                //"Connection OFF"
                //對手斷線後的動作
                //---
            }
        }
    }*/

    public void EliminateRoomNameOnList()
    {
        
    }
    public void Return()
    {
        //SceneManager.LoadScene(0);
    }
}
