using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLobby : MonoBehaviour {
    public void ReturnToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ExitFromGame()
    {
        Application.Quit();
    }
}