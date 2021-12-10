using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogSystem : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("UI component")]
    public Text textLabel;
    public Image faceImage;

    [Header("text component")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("head")]
    public Sprite face01, face02, face03, face04, face05;

    private bool textFinished,exit;
    bool cancelTyping;
    public bool Finished { get { return exit; } }
    List<string> textList = new List<string>();

 
    void Awake()
    {
        GetTextFromfile(textFile);
    }

    private void OnEnable()
    {
        //textLabel.text = textList[index];
        // index++;
        textFinished = true;
        exit = false;
        StartCoroutine(SetTextUI());
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && index == textList.Count)
        {
            exit = true;
            gameObject.SetActive(false);
            index = 0;
            return;
        }
  
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(textFinished && !cancelTyping)//打完目前這行要繼續下一行
            {
                StartCoroutine(SetTextUI());
            }
            else if(!textFinished)//正在打字
            {
                cancelTyping = !cancelTyping;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
        switch (textList[index])
        {
            case "A\r":
                faceImage.sprite = face01;
                index++;
                break;
            case "B\r":
                faceImage.sprite = face02;
                index++;
                break;
            case "C\r":
                faceImage.sprite = face03;
                index++;
                break;
            case "D\r":
                faceImage.sprite = face04;
                index++;
                break;
        }
        int letter = 0;
        while(!cancelTyping && letter< textList[index].Length - 1)
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

