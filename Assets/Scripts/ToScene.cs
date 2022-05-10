using UnityEngine;
using UnityEngine.SceneManagement;

public class ToScene : MonoBehaviour {
    public void ReturnToLobby() {
        SceneManager.LoadScene("Lobby");
    }

    public void ReturnToSingleMode() {
        SceneManager.LoadScene("SingleMode");
    }

    public void ExitFromGame() {
        Application.Quit();
    }
}