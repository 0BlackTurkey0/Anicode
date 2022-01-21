using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimpleStory_Begin : MonoBehaviour {
    [Header("UI component")]
    public Text textLabel;
    //public Image faceImage;

    [Header("text component")]
    public TextAsset textFile;
    public float textSpeed;

    private int index;
    private bool cancelTyping, textFinished = true;
    //public bool Finished { get { return exit; } }
    private List<string> textList = new List<string>();
    //private GameObject buttonReturn = GameObject.Find("Return").gameObject;
    private ApplicationHandler applicationHandler;

    void Awake()
    {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
    }

    void Start()
    {
        GetTextFromfile(textFile);
        StartCoroutine(SetTextUI());
    }

    void Update()
    {
        //buttonReturn.GetComponent<Button>().onClick.AddListener(delegate () { SceneManager.LoadScene("lobby"); });
        if (Keyboard.current.spaceKey.isPressed && index == textList.Count) {
            applicationHandler.GameData.Schedule_Simple = 1;
            applicationHandler.GameData.SaveData();
            GameObject.Find("DialogPanel").SetActive(false);
            GameObject.Find("Leader").SetActive(false);
            GameObject buttonStart = GameObject.Find("control").gameObject.transform.GetChild(0).gameObject;
            buttonStart.SetActive(true);
            buttonStart.GetComponent<Button>().onClick.AddListener(delegate () { PlayStory(); });
            index = 0;
        }
        else {
            if (Keyboard.current.spaceKey.isPressed && applicationHandler.GameData.Schedule_Simple == 0) {
                if (textFinished && !cancelTyping)   //打完目前這行要繼續下一行
                    StartCoroutine(SetTextUI());
                else if (!textFinished)   //正在打字
                    cancelTyping = !cancelTyping;
            }
        }
    }

    private void GetTextFromfile(TextAsset file)
    {
        textList.Clear();
        index = 0;
        var lineData = file.text.Split('\n');
        foreach (var line in lineData)
            textList.Add(line);
    }

    private IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";
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

    private void PlayStory()
    {
        if (applicationHandler.GameData.Schedule_Simple == 1) {
            applicationHandler.GameData.SaveData();
            SceneManager.LoadScene("SimpleStory");
        }
    }

    public void OnClick()
    {
        SceneManager.LoadScene("Lobby");
    }
}