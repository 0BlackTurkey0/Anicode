using UnityEngine;
using UnityEngine.SceneManagement;

public class Control_in_SingleMotion : MonoBehaviour {
    // Start is called before the first frame update
    void Start()
    {

    }
    public void ReturnToLobby()
    {
        SceneManager.LoadScene(0);
    }
    public void ClickEasySingleMotion()
    {
        SceneManager.LoadScene(9);
    }
}
