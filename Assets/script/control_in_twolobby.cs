using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class control_in_twolobby : MonoBehaviour
{
    [SerializeField] GameObject MotionSetting;
    [SerializeField] GameObject JoinBtn;


   

    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void Start()
    {
        JoinBtn.transform.GetComponent<Button>().enabled = false;
        MotionSetting.SetActive(false);
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
//---------------------------------------------------------------------------------------------------------------
    public void Search()
    {

    }
    public void ShowMotionSetting()
    {
        MotionSetting.SetActive(true);
    }
    
    public void unShowMotionSetting()
    {
        MotionSetting.SetActive(false);
        JoinBtn.transform.GetComponent<Button>().enabled = true;
    } 
    public void Join()
    {

    }
    public void AcceptClick()
    {
        MotionSetting.SetActive(false);
    }
    public void EliminateRoomNameOnList()
    {
        
    }
    public void Return()
    {
        //SceneManager.LoadScene(0);
    }
}
