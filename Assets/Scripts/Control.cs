using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
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
         Ach11.SetActive(false);
         Ach12.SetActive(false);
         Ach13.SetActive(false);
         Ach14.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Ach1_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "在單人模式中完成入門難度晉級賽"; 
    }
    public void Ach2_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "在單人模式中完成簡單難度晉級賽";
    }
    public void Ach3_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "在單人模式中完成普通難度晉級賽";
    }
    public void Ach4_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "在單人模式中完成困難難度晉級賽";
    }
    public void Ach5_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "在雙人模式中的一回合內執行4次以上的迴圈(Loop)指令";
    }
    public void Ach6_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "在雙人模式中的一回合內執行8次以上的條件(If...Else...)指令";
    }
    public void Ach7_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "在雙人模式中的一回合內造成100點以上的傷害";
    }
    public void Ach8_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "在雙人模式中的一回合內執行15次以上的算數運算子";
    }
    public void Ach9_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "在雙人模式中的一回合內執行25次以上的邏輯或關係運算子";
    }
    public void Ach10_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "在雙人模式中的一回合內執行7種以上的特殊指令";
    }
    public void Ach11_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "在雙人模式中回合數達到8以上";
    }
    public void Ach12_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "在雙人模式中完成題目";
    }
    public void Ach13_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "在雙人模式中取得勝利";
    }
    public void Ach14_Show()
    {
        Text temp = illustration_text.transform.GetComponent<Text>();
        illustration.SetActive(true);
        temp.text = "在雙人模式中累積取得10次勝利";
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