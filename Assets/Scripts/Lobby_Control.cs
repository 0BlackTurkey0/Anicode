using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Lobby_Control : MonoBehaviour
{
    [SerializeField] GameObject ReviseNameWindow;
    [SerializeField] GameObject SingleMotionBtn;
    [SerializeField] GameObject DoubleMotionBtn;
    [SerializeField] GameObject StoreBtn;
    [SerializeField] GameObject ReviseNameBtn;
    [SerializeField] GameObject BookBtn;
    [SerializeField] GameObject AchievementBtn;
    [SerializeField] GameObject SettingBtn;
    [SerializeField] GameObject WarningOnPlayerNameWindow;
    [SerializeField] GameObject PlayerName;
    [SerializeField] Text RevisePlayerName;
    [SerializeField] GameObject RevisePlayerNamePlaceholder;
    // Start is called before the first frame update
    void Start()
    {
        RevisePlayerNamePlaceholder.GetComponent<Text>().text = PlayerName.GetComponent<Text>().text;
        ReviseNameWindow.SetActive(false);
        WarningOnPlayerNameWindow.SetActive(false);
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
        ReviseNameWindow.SetActive(true);
        SingleMotionBtn.SetActive(false);
        DoubleMotionBtn.SetActive(false);
        StoreBtn.SetActive(false);
        ReviseNameBtn.SetActive(false);
        BookBtn.SetActive(false);
        AchievementBtn.SetActive(false);
        SettingBtn.SetActive(false);
    }
    public void ReviseNameConfirm()
    {
        if (RevisePlayerName.text.Length > 9 || RevisePlayerName.text.Length == 0) {
            WarningOnPlayerNameWindow.SetActive(true);
        }
        else {
            PlayerName.GetComponent<Text>().text = RevisePlayerName.text;
            RevisePlayerNamePlaceholder.GetComponent<Text>().text = PlayerName.GetComponent<Text>().text;
            RevisePlayerName.text.Remove(0, PlayerName.GetComponent<Text>().text.Length);
            ReviseNameWindow.SetActive(false);
            SingleMotionBtn.SetActive(true);
            DoubleMotionBtn.SetActive(true);
            StoreBtn.SetActive(true);
            ReviseNameBtn.SetActive(true);
            BookBtn.SetActive(true);
            AchievementBtn.SetActive(true);
            SettingBtn.SetActive(true);
        }
    }

    public void ReviseNameCancel()
    {
        RevisePlayerNamePlaceholder.GetComponent<Text>().text = PlayerName.GetComponent<Text>().text;
        RevisePlayerName.text = "";
        ReviseNameWindow.SetActive(false);
        SingleMotionBtn.SetActive(true);
        DoubleMotionBtn.SetActive(true);
        StoreBtn.SetActive(true);
        ReviseNameBtn.SetActive(true);
        BookBtn.SetActive(true);
        AchievementBtn.SetActive(true);
        SettingBtn.SetActive(true);
    }
    public void WarningOnPlayerNameRealizeOnClick()
    {
        WarningOnPlayerNameWindow.SetActive(false);
        RevisePlayerNamePlaceholder.GetComponent<Text>().text = PlayerName.GetComponent<Text>().text; ;
        RevisePlayerName.text = "";
    }
    public void ExceptForLobbyReturnOnClick()
    {
        SceneManager.LoadScene(0);
    }
}
