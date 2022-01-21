using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSingleMode : MonoBehaviour {
    public void Return()
    {
        SceneManager.LoadScene("SingleMode");
    }
}