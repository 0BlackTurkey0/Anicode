using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour {
    public GameObject Ach1;
    public GameObject Ach2;
    public GameObject Ach3;
    public GameObject Ach4;
    public GameObject Ach5;
    public GameObject Ach6;
    public GameObject Ach7;
    public GameObject Ach8;
    public GameObject Ach9;
    public GameObject Ach10;
    public GameObject Ach11;
    public GameObject Ach12;
    public GameObject Ach13;
    public GameObject Ach14;
    public GameObject illustration;
    public GameObject illustration_text;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Ach1_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "�b��H�Ҧ��������J�����׮ʯ���";
    }
    public void Ach2_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "�b��H�Ҧ�������²�����׮ʯ���";
    }
    public void Ach3_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "�b��H�Ҧ����������q���׮ʯ���";
    }
    public void Ach4_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "�b��H�Ҧ��������x�����׮ʯ���";
    }
    public void Ach5_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "�b���H�Ҧ������@�^�X������4���H�W���j��(Loop)���O";
    }
    public void Ach6_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "�b���H�Ҧ������@�^�X������8���H�W������(If...Else...)���O";
    }
    public void Ach7_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "�b���H�Ҧ������@�^�X���y��100�I�H�W���ˮ`";
    }
    public void Ach8_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "�b���H�Ҧ������@�^�X������15���H�W����ƹB��l";
    }
    public void Ach9_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "�b���H�Ҧ������@�^�X������25���H�W���޿�����Y�B��l";
    }
    public void Ach10_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "�b���H�Ҧ������@�^�X������7�إH�W���S����O";
    }
    public void Ach11_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "�b���H�Ҧ����^�X�ƹF��8�H�W";
    }
    public void Ach12_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "�b���H�Ҧ��������D��";
    }
    public void Ach13_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "�b���H�Ҧ������o�ӧQ";
    }
    public void Ach14_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "�b���H�Ҧ����ֿn���o10���ӧQ";
    }

    public void CloseButton2_OnClick()
    {
        illustration.SetActive(false);
    }

    public void NextButton_OnClick()
    {
        Ach1.SetActive(!Ach1.activeSelf);
        Ach2.SetActive(!Ach2.activeSelf);
        Ach3.SetActive(!Ach3.activeSelf);
        Ach4.SetActive(!Ach4.activeSelf);
        Ach5.SetActive(!Ach5.activeSelf);
        Ach6.SetActive(!Ach6.activeSelf);
        Ach7.SetActive(!Ach7.activeSelf);
        Ach8.SetActive(!Ach8.activeSelf);
        Ach9.SetActive(!Ach9.activeSelf);
        Ach10.SetActive(!Ach10.activeSelf);
        Ach12.SetActive(!Ach12.activeSelf);
        Ach13.SetActive(!Ach13.activeSelf);
        Ach14.SetActive(!Ach14.activeSelf);
        Ach11.SetActive(!Ach11.activeSelf);
    }

}