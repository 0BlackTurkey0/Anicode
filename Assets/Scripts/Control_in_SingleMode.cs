using UnityEngine;
using UnityEngine.SceneManagement;

public class Control_in_SingleMode : MonoBehaviour {
    private ApplicationHandler applicationHandler;

    void Awake()
    {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
    }

    public void ReturnToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ClickSingleMode()
    {
        SceneManager.LoadScene("SingleStory");
    }

    public void ClickSimpleMode()
    {
        if (0 < applicationHandler.GameData.Schedule_Simple && applicationHandler.GameData.Schedule_Simple < 5)
            SceneManager.LoadScene("SimpleStory");
        else
            SceneManager.LoadScene("Start");
    }

}
