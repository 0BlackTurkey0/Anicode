using UnityEngine;
using UnityEngine.SceneManagement;

public class Control_in_SingleMode : MonoBehaviour
{
    private ApplicationHandler applicationHandler;
    // Start is called before the first frame update

    void Awake() {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
    }

    void Start()
    {

    }
    public void ReturnToLobby()
    {
        SceneManager.LoadScene(0);
    }
    public void ClickSimpleMode()
    {
        if (0 < applicationHandler.GameData.Schedule_Simple && applicationHandler.GameData.Schedule_Simple < 5)
            SceneManager.LoadScene("SimpleStory");
        else
            SceneManager.LoadScene(9);
    }
}
