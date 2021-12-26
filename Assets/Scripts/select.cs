using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class select : MonoBehaviour
{
    int first;
    public void PlayStory()
    {
        first = PlayerPrefs.GetInt("Record_First", 0);
        if (first != 0)
        {
            Debug.Log("aaaaaaa");
            Debug.Log(first);
            SceneManager.LoadScene("story1");
        }
    }
    /*public void PlayStory1()
    {
        SceneManager.LoadScene("story1");
    }
    public void PlayStory2()
    {
        SceneManager.LoadScene("story2");
    }
    public void PlayStory3()
    {
        SceneManager.LoadScene("story3");
    }
    public void PlayStory()
    {
        Debug.Log("###");
        SceneManager.LoadScene("select1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }*/
}
