using System.Collections.Generic;
using UnityEngine;
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

    private readonly string[] playerRankType = { "入門", "簡單", "普通", "困難", "無" };
    private readonly string[] statusType = { "閒置", "忙碌", "對戰中" };
    private int seletedIndex = -1;
    private DualModePresenter presenter;

    void Awake() {
        presenter = GameObject.Find("Control").GetComponent<DualModePresenter>();
    }

    void Start() {
        ModeSetting.SetActive(false);
        WaitingListUpdate.SetActive(false);
        HintWhenBusy.SetActive(false);
        BothNoSameDifficulty.SetActive(false);
        WaitingOpponentRespond.SetActive(false);
        RespondAcceptOrNot.SetActive(false);
        ModeSettingHint.SetActive(false);
    }

    public void ShowPlayerList(Dictionary<string, (string, int, int)> playerList) {
        int height = playerList.Count > 6 ? playerList.Count * 100 : 600;
        PlayerListContent.transform.localPosition = new Vector3(0, -height/2, 0);
        PlayerListContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, height);
        if (playerList.Count > 0) {
            int posY = height / 2 - 50;//250;
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
                temp.transform.localPosition = new Vector2(0, posY);
                temp.GetComponent<Button>().onClick.RemoveAllListeners();
                temp.GetComponent<Button>().onClick.AddListener(delegate () { OnClick_Select(temp.transform.GetSiblingIndex()); });
                posY -= 100;
            }
        }
    }

    public void UpdatePlayerListStatus(Dictionary<string, (string, int, int)> playerList) {
        for (int i = 0;i < PlayerListContent.transform.childCount;i += 1)
            PlayerListContent.transform.GetChild(i).GetChild(3).gameObject.GetComponent<Text>().text = statusType[playerList[PlayerListContent.transform.GetChild(i).gameObject.name].Item3];
    }

    public void ShowReceiveChallenge(string name, int? time) {
        RespondAcceptOrNot.transform.GetChild(1).GetComponent<Text>().text = name + ":";
        RespondAcceptOrNot.transform.GetChild(3).GetComponent<Text>().text = time.ToString();
    }

    //---------------------------------------------------------------------------------------------

    public void ShowWaitingListUpdate(bool isShow) {
        WaitingListUpdate.SetActive(isShow);
    }

    public void ShowModeSetting(bool isShow) {
        ModeSetting.SetActive(isShow);
    }

    public void ShowHintWhenDeny(bool isShow) {
        HintWhenDeny.SetActive(isShow);
    }

    public void ShowBothNoSameDifficulty(bool isShow) {
        BothNoSameDifficulty.SetActive(isShow);
    }

    public void ShowWaitingOpponentRespond(bool isShow) {
        WaitingOpponentRespond.SetActive(isShow);
    }

    public void ShowRespondAcceptOrNot(bool isShow) {
        RespondAcceptOrNot.SetActive(isShow);
    }

    //---------------------------------------------------------------------------------------------

    public void OnClick_ConfirmInModeSetting() {
        ModeSetting.SetActive(false);
        //JoinButton.transform.GetComponent<Button>().enabled = true;
        NoneModeSettingButton.SetActive(false);
    }

    public void OnClick_Search() {
        WaitingListUpdate.SetActive(true);
        presenter.SearchUser();
        //SearchButton.GetComponent<Button>().enabled = false;
    }

    public void OnClick_Challenge()   //發起挑戰
    {
        if (seletedIndex != -1) {
            if (PlayerListContent.transform.GetChild(seletedIndex).GetChild(3).gameObject.GetComponent<Text>().text == "閒置") {
                presenter.SendChallenge(PlayerListContent.transform.GetChild(seletedIndex).name);
                WaitingOpponentRespond.SetActive(true);
            }
            else {
                HintWhenBusy.SetActive(true);
            }
            seletedIndex = -1;
        }
    }

    public void OnClick_Accept()    //接收挑戰
    {
        presenter.AcceptChallenge();
        RespondAcceptOrNot.SetActive(false);
    }

    public void OnClick_Deny()      //拒絕挑戰
    {
        presenter.DenyChallenge();
        RespondAcceptOrNot.SetActive(false);
    }

    private void OnClick_Select(int index) {
        seletedIndex = index;
    }
}