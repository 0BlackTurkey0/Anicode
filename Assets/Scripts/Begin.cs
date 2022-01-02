using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Begin : MonoBehaviour {
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

    void Awake() {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GetTextFromfile(textFile);
        StartCoroutine(SetTextUI());
    }

    // Update is called once per frame
    void Update()
    {
        //buttonReturn.GetComponent<Button>().onClick.AddListener(delegate () { SceneManager.LoadScene("lobby"); });
        if (Input.GetKeyDown(KeyCode.Space) && index == textList.Count) {
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
            if (Input.GetKeyDown(KeyCode.Space) && applicationHandler.GameData.Schedule_Simple == 0) {
                if (textFinished && !cancelTyping)   //打完目前這行要繼續下一行
                    StartCoroutine(SetTextUI());
                else if (!textFinished)   //正在打字
                    cancelTyping = !cancelTyping;
            }
            if (Input.GetKeyDown(KeyCode.Escape) && applicationHandler.GameData.Schedule_Simple == 0)
                Application.Quit();
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
        if (applicationHandler.GameData.Schedule_Simple == 1)
            SceneManager.LoadScene("SimpleStory");
    }

    public void OnClick()
    {
        SceneManager.LoadScene("Lobby");
    }
}

