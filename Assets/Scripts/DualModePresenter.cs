using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DualModePresenter : MonoBehaviour {
    //private int playerRank;
    private Dictionary<string, (string, int, int)> playerList;
    private bool isResponseChanllenge = false, isUpdateStatus = false, isNotOverTime = false;
    private ApplicationHandler applicationHandler;
    private Network network;
    private DualMode dualMode;

    void Awake() {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
        network = GameObject.Find("Network").GetComponent<Network>();
        dualMode = GameObject.Find("Control").GetComponent<DualMode>();
    }

    void Start() {
        StartCoroutine(UpdateNetwork());
    }

    private IEnumerator UpdateNetwork() {   //藉由systemMessage值來確認狀態
        while (true) {
            switch (network.systemMessage) {
                case SYS.CHALLENGE:
                    StartCoroutine(ReceiveChallenge());
                    network.ClearSystemMessage();
                    break;

                case SYS.ACCEPT:
                    if (network.isGuest) {
                        isNotOverTime = true;
                        dualMode.ShowWaitingOpponentRespond(false);
                        while (!network.isModeReceive) {

                        }
                        network.isModeReceive = false;
                    }
                    else {
                        DecideDifficulty();
                    }
                    break;

                case SYS.DENY:
                    dualMode.ShowWaitingOpponentRespond(false);
                    dualMode.ShowHintWhenDeny(true);
                    network.ClearSystemMessage();
                    break;

                case SYS.MODE:
                    dualMode.ShowBothNoSameDifficulty(true);
                    network.ClearSystemMessage();
                    //network.DenyChallenge();
                    break;

                case SYS.READY:
                    network.IntoGame();
                    applicationHandler.IsDuel = true;
                    SceneManager.LoadScene("Battle");
                    break;

                case SYS.GAME:
                    CheckConnection();
                    break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator UpdateList() {
        int loop = 4;
        while (loop-- > 0) {
            playerList = network.dict;
            dualMode.ShowPlayerList(playerList);
            yield return new WaitForSeconds(1);
        }
        dualMode.ShowWaitingListUpdate(false);
    }

    private IEnumerator UpdateStatus() {
        isUpdateStatus = true;
        while (isUpdateStatus) {
            if (playerList.Count > 0)
                foreach (KeyValuePair<string, (string Name, int Rank, int Status)> item in playerList)
                    network.SendStatus(item.Key);
            yield return new WaitForSeconds(1);
            playerList = network.dict;
            if (playerList.Count > 0)
                dualMode.UpdatePlayerListStatus(playerList);
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator ReceiveChallenge()  //接收來自別人的挑戰
    {
        dualMode.ShowRespondAcceptOrNot(true);
        string name = playerList[network.challengerIP].Item1;
        int countTime = 10;
        while (countTime >= 0) {
            dualMode.ShowReceiveChallenge(name, countTime);
            yield return new WaitForSeconds(1);
            countTime -= 1;
        }
        dualMode.ShowReceiveChallenge(name, null);
        if (!isResponseChanllenge)
            network.DenyChallenge();
        isResponseChanllenge = false;
        dualMode.ShowRespondAcceptOrNot(false);
    }

    private IEnumerator DetectOverTime() {
        yield return new WaitForSeconds(15);
        if (!isNotOverTime)
            network.OverTimeDeny();
        isNotOverTime = false;
    }

    //---------------------------------------------------------------------------------------------

    private void DecideDifficulty() {
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
                dualMode.ShowBothNoSameDifficulty(true);
                network.finalDifficulty = -1;
            }
        }
        else {
            dualMode.ShowBothNoSameDifficulty(true);
            network.finalDifficulty = -1;
        }
        network.SendFinalDifficulty();
    }

    private void CheckConnection() {
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

    public void SearchUser() {
        network.SearchUser();
        StartCoroutine(UpdateList());
        if (!isUpdateStatus)
            StartCoroutine(UpdateStatus());
    }

    public void SendChallenge(string ip) {
        network.SendChallenge(ip);
        StartCoroutine(DetectOverTime());
    }

    public void AcceptChallenge() {
        network.AcceptChallenge();
        isResponseChanllenge = true;
    }

    public void DenyChallenge() {
        network.DenyChallenge();
    }
}
