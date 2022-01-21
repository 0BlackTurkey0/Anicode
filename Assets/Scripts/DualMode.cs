using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DualMode : MonoBehaviour {
    [Header("Panel")]
    [SerializeField] GameObject ModeSetting;
    [SerializeField] GameObject PlayerListContent;
    [SerializeField] GameObject WaitingListUpdate;
    [SerializeField] GameObject HintWhenBusy;
    [SerializeField] GameObject HintWhenDeny;
    [SerializeField] GameObject BothNoSameDifficulty;
    [SerializeField] GameObject WaitingOpponentRespond;
    [SerializeField] GameObject RespondAcceptOrNot;
    [SerializeField] GameObject ModeSettingHint;
    [Header("Button")]
    [SerializeField] GameObject JoinButton;
    [SerializeField] GameObject SearchButton;
    [SerializeField] GameObject NoneModeSettingButton;
    [Header("Prefab")]
    [SerializeField] GameObject PlayerBarPrefab;

    private int playerRank;
    private Dictionary<string, (string, int, int)> playerList { get { return network.dict; } }
    private readonly string[] playerRankType = { "入門", "簡單", "普通", "困難", "無" };
    private readonly string[] statusType = { "閒置", "忙碌", "對戰中" };

    private int seletedIndex = -1;
    private bool isResponseChanllenge = false, isUpdateStatus = false, isNotOverTime = false;
    private ApplicationHandler applicationHandler;
    private Network network;

    void Awake()
    {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
        network = GameObject.Find("Network").GetComponent<Network>();
    }

    void Start()
    {
        ModeSetting.SetActive(false);
        WaitingListUpdate.SetActive(false);
        HintWhenBusy.SetActive(false);
        BothNoSameDifficulty.SetActive(false);
        WaitingOpponentRespond.SetActive(false);
        RespondAcceptOrNot.SetActive(false);
        ModeSettingHint.SetActive(false);
        StartCoroutine(UpdateNetwork());
    }

    void OnApplicationQuit()
    {

    }

    private IEnumerator UpdateNetwork()    //藉由systemMessage值來確認狀態
    {
        while (true) {
            switch (network.systemMessage) {
                case SYS.CHALLENGE:
                    StartCoroutine(ReceiveChallenge());
                    network.ClearSystemMessage();
                    break;

                case SYS.ACCEPT:
                    if (network.isGuest) {
                        isNotOverTime = true;
                        WaitingOpponentRespond.SetActive(false);
                        while (!network.isModeReceive) {

                        }
                        network.isModeReceive = false;
                    }
                    else {
                        DecideDifficulty();
                    }
                    break;

                case SYS.DENY:
                    WaitingOpponentRespond.SetActive(false);
                    HintWhenDeny.SetActive(true);
                    network.ClearSystemMessage();
                    break;

                case SYS.MODE:
                    BothNoSameDifficulty.SetActive(true);
                    network.ClearSystemMessage();
                    //network.DenyChallenge();
                    break;

                case SYS.READY:
                    network.IntoGame();
                    applicationHandler.IsDuel = true;
                    SceneManager.LoadScene("Battle");
                    break;

                case SYS.GAME:
                    Connection();
                    break;
            }
            yield return new WaitForSeconds(1);
        }
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

            int height = playerList.Count > 6 ? playerList.Count * 100 : 600;
            PlayerListContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, height);

            if (playerList.Count > 0) {
                int posY = -50;
                foreach (KeyValuePair<string, (string Name, int Rank, int Status)> item in playerList) {
                    GameObject temp = PlayerListContent.transform.Find(item.Key)?.gameObject;
                    if (temp == null) {
                        temp = Instantiate(PlayerBarPrefab, PlayerListContent.transform);
                        temp.name = item.Key;
                        temp.transform.GetChild(0).gameObject.GetComponent<Text>().text = item.Key;
                    }
                    temp.transform.GetChild(1).gameObject.GetComponent<Text>().text = playerRankType[item.Value.Rank];
                    temp.transform.GetChild(2).gameObject.GetComponent<Text>().text = item.Value.Name;
                    temp.transform.GetChild(3).gameObject.GetComponent<Text>().text = statusType[item.Value.Status];
                    temp.transform.localPosition = new Vector2(650, posY);
                    temp.GetComponent<Button>().onClick.RemoveAllListeners();
                    temp.GetComponent<Button>().onClick.AddListener(delegate () { OnClick_Select(temp.transform.GetSiblingIndex()); });
                    posY -= 100;
                }
            }
            yield return new WaitForSeconds(1);
        }
        //SearchButton.GetComponent<Button>().enabled = true;
        WaitingListUpdate.SetActive(false);
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
                    PlayerListContent.transform.GetChild(i).GetChild(3).gameObject.GetComponent<Text>().text = statusType[playerList[PlayerListContent.transform.GetChild(i).gameObject.name].Item3];
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator ReceiveChallenge()  //接收來自別人的挑戰
    {
        RespondAcceptOrNot.SetActive(true);
        RespondAcceptOrNot.transform.GetChild(1).GetComponent<Text>().text = playerList[network.challengerIP].Item1 + ":";
        int countTime = 10;
        while (countTime >= 0) {
            RespondAcceptOrNot.transform.GetChild(3).GetComponent<Text>().text = countTime.ToString();
            yield return new WaitForSeconds(1);
            countTime -= 1;
        }
        RespondAcceptOrNot.transform.GetChild(3).GetComponent<Text>().text = null;
        if (!isResponseChanllenge)
            network.DenyChallenge();
        isResponseChanllenge = false;
        RespondAcceptOrNot.SetActive(false);
    }

    private IEnumerator DetectOverTime()
    {
        yield return new WaitForSeconds(15);
        if (!isNotOverTime)
            network.OverTimeDeny();
        isNotOverTime = false;
    }
    //---------------------------------------------------------------------------------------------------------------

    public void ShowModeSetting()
    {
        ModeSetting.SetActive(true);
    }

    private void DecideDifficulty()
    {
        List<int> temp = new List<int>();
        if (network.playerMode != null && network.challengerMode != null) {
            for (int i = 0;i < 4;i += 1)
                if (network.playerMode.Difficulty[i] && network.challengerMode.Difficulty[i])
                    temp.Add(i);
            if (temp.Count > 0) {
                var random = new System.Random();
                network.finalDifficulty = temp[random.Next(temp.Count)];
            }
            else {
                BothNoSameDifficulty.SetActive(true);
                network.finalDifficulty = -1;
            }
        }
        else {
            BothNoSameDifficulty.SetActive(true);
            network.finalDifficulty = -1;
        }
        network.SendFinalDifficulty();
    }

    private void Connection()
    {
        network.SendConnection();
        DateTime LocalTime = DateTime.Now;
        if (LocalTime.Second % 5 == 0) {    //每五秒確認對手是否斷線
            DateTime tempTime = network.responseTime;
            if (DateTime.Compare(LocalTime, tempTime.AddSeconds(5)) == 1)
                network.isConnect = false;
            else
                network.isConnect = true;
        }
    }

    public void OnClick_CancelInModeSetting()
    {
        ModeSetting.SetActive(false);
    }

    public void OnClick_ConfirmInModeSetting()
    {
        ModeSetting.SetActive(false);
        //JoinButton.transform.GetComponent<Button>().enabled = true;
        NoneModeSettingButton.SetActive(false);
    }

    public void OnClick_Search()
    {
        WaitingListUpdate.SetActive(true);
        network.SearchUser();
        StartCoroutine(UpdateList());
        if (!isUpdateStatus)
            StartCoroutine(UpdateStatus());
        //SearchButton.GetComponent<Button>().enabled = false;
    }

    public void OnClick_NoneModeSetting()
    {
        ModeSettingHint.SetActive(true);
    }

    public void OnClick_Challenge()   //發起挑戰
    {
        if (seletedIndex != -1) {
            string ip = PlayerListContent.transform.GetChild(seletedIndex).GetChild(0).gameObject.GetComponent<Text>().text;
            int status = Convert.ToInt32(playerList[ip].Item3);
            seletedIndex = -1;
            if (status == 0) {
                network.SendChallenge(ip);
                WaitingOpponentRespond.SetActive(true);
                StartCoroutine(DetectOverTime());
            }
            else {
                HintWhenBusy.SetActive(true);
            }
        }
    }

    public void OnClick_Accept()    //接收挑戰
    {
        network.AcceptChallenge();
        isResponseChanllenge = true;
        RespondAcceptOrNot.SetActive(false);
    }

    public void OnClick_Deny()      //拒絕挑戰
    {
        network.DenyChallenge();
        RespondAcceptOrNot.SetActive(false);
    }

    private void OnClick_Select(int index)
    {
        seletedIndex = index;
    }

    public void OnClick_ConfirmInHintWhenBusy()
    {
        HintWhenBusy.SetActive(false);
    }

    public void OnClick_ConfirmInHintWhenDeny()
    {
        HintWhenDeny.SetActive(false);
    }

    public void OnClick_ConfirmInBothNoSameDifficulty()
    {
        BothNoSameDifficulty.SetActive(false);
    }

    public void OnClick_ConfirmInModeSettingHint()
    {
        ModeSettingHint.SetActive(false);
    }

    public void ReturnToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}