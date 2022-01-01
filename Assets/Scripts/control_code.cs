using UnityEngine;
using UnityEngine.UI;

public class Control_Code : MonoBehaviour {
    public GameObject preview_iamge;
    public Sprite picture;
    public GameObject ani_detail;
    public GameObject att_detail;
    public GameObject ins_detail;
    public GameObject cod_detail;
    public GameObject intro_frame;
    public TextAsset TxtFile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
    public void ChangeToKangaroo()
    {
        iamge.GetComponent<RawImage>().texture = picture_kangaroo;
    }
    public void ChangeToWhale()
    {
        iamge.GetComponent<RawImage>().texture = picture_whale;
    }
    public void ChangeToOwl()
    {
        iamge.GetComponent<RawImage>().texture = picture_owl;
    }
    public void ChangeToKoala()
    {
        iamge.GetComponent<RawImage>().texture = picture_koala;
    }
    public void ChangeToFox()
    {
        iamge.GetComponent<RawImage>().texture = picture_fox;
    }
    */
    public void ChangeToAnimal()
    {
        att_detail.SetActive(false);
        ins_detail.SetActive(false);
        cod_detail.SetActive(false);
        ani_detail.SetActive(true);
        intro_frame.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300, -200, 0);
        intro_frame.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 200);
        intro_frame.transform.GetChild(0).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        intro_frame.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(360, 130);
    }
    public void ChangeToAttribute()
    {
        ani_detail.SetActive(false);
        ins_detail.SetActive(false);
        cod_detail.SetActive(false);
        att_detail.SetActive(true);
        intro_frame.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300, -200, 0);
        intro_frame.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 200);
        intro_frame.transform.GetChild(0).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        intro_frame.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(360, 130);
    }
    public void ChangeToInstruction()
    {
        ani_detail.SetActive(false);
        att_detail.SetActive(false);
        cod_detail.SetActive(false);
        ins_detail.SetActive(true);
        intro_frame.GetComponent<RectTransform>().anchoredPosition = new Vector3(500, -120, 0);
        intro_frame.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 240);
        intro_frame.transform.GetChild(0).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        intro_frame.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(220, 150);
    }
    public void ChangeToCodeChallenge()
    {
        ani_detail.SetActive(false);
        att_detail.SetActive(false);
        ins_detail.SetActive(false);
        cod_detail.SetActive(true);
        intro_frame.GetComponent<RectTransform>().anchoredPosition = new Vector3(500, -120, 0);
        intro_frame.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 240);
        intro_frame.transform.GetChild(0).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        intro_frame.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(220, 150);

    }
    public void ShowKanText()
    {
        string content = Resources.Load<TextAsset>("kangaroo_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;

    }
    public void ShowWhaText()
    {
        string content = Resources.Load<TextAsset>("whale_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }

    public void ShowOwlText()
    {
        string content = Resources.Load<TextAsset>("owl_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowKoaText()
    {
        string content = Resources.Load<TextAsset>("koala_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowFoxText()
    {
        string content = Resources.Load<TextAsset>("fox_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowPhyAtkText()
    {
        string content = Resources.Load<TextAsset>("phy_atk_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowPhyDefText()
    {
        string content = Resources.Load<TextAsset>("phy_def_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowSpeAtkText()
    {
        string content = Resources.Load<TextAsset>("spe_atk_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowSpeDefText()
    {
        string content = Resources.Load<TextAsset>("spe_def_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowSpeedText()
    {
        string content = Resources.Load<TextAsset>("speed_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowIfText()
    {
        string content = Resources.Load<TextAsset>("if_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowLoopText()
    {
        string content = Resources.Load<TextAsset>("loop_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowMoveText()
    {
        string content = Resources.Load<TextAsset>("move_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowAssignText()
    {
        string content = Resources.Load<TextAsset>("assign_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowAttackText()
    {
        string content = Resources.Load<TextAsset>("attack_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowAndText()
    {
        string content = Resources.Load<TextAsset>("and_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowOrText()
    {
        string content = Resources.Load<TextAsset>("or_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowGCDText()
    {
        string content = Resources.Load<TextAsset>("GCD_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowLCMText()
    {
        string content = Resources.Load<TextAsset>("LCM_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowSortText()
    {
        string content = Resources.Load<TextAsset>("sort_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowMinText()
    {
        string content = Resources.Load<TextAsset>("min_Text").ToString();
        Debug.Log(content);
        GameObject.Find("introduceframe").transform.GetChild(0).gameObject.GetComponent<Text>().text = content;
    }
    public void ShowKangaroo()
    {
        preview_iamge.SetActive(true);
        picture = Resources.Load("kangaroo", typeof(Sprite)) as Sprite;
        preview_iamge.GetComponent<Image>().sprite = picture;
    }
    public void ShowWhale()
    {
        preview_iamge.SetActive(true);
        picture = Resources.Load("whale", typeof(Sprite)) as Sprite;
        preview_iamge.GetComponent<Image>().sprite = picture;
    }
    public void ShowOwl()
    {
        preview_iamge.SetActive(true);
        picture = Resources.Load("owl", typeof(Sprite)) as Sprite;
        preview_iamge.GetComponent<Image>().sprite = picture;
    }
    public void ShowKoala()
    {
        preview_iamge.SetActive(true);
        picture = Resources.Load("koala", typeof(Sprite)) as Sprite;
        preview_iamge.GetComponent<Image>().sprite = picture;
    }
    public void ShowFox()
    {
        preview_iamge.SetActive(true);
        picture = Resources.Load("fox", typeof(Sprite)) as Sprite;
        preview_iamge.GetComponent<Image>().sprite = picture;
    }

}
