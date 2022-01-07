using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyStory_ReturnLobby : MonoBehaviour
{
    public void Return()
    {
        SceneManager.LoadScene("SingleMode");
    }
}
