// TODO::playerList�޿�ק�(�����ƥͦ�)
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Control_in_Twolobby : MonoBehaviour {
    [Header("Information")]
    [SerializeField] GameObject playerNameObject;     //TBD ����ϥΪ̦W��
    [Header("Panel")]
    [SerializeField] GameObject ModeSetting;
    [SerializeField] GameObject PlayerListContent;
    [SerializeField] GameObject WaitingListUpdate;
    [SerializeField] GameObject HintWhenBusy;
    [SerializeField] GameObject WaitingOpponentRespond;
    [SerializeField] GameObject RespondAcceptOrNot;
    [Header("Button")]
    [SerializeField] GameObject JoinButton;
    [SerializeField] GameObject SearchButton;
    [Header("Prefab")]
    [SerializeField] GameObject PlayerBarPrefab;

    private Network network;
    private string playerName;
    private Dictionary<string, (string, int)> playerList { get { return network.dict; } }
    private int playerRank {
        get { return PlayerPrefs.GetInt("Rank", 0); }
        set { PlayerPrefs.SetInt("Rank", value); }
    }
    private readonly string[] playerRankType = { "�J��", "²��", "���q", "�x��" };
    private readonly string[] statusType = { "���m", "���L", "��Ԥ�" };
    public static GameMode mode = new GameMode();
    private int seletedIndex = -1;
    private bool isResponseChanllenge = false, isUpdateStatus = false;
    private DateTime LocalTime { get { return DateTime.Now; } }
    private DateTime time;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        network = new Network(playerName, playerRank);

        JoinButton.transform.GetComponent<Button>().enabled = true;    //!!!�O�o�]�^false
        ModeSetting.transform.GetChild(0).gameObject.SetActive(false);
        WaitingListUpdate.transform.GetChild(0).gameObject.SetActive(false);
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

    private IEnumerator UpdateNetwork()    //�ǥ�systemMessage�ȨӽT�{���A
    {
        switch (network.systemMessage) {
            case null:
                //if (!isUpdateStatus)
                //    StartCoroutine(UpdateStatus());
                break;

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
                //��Զi�椤���ʧ@
                //---
                break;
        }
        yield return new WaitForSeconds(1);
        //---
        //�M��playerList����k
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
                GameObject temp = PlayerListContent.transform.GetChild(i).gameObject;
                if (playerList.Count > 0 && !playerList.ContainsKey(temp.name))
                    Destroy(temp);
            }

            int height = playerList.Count > 5 ? playerList.Count * 100 : 500;
            PlayerListContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, height);
            if (playerList.Count > 0) {
                int posY = -50;
                foreach (KeyValuePair<string, (string Name, int Status)> item in playerList) {
                    GameObject temp = PlayerListContent.transform.Find(item.Key)?.gameObject;
                    if (temp == null) {
                        temp = Instantiate(PlayerBarPrefab, PlayerListContent.transform);
                        temp.name = item.Key;
                        temp.transform.GetChild(0).gameObject.GetComponent<Text>().text = item.Key;
                        temp.transform.GetChild(1).gameObject.GetComponent<Text>().text = playerRankType[playerRank];
                        temp.transform.GetChild(2).gameObject.GetComponent<Text>().text = item.Value.Name;
                        temp.transform.GetChild(3).gameObject.GetComponent<Text>().text = statusType[item.Value.Status];
                    }
                    temp.transform.localPosition = new Vector2(600, posY);
                    temp.GetComponent<Button>().onClick.RemoveAllListeners();
                    temp.GetComponent<Button>().onClick.AddListener(delegate () { OnClick_Select(temp.transform.GetSiblingIndex()); });
                    posY -= 100;
                }
                /*GameObject temp1 = Instantiate(PlayerBarPrefab, PlayerListContent.transform);
                temp1.name = "192.168.1.101";
                temp1.transform.GetChild(0).gameObject.GetComponent<Text>().text = "192.168.1.101";
                temp1.transform.GetChild(1).gameObject.GetComponent<Text>().text = playerRankType[0];
                temp1.transform.GetChild(2).gameObject.GetComponent<Text>().text = "hahaha";
                temp1.transform.GetChild(3).gameObject.GetComponent<Text>().text = statusType[0];
                temp1.transform.localPosition = new Vector2(600, posY);
                temp1.GetComponent<Button>().onClick.RemoveAllListeners();
                temp1.GetComponent<Button>().onClick.AddListener(delegate () { OnClick_Select(temp1.transform.GetSiblingIndex()); });
                posY -= 100;*/
            }
            yield return new WaitForSeconds(1);
        }
        SearchButton.GetComponent<Button>().enabled = true;
        WaitingListUpdate.transform.GetChild(0).gameObject.SetActive(false);
    }

    private IEnumerator UpdateStatus()
    {
        isUpdateStatus = true;
        while (isUpdateStatus) {
            if (playerList.Count > 0)
                for (int i = 0;i < PlayerListContent.transform.childCount;i += 1)
                    network.SendStatus(PlayerListContent.transform.GetChild(i).gameObject.name);
            yield return new WaitForSeconds(1);
            if (playerList.Count > 0)
                for (int i = 0;i < PlayerListContent.transform.childCount;i += 1)
                    PlayerListContent.transform.GetChild(i).GetChild(3).gameObject.GetComponent<Text>().text = statusType[playerList[PlayerListContent.transform.GetChild(i).gameObject.name].Item2];
            yield return new WaitForSeconds(5);
        }
    }

    private IEnumerator ReceiveChallenge()  //�����ӦۧO�H���D��
    {
        RespondAcceptOrNot.transform.GetChild(0).gameObject.SetActive(true);
        RespondAcceptOrNot.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = playerList[network.challengerIP].Item1 + ":";
        int countTime = 10;
        while (countTime >= 0) {
            RespondAcceptOrNot.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = countTime.ToString();
            yield return new WaitForSeconds(1);
            countTime -= 1;
        }
        RespondAcceptOrNot.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = null;
        if (!isResponseChanllenge)
            network.DenyChallenge();
        RespondAcceptOrNot.transform.GetChild(0).gameObject.SetActive(false);
    }
    //---------------------------------------------------------------------------------------------------------------

    public void ShowModeSetting()
    {
        ModeSetting.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnClick_CancelInModeSetting()
    {
        ModeSetting.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnClick_ConfirmInModeSetting()
    {
        ModeSetting.transform.GetChild(0).gameObject.SetActive(false);
        JoinButton.transform.GetComponent<Button>().enabled = true;
    }

    public void OnClick_Search()
    {
        WaitingListUpdate.transform.GetChild(0).gameObject.SetActive(true);

        network.SearchUser();
        StartCoroutine(UpdateList());
        if (!isUpdateStatus)
            StartCoroutine(UpdateStatus());
        SearchButton.GetComponent<Button>().enabled = false;
    }

    public void OnClick_Challenge()   //�o�_�D��
    {
        if (seletedIndex != -1) {
            string ip = PlayerListContent.transform.GetChild(seletedIndex).GetChild(0).gameObject.GetComponent<Text>().text;
            int status = Convert.ToInt32(playerList[ip].Item2);
            //Debug.Log(ip);
            seletedIndex = -1;
            if (status == 0) {
                network.SendChallenge(ip);
                WaitingOpponentRespond.transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                HintWhenBusy.transform.GetChild(0).gameObject.SetActive(true);
                HintWhenBusy.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate () { OnClick_ConfirmInHintPanel(); });
            }
        }
    }

    public void OnClick_Accept()  //�����D��
    {
        network.AcceptChallenge();
        RespondAcceptOrNot.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnClick_Deny() //�ڵ��D��
    {
        network.DenyChallenge();
        RespondAcceptOrNot.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnClick_Select(int index)
    {
        seletedIndex = index;
    }

    private void OnClick_ConfirmInHintPanel()
    {
        HintWhenBusy.transform.GetChild(0).gameObject.SetActive(false);
    }

    /*private void Connection()
    {
        network.SendConnection();
        if (LocalTime.Second % 5 == 0)
        {    //�C�������T�{���O�_�_�u

            DateTime tempTime = network.responseTime;
            if (DateTime.Compare(LocalTime, tempTime.AddSeconds(5)) == 1)
            {
                //---
                //"Connection OFF"
                //����_�u�᪺�ʧ@
                //---
            }
        }
    }*/

    public void ReturnToLobby()
    {
        SceneManager.LoadScene(0);
    }
}
