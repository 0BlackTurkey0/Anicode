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
    [SerializeField] GameObject BothNoSameDifficulty;
    [SerializeField] GameObject WaitingOpponentRespond;
    [SerializeField] GameObject RespondAcceptOrNot;
    [Header("Button")]
    [SerializeField] GameObject JoinButton;
    [SerializeField] GameObject SearchButton;
    [Header("Prefab")]
    [SerializeField] GameObject PlayerBarPrefab;

    public Network network = null;
    public GameMode playerMode { get; private set; } = new GameMode();
    public bool isConnect { get; private set; } = true;
    private string playerName;
    private Dictionary<string, (string, int)> playerList { get { return network.dict; } }
    private int playerRank {
        get { return PlayerPrefs.GetInt("Rank", 0); }
        set { PlayerPrefs.SetInt("Rank", value); }
    }
    private readonly string[] playerRankType = { "�J��", "²��", "���q", "�x��" };
    private readonly string[] statusType = { "���m", "���L", "��Ԥ�" };

    private int seletedIndex = -1;
    private bool isResponseChanllenge = false, isUpdateStatus = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (network == null || !network.IsRun())
            network = new Network(playerName, playerRank);

        JoinButton.transform.GetComponent<Button>().enabled = false;
        ModeSetting.transform.GetChild(0).gameObject.SetActive(false);
        WaitingListUpdate.transform.GetChild(0).gameObject.SetActive(false);
        HintWhenBusy.transform.GetChild(0).gameObject.SetActive(false);
        BothNoSameDifficulty.transform.GetChild(0).gameObject.SetActive(false);
        WaitingOpponentRespond.transform.GetChild(0).gameObject.SetActive(false);
        RespondAcceptOrNot.transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(UpdateNetwork());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnApplicationQuit()
    {
        network.Quit();
    }

    private IEnumerator UpdateNetwork()    //�ǥ�systemMessage�ȨӽT�{���A
    {
        while (true) {
            switch (network.systemMessage) {
                case SYS.CHALLENGE:
                    StartCoroutine(ReceiveChallenge());
                    network.ClearSystemMessage();
                    break;

                case SYS.ACCEPT:
                    network.SendModeSetting(playerMode);
                    break;

                case SYS.DENY:
                    WaitingOpponentRespond.transform.GetChild(0).gameObject.SetActive(false);
                    break;

                case SYS.READY:
                    Debug.Log("###");
                    if (network.isGuest) {
                        WaitingOpponentRespond.transform.GetChild(0).gameObject.SetActive(false);
                        while (!network.isModeReceive) {

                        }
                        network.isModeReceive = false;
                        network.IntoGame();
                        //SceneManager.LoadScene();
                    }
                    else {
                        DecideDifficulty();
                        network.IntoGame();
                        //SceneManager.LoadScene();
                    }
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
                        temp.transform.GetChild(3).gameObject.GetComponent<Text>().text = statusType[item.Value.Status];
                    }
                    temp.transform.GetChild(2).gameObject.GetComponent<Text>().text = item.Value.Name;
                    temp.transform.localPosition = new Vector2(600, posY);
                    temp.GetComponent<Button>().onClick.RemoveAllListeners();
                    temp.GetComponent<Button>().onClick.AddListener(delegate () { OnClick_Select(temp.transform.GetSiblingIndex()); });
                    posY -= 100;
                }
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

    public void SetMode(GameMode mode)
    {
        playerMode = mode;
    }

    private void DecideDifficulty()
    {
        List<int> temp = new List<int>();
        for (int i = 0;i < 4;i += 1)
            if (playerMode.Difficulty[i] && network.challengerMode.Difficulty[i])
                temp.Add(i);
        if (temp.Count > 0) {
            System.Random random = new System.Random();
            network.finalDifficulty = temp[random.Next(temp.Count)];
        }
        else {
            BothNoSameDifficulty.transform.GetChild(0).gameObject.SetActive(true);
            network.finalDifficulty = -1;
        }
        network.SendFinalDifficulty();
    }

    private void Connection()
    {
        network.SendConnection();
        DateTime LocalTime = DateTime.Now;
        if (LocalTime.Second % 5 == 0) {    //�C�������T�{���O�_�_�u
            DateTime tempTime = network.responseTime;
            if (DateTime.Compare(LocalTime, tempTime.AddSeconds(5)) == 1)
                isConnect = false;
            else
                isConnect = true;
        }
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
            }
        }
    }

    public void OnClick_Accept()  //�����D��
    {
        network.AcceptChallenge();
        isResponseChanllenge = true;
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

    public void OnClick_ConfirmInHintWhenBusy()
    {
        HintWhenBusy.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnClick_ConfirmInBothNoSameDifficulty()
    {
        BothNoSameDifficulty.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ReturnToLobby()
    {
        SceneManager.LoadScene(0);
    }
}
