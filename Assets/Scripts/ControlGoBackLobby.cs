using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlGoBackLobby : MonoBehaviour {
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ReturnToLobby()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitFromGame()
    {
        Application.Quit();
    }
}
