// TODO::playerList邏輯修改(不重複生成)
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class control_in_twolobby : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] GameObject playerNameObject;     //TBD 抓取使用者名稱
    [Header("Object for Canvas")]
    [SerializeField] GameObject MotionSetting;
    [SerializeField] GameObject PlayerListContent;
    [SerializeField] GameObject WaitingListUpdate;
    [SerializeField] GameObject WaitingOpponentRespond;
    [SerializeField] GameObject RespondAcceptOrNot;
    [Header("Button")]
    [SerializeField] GameObject JoinBtn;
    [SerializeField] GameObject SearchBtn;
    [Header("Prefab")]
    [SerializeField] GameObject PlayerBarPrefab;

    private Network network;
    private string playerName;
    private Dictionary<string, string> playerList { get { return network.dict; } }
    private int rank {
        get { return PlayerPrefs.GetInt("Rank", 0); }
        set { PlayerPrefs.SetInt("Rank", value); }
    }
    private readonly string[] rankType = { "入門", "簡單", "普通", "困難" };
    public static GameMode mode = new GameMode();
    private int seletedIndex = -1;
    private bool isResponseChanllenge = false;
    private DateTime LocalTime { get { return DateTime.Now; } }
    private DateTime time;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        network = new Network(playerName);

        JoinBtn.transform.GetComponent<Button>().enabled = true;    //!!!記得設回false
        MotionSetting.SetActive(false);
        WaitingListUpdate.SetActive(false);
        WaitingOpponentRespond.transform.GetChild(0).gameObject.SetActive(false);
        RespondAcceptOrNot.transform.GetChild(0).gameObject.SetActive(false);
        
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
                StartCoroutine(ReceiveChallenge());
                network.ClearSystemMessage();
                break;

            case SYS.ACCEPT:

                isResponseChanllenge = true;
                Debug.Log("###");
                //SceneManager.LoadScene();
                break;

            case SYS.DENY:
                WaitingOpponentRespond.transform.GetChild(0).gameObject.SetActive(false);
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
                    temp.transform.GetChild(1).gameObject.GetComponent<Text>().text = rankType[rank];
                    temp.transform.GetChild(2).gameObject.GetComponent<Text>().text = item.Value;
                    temp.transform.localPosition = new Vector2(600, posY);
                    temp.GetComponent<Button>().onClick.AddListener(delegate () { OnClick_Select(temp.transform.GetSiblingIndex()); });
                    posY -= 100;
                }
                GameObject temp1 = Instantiate(PlayerBarPrefab, PlayerListContent.transform);
                temp1.transform.GetChild(0).gameObject.GetComponent<Text>().text = "192.168.1.101";
                temp1.transform.GetChild(1).gameObject.GetComponent<Text>().text = rankType[rank];
                temp1.transform.GetChild(2).gameObject.GetComponent<Text>().text = "hahaha";
                temp1.transform.localPosition = new Vector2(600, posY);
                temp1.GetComponent<Button>().onClick.AddListener(delegate () { OnClick_Select(temp1.transform.GetSiblingIndex()); });
            }
            yield return new WaitForSeconds(1);
        }
        SearchBtn.GetComponent<Button>().enabled = true;
        WaitingListUpdate.SetActive(false);
    }

    private IEnumerator ReceiveChallenge()  //接收來自別人的挑戰
    {
        RespondAcceptOrNot.transform.GetChild(0).gameObject.SetActive(true);
        RespondAcceptOrNot.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = playerList[network.challengerIP] + ":";
        yield return new WaitForSeconds(10);
        if (!isResponseChanllenge)
            network.DenyChallenge();
        RespondAcceptOrNot.transform.GetChild(0).gameObject.SetActive(false);
    }
    //---------------------------------------------------------------------------------------------------------------

    public void ShowMotionSetting()
    {
        MotionSetting.SetActive(true);
    }
    public void OnClickCancelInMotionSetting()
    {
        MotionSetting.SetActive(false);
    }
    public void OnClickConfirmInMotionSetting()
    {
        MotionSetting.SetActive(false);
        JoinBtn.transform.GetComponent<Button>().enabled = true;
    }
    public void OnClick_Search()
    {
        WaitingListUpdate.SetActive(true);

        network.SearchUser();
        StartCoroutine(UpdateList());
        SearchBtn.GetComponent<Button>().enabled = false;

    }
    public void OnClick_Challenge()   //發起挑戰
    {
        if (seletedIndex != -1) {
            string ip = PlayerListContent.transform.GetChild(seletedIndex).GetChild(0).gameObject.GetComponent<Text>().text;
            Debug.Log(ip);
            seletedIndex = -1;

            network.SendChallenge(ip);
            WaitingOpponentRespond.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void OnClick_Accept()  //接受挑戰
    {
        network.AcceptChallenge();
        RespondAcceptOrNot.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnClick_Deny() //拒絕挑戰
    {
        network.DenyChallenge();
        RespondAcceptOrNot.transform.GetChild(0).gameObject.SetActive(false);
    }
    private void OnClick_Select(int index)
    {
        seletedIndex = index;
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

    public void ReturnToLobby()
    {
        SceneManager.LoadScene(0);
    }
}
