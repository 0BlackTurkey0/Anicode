using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class control_in_twolobby : MonoBehaviour
{
    [SerializeField] GameObject MotionSetting;
    [SerializeField] GameObject JoinBtn;
    [SerializeField] GameObject SearchBtn;
    [SerializeField] GameObject PlayerBarPrefab;
    [SerializeField] GameObject PlayerListContent;
    [SerializeField] GameObject WaitingListUpdate;
    [SerializeField] GameObject RespondAcceptOrNot;
    [SerializeField] GameObject WaitingOpponentRespond;

    private Network network;
    private string playerName;  //TBD 抓取使用者名稱
    private Dictionary<string, string> playerList { get { return network.dict; } }
    public static GameMode mode = new GameMode();
    private int SeletedIndex = -1;
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

        JoinBtn.transform.GetComponent<Button>().enabled = true; //!!
        MotionSetting.SetActive(false);
        WaitingListUpdate.SetActive(false);
        RespondAcceptOrNot.SetActive(false);
        WaitingOpponentRespond.SetActive(false);
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

    private IEnumerator UpdateList()
    {
        int loop = 4;
        while (loop-- > 0) {
            for (int i = 0;i < PlayerListContent.transform.childCount;i += 1) {
                Destroy(PlayerListContent.transform.GetChild(i).gameObject);
            }

            int height = playerList.Count > 5 ? playerList.Count * 100 : 500;
            PlayerListContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, height);

            if (playerList.Count > 0) {
                int posY = -50;
                foreach (KeyValuePair<string, string> item in playerList) {
                    GameObject temp = Instantiate(PlayerBarPrefab, PlayerListContent.transform);
                    temp.transform.GetChild(0).gameObject.GetComponent<Text>().text = item.Key;
                    temp.transform.GetChild(2).gameObject.GetComponent<Text>().text = item.Value;
                    temp.transform.localPosition = new Vector2(600, posY);
                    temp.GetComponent<Button>().onClick.AddListener(delegate () { OnClick_Select(temp.transform.GetSiblingIndex()); });
                    posY -= 100;
                }
                GameObject temp1 = Instantiate(PlayerBarPrefab, PlayerListContent.transform);
                temp1.transform.GetChild(0).gameObject.GetComponent<Text>().text = "192.168.1.101";
                temp1.transform.GetChild(2).gameObject.GetComponent<Text>().text = "hahaha";
                temp1.transform.localPosition = new Vector2(600, posY);
                temp1.GetComponent<Button>().onClick.AddListener(delegate () { OnClick_Select(temp1.transform.GetSiblingIndex()); });
            }
            yield return new WaitForSeconds(1);
        }
        SearchBtn.GetComponent<Button>().enabled = true;
        WaitingListUpdate.SetActive(false);
        MotionSetting.SetActive(true);
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
        WaitingListUpdate.SetActive(true);
        MotionSetting.SetActive(false);

        network.SearchUser();
        StartCoroutine(UpdateList());
        SearchBtn.GetComponent<Button>().enabled = false;

    }
    public void OnClick_Challenge()   //發起挑戰
    {
        if (SeletedIndex != -1) {
            string ip = PlayerListContent.transform.GetChild(SeletedIndex).GetChild(0).gameObject.GetComponent<Text>().text;
            Debug.Log(ip);
            SeletedIndex = -1;
            //---
            network.SendChallenge(ip);
            //---
            //等待對手回應
            //---
            WaitingOpponentRespond.SetActive(true);
        }
    }
    public void ReceiveChallenge()  //接收來自別人的挑戰
    {
        RespondAcceptOrNot.SetActive(true);
    }
    public void OnClick_Accept()  //接受挑戰
    {
        //---
        //這部分為當使用者按下確認按鈕時
        //---
        RespondAcceptOrNot.SetActive(false);
        network.AcceptChallenge();
    }

    public void OnClick_Deny() //拒絕挑戰
    {
        //---
        //這部分為當使用者按下拒絕按鈕時
        //---
        RespondAcceptOrNot.SetActive(false);
        network.DenyChallenge();
    }

    public void OnClick_Confirm()
    {
        MotionSetting.SetActive(false);
    }

    private void OnClick_Select(int index)
    {
        SeletedIndex = index;
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

    public void Return()
    {
        //SceneManager.LoadScene(0);
    }
}
