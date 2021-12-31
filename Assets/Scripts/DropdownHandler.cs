using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    [SerializeField] Text CharactorText;
    [SerializeField] Text RankText;
    [SerializeField] GameObject CharactorDropDown;
    [SerializeField] GameObject CharactorAttribute;
    [SerializeField] GameObject EnteranceToggle;
    [SerializeField] GameObject EasyToggle;
    [SerializeField] GameObject NormalToggle;
    [SerializeField] GameObject DifficultToggle;
    [SerializeField] GameObject Attribute;
    [SerializeField] GameObject ConfirmBtn;

    // Start is called before the first frame update
    void Start()
    {
        var charactordropdown = CharactorDropDown.transform.GetComponent<Dropdown>();
        charactordropdown.options.Clear();
        List<string> Charactoritems = new List<string>();
        Charactoritems.Add("狐狸");
        Charactoritems.Add("無尾熊");
        Charactoritems.Add("袋鼠");
        Charactoritems.Add("貓頭鷹");
        Charactoritems.Add("鯨魚");
        foreach (var Charactoritem in Charactoritems) {
            charactordropdown.options.Add(new Dropdown.OptionData() { text = Charactoritem });
        }
        CharactorDropdownItemSelected(charactordropdown);
        charactordropdown.onValueChanged.AddListener(delegate { CharactorDropdownItemSelected(charactordropdown); });

        EnteranceToggle.GetComponent<Toggle>().isOn = false;
        EasyToggle.GetComponent<Toggle>().isOn = false;
        NormalToggle.GetComponent<Toggle>().isOn = false;
        DifficultToggle.GetComponent<Toggle>().isOn = false;

    }
    void Update()
    {
        if (EnteranceToggle.GetComponent<Toggle>().isOn == false && EasyToggle.GetComponent<Toggle>().isOn == false && NormalToggle.GetComponent<Toggle>().isOn == false && DifficultToggle.GetComponent<Toggle>().isOn == false)
            ConfirmBtn.transform.GetComponent<Button>().enabled = false;
        else
            ConfirmBtn.transform.GetComponent<Button>().enabled = true;
    }
    public void EnteranceToggle_sOn()
    {
        if (EnteranceToggle.GetComponent<Toggle>().isOn == true) {
            CharactorAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(4).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(5).gameObject.SetActive(true);
            string content = Resources.Load<TextAsset>("Enterance_Text").ToString();
            Attribute.transform.GetChild(5).gameObject.GetComponent<Text>().text = content;
        }
    }
    public void EasyToggle_sOn()
    {
        if (EasyToggle.GetComponent<Toggle>().isOn == true) {
            CharactorAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(4).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(5).gameObject.SetActive(true);
            string content = Resources.Load<TextAsset>("Easy_Text").ToString();
            Attribute.transform.GetChild(5).gameObject.GetComponent<Text>().text = content;
        }
    }
    public void NormalToggle_sOn()
    {
        if (NormalToggle.GetComponent<Toggle>().isOn == true) {
            CharactorAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(4).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(5).gameObject.SetActive(true);
            string content = Resources.Load<TextAsset>("Normal_Text").ToString();
            Attribute.transform.GetChild(5).gameObject.GetComponent<Text>().text = content;
        }
    }
    public void DifficultToggle_sOn()
    {
        if (DifficultToggle.GetComponent<Toggle>().isOn == true) {
            CharactorAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(4).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(5).gameObject.SetActive(true);
            string content = Resources.Load<TextAsset>("Difficult_Text").ToString();
            Attribute.transform.GetChild(5).gameObject.GetComponent<Text>().text = content;
        }
    }

    public void CharactorDropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        CharactorText.text = dropdown.options[index].text;
        if (CharactorText.text == "狐狸") {
            CharactorAttribute.transform.GetChild(0).gameObject.SetActive(true);
            CharactorAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(4).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(5).gameObject.SetActive(false);
        }
        else if (CharactorText.text == "無尾熊") {
            CharactorAttribute.transform.GetChild(1).gameObject.SetActive(true);
            CharactorAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(4).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(5).gameObject.SetActive(false);
        }
        else if (CharactorText.text == "袋鼠") {
            CharactorAttribute.transform.GetChild(2).gameObject.SetActive(true);
            CharactorAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(4).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(5).gameObject.SetActive(false);
        }
        else if (CharactorText.text == "鯨魚") {
            CharactorAttribute.transform.GetChild(3).gameObject.SetActive(true);
            CharactorAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(5).gameObject.SetActive(false);
        }
        else if (CharactorText.text == "貓頭鷹") {
            CharactorAttribute.transform.GetChild(4).gameObject.SetActive(true);
            CharactorAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(5).gameObject.SetActive(false);
        }
    }

    public void Confirm()
    {
        bool[] difficulty = new bool[4];
        if (EnteranceToggle.GetComponent<Toggle>().isOn == true)
            difficulty[0] = true;
        if (EasyToggle.GetComponent<Toggle>().isOn == true)
            difficulty[1] = true;
        if (NormalToggle.GetComponent<Toggle>().isOn == true)
            difficulty[2] = true;
        if (DifficultToggle.GetComponent<Toggle>().isOn == true)
            difficulty[3] = true;
        Control_in_Twolobby.mode.Difficulty = difficulty;
        Control_in_Twolobby.mode.Character = CharactorText.text;
    }
}
