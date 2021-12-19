using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lobby_Control : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SingleMotionOnclick()
    {
        SceneManager.LoadScene(2);
    }
    public void DoubleMotionOnClick()
    {
        SceneManager.LoadScene(1);
    }
    public void StoreOnClick()
    {
        SceneManager.LoadScene(3);
    }
    public void AchievementOnclick()
    {
        SceneManager.LoadScene(5);
    }
    public void BookOnClick()
    {
        SceneManager.LoadScene(6);
    }
    public void SettingOnClick()
    {
        SceneManager.LoadScene(4);
    }
    public void ReviseNameOnClick()
    {

    }
    public void ExceptForLobbyReturnOnClick()
    {
        SceneManager.LoadScene(0);
    }
}
