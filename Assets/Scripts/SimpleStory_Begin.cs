using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimpleStory_Begin : MonoBehaviour {
    [Header("UI Component")]
    [SerializeField] Text textLabel;
    [SerializeField] GameObject Leader;
    [SerializeField] GameObject DialogPanel;
    [SerializeField] GameObject StartBtn;

    [Header("Text Component")]
    [SerializeField] TextAsset textFile;
    [SerializeField] float textSpeed;

    private int index;
    private bool cancelTyping, textFinished = true;
    //public bool Finished { get { return exit; } }
    private List<string> textList = new List<string>();
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
        if (Keyboard.current.spaceKey.isPressed && index == textList.Count) {
            applicationHandler.GameData.Schedule_Simple = 1;
            applicationHandler.GameData.SaveData();
            DialogPanel.SetActive(false);
            Leader.SetActive(false);
            StartBtn.SetActive(true);
            StartBtn.GetComponent<Button>().onClick.AddListener(delegate () { PlayStory(); });
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
        yield return new WaitForSeconds(0.5f);
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
}