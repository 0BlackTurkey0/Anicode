using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SimpleStory_DialogSystem : MonoBehaviour {
    [Header("UI component")]
    [SerializeField] Image faceImage;
    [SerializeField] Text textLabel;
    [SerializeField] GameObject HintText;

    [Header("text component")]
    [SerializeField] TextAsset textFile;
    [SerializeField] int index;
    [SerializeField] float textSpeed;

    [Header("head")]
    [SerializeField] Sprite face01, face02, face03, face04, face05;

    public bool Finished { get { return exit; } }
    private bool textFinished, cancelTyping, exit;
    private List<string> textList = new List<string>();

    void Start() {
        HintText.SetActive(false);
        GetTextFromfile(textFile);
        textFinished = true;
        exit = false;
        StartCoroutine(SetTextUI());
    }

    void Update() {
        if (Keyboard.current.spaceKey.isPressed && index == textList.Count) {
            exit = true;
            textLabel.enabled = false;
            faceImage.enabled = false;
            HintText.SetActive(true);
            index = 0;
            return;
        }
        if (Keyboard.current.spaceKey.isPressed && !exit) {
            if (textFinished && !cancelTyping)  //打完目前這行要繼續下一行
                StartCoroutine(SetTextUI());
            else if (!textFinished) //正在打字
                cancelTyping = !cancelTyping;
        }
    }

    void GetTextFromfile(TextAsset file) {
        textList.Clear();
        index = 0;
        var lineData = file.text.Split('\n');
        foreach (var line in lineData)
            textList.Add(line);
    }

    IEnumerator SetTextUI() {
        textFinished = false;
        textLabel.enabled = true;
        textLabel.text = "";
        faceImage.enabled = true;
        HintText.SetActive(false);
        switch (textList[index]) {
            case "A":
            case "A\r":
                textLabel.enabled = true;
                faceImage.sprite = face01;
                index++;
                break;
            case "B":
            case "B\r":
                textLabel.enabled = true;
                faceImage.sprite = face02;
                index++;
                break;
            case "C":
            case "C\r":
                textLabel.enabled = true;
                faceImage.sprite = face03;
                index++;
                break;
            case "D":
            case "D\r":
                textLabel.enabled = true;
                faceImage.sprite = face04;
                index++;
                break;
            case "E":
            case "E\r":
                textLabel.enabled = true;
                faceImage.enabled = false;
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
        yield return new WaitForSeconds(0.5f);
        cancelTyping = false;
        textFinished = true;
        index++;
    }
}