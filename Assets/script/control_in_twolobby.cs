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
    private string playerName;  //TBD ����ϥΪ̦W��
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

    private IEnumerator UpdateNetwork()    //�ǥ�systemMessage�ȨӽT�{���A
    {
        switch (network.systemMessage)
        {
            case SYS.CHALLENGE:
                //---
                //"Challenge from " + playerList[network.challengerIP];
                //���a��ܱ����Ωڵ�
                //---
                break;

            case SYS.ACCEPT:
                //---
                //�i�J��Եe��
                //---
                break;

            case SYS.DENY:
                //---
                //���ڵ���ԫ᪺�ʧ@
                //---
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
        //���a�i�J���H�j�U�Ϋ��U�j�M���s
        //---
        network.SearchUser();
        //---
        //�b�M�椤��ܩҦ����a
        //---
    }
    public void OnClick_Challenge()   //�o�_�D��
    {
        //---
        //�o�������ϥΪ̿�ܪ����
        //�åHip��m���ѼƩI�sSendChallenge
        string ip = "192.168.2.100";
        //---
        network.SendChallenge(ip);
        //---
        //���ݹ��^��
        //---
    }
    public void OnClick_Accept()  //�����D��
    {
        //---
        //�o��������ϥΪ̫��U�T�{���s��
        //---
        network.AcceptChallenge();
    }

    public void OnClick_Deny() //�ڵ��D��
    {
        //---
        //�o��������ϥΪ̫��U�ڵ����s��
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

    public void EliminateRoomNameOnList()
    {
        
    }
    public void Return()
    {
        //SceneManager.LoadScene(0);
    }
}
