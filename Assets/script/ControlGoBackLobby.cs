using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlGoBackLobby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ReturnToLobby()
    {
        SceneManager.LoadScene(0);
    }
}
