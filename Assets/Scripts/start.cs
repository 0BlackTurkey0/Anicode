using System.Collections;
using System.Collections.Generic;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("UI component")]
    public Text textLabel;
    //public Image faceImage;

    [Header("text component")]
    public TextAsset textFile;
    public int index,first;
    public float textSpeed;

    private bool textFinished;
    bool cancelTyping;
    //public bool Finished { get { return exit; } }
    List<string> textList = new List<string>();
    

    void Awake()
    {
        PlayerPrefs.SetInt("Record_First", 0);
        Debug.Log(first);
        GetTextFromfile(textFile);
        first=PlayerPrefs.GetInt("Record_First", 0);
        Debug.Log(first);
    }

    private void OnEnable()
    {
        textFinished = true;
        //exit = false;
        StartCoroutine(SetTextUI());
    }

   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && index == textList.Count)
        {
            PlayerPrefs.SetInt("Record_First", 1);
            first = PlayerPrefs.GetInt("Record_First", 0);
            Debug.Log("******");
            Debug.Log(first);
            GameObject.Find("DialogPanel").SetActive(false);
            GameObject.Find("Leader").SetActive(false);
            GameObject.Find("control").gameObject.transform.GetChild(0).gameObject.SetActive(true);
            index = 0;
            //return;
        }
        /*if (first != 0)
        {
            SceneManager.LoadScene("Story1");
        }*/
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && first == 0)
            {
                if (textFinished && !cancelTyping)//打完目前這行要繼續下一行
                {
                    StartCoroutine(SetTextUI());
                }
                else if (!textFinished)//正在打字
                {
                    cancelTyping = !cancelTyping;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape) && first == 0)
            {
                Application.Quit();
            }
        }
        
    }

    void GetTextFromfile(TextAsset file)
    {
        textList.Clear();
        index = 0;
        var lineData = file.text.Split('\n');
        foreach (var line in lineData)
         {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";
        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;
    }
}

