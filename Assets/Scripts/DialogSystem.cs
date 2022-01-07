using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogSystem : MonoBehaviour {
    [Header("UI component")]
    public Text textLabel;
    public Image faceImage;

    [Header("text component")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("head")]
    public Sprite face01, face02, face03, face04, face05;
    public bool Finished { get { return exit; } }

    private bool textFinished, cancelTyping, exit;
    private List<string> textList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Hint").gameObject.transform.GetChild(0).gameObject.SetActive(false);
        GetTextFromfile(textFile);
        textFinished = true;
        exit = false;
        StartCoroutine(SetTextUI());
    }
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed && index == textList.Count) {
            exit = true;
            //GameObject.Find("Panel").SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find("Hint").gameObject.transform.GetChild(0).gameObject.SetActive(true);
            index = 0;
            return;
        }
        //if(Input.GetKeyDown(KeyCode.RightArrow) && index == textList.Count)
          //  GameObject.Find("Hint").gameObject.transform.GetChild(0).gameObject.SetActive(false);
        if (Keyboard.current.spaceKey.isPressed && !exit) {
            if (textFinished && !cancelTyping)//打完目前這行要繼續下一行
                StartCoroutine(SetTextUI());
            else if (!textFinished)//正在打字
                cancelTyping = !cancelTyping;
        }
        //if (Input.GetKeyDown(KeyCode.Escape))
          //  Application.Quit();
    }

    void GetTextFromfile(TextAsset file)
    {
        textList.Clear();
        index = 0;
        var lineData = file.text.Split('\n');
        foreach (var line in lineData)
            textList.Add(line);
    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("Hint").gameObject.transform.GetChild(0).gameObject.SetActive(false);
        switch (textList[index]) {
            case "A\r":
                gameObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                faceImage.sprite = face01;
                index++;
                break;
            case "B\r":
                gameObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                faceImage.sprite = face02;
                index++;
                break;
            case "C\r":
                gameObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                faceImage.sprite = face03;
                index++;
                break;
            case "D\r":
                gameObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                faceImage.sprite = face04;
                index++;
                break;
            case "E\r":
                gameObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                index++;
                break;
        }
        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1) {
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

