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
        SceneManager.LoadScene(0);
    }

    public void ClickSingleMode()
    {
        SceneManager.LoadScene(10);
    }

    public void ClickSimpleMode()
    {
        if (0 < applicationHandler.GameData.Schedule_Simple && applicationHandler.GameData.Schedule_Simple < 5)
            SceneManager.LoadScene(7);
        else
            SceneManager.LoadScene(9);
    }
}
