using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour {
    [SerializeField] GameObject Ach1;
    [SerializeField] GameObject Ach2;
    [SerializeField] GameObject Ach3;
    [SerializeField] GameObject Ach4;
    [SerializeField] GameObject Ach5;
    [SerializeField] GameObject Ach6;
    [SerializeField] GameObject Ach7;
    [SerializeField] GameObject Ach8;
    [SerializeField] GameObject Ach9;
    [SerializeField] GameObject Ach10;
    [SerializeField] GameObject illustration;
    [SerializeField] GameObject illustration_text;

    private ApplicationHandler applicationHandler;

    void Awake()
    {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
    }

    void Start()
    {
        int count = 0;
        for (int i = 0;i < 10;i++)
            if (applicationHandler.GameData.Items[i]) count++;
        if (count >= 4) Ach1.transform.GetChild(1).gameObject.SetActive(true);
        if (count >= 6) Ach2.transform.GetChild(1).gameObject.SetActive(true);
        if (count >= 8) Ach3.transform.GetChild(1).gameObject.SetActive(true);
        count = 0;
        for (int i = 15;i < 24;i++)
            if (applicationHandler.GameData.Items[i]) count++;
        if (count >= 4) Ach4.transform.GetChild(1).gameObject.SetActive(true);
        if (count >= 6) Ach5.transform.GetChild(1).gameObject.SetActive(true);
        if (count >= 8) Ach6.transform.GetChild(1).gameObject.SetActive(true);
        count = 0;
        for (int i = 29;i < 34;i++)
            if (applicationHandler.GameData.Items[i]) count++;
        if (count >= 3) Ach7.transform.GetChild(1).gameObject.SetActive(true);
        if (count >= 4) Ach8.transform.GetChild(1).gameObject.SetActive(true);
        if (count >= 5) Ach9.transform.GetChild(1).gameObject.SetActive(true);
        count = 0;
        for (int i = 0;i < 34;i++)
            if (applicationHandler.GameData.Items[i]) count++;
        if (count == 21) Ach10.transform.GetChild(1).gameObject.SetActive(true);

    }

    public void Ach1_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "成功集滿四個水果!!";
    }

    public void Ach2_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "成功集滿六個水果!!";
    }

    public void Ach3_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "成功集滿八個水果!!";
    }

    public void Ach4_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "成功集滿四個肉類!!";
    }

    public void Ach5_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "成功集滿六個肉類!!";
    }

    public void Ach6_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "成功集滿八個肉類!!";
    }

    public void Ach7_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "成功集滿三個寶物!!";
    }

    public void Ach8_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "成功集滿四個寶物!!";
    }

    public void Ach9_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "成功集滿五個寶物!!";
    }

    public void Ach10_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "成功集滿所有物品!!";
    }

    public void CloseButton_OnClick()
    {
        illustration.SetActive(false);
    }
}