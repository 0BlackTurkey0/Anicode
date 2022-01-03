using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    [SerializeField] Text CharacterText;
    [SerializeField] GameObject CharacterDropDown;
    [SerializeField] GameObject CharacterAttribute;
    [SerializeField] GameObject EnteranceToggle;
    [SerializeField] GameObject EasyToggle;
    [SerializeField] GameObject NormalToggle;
    [SerializeField] GameObject DifficultToggle;
    [SerializeField] GameObject Attribute;
    [SerializeField] GameObject ConfirmBtn;
    [SerializeField] GameObject ChoosingRankHint;
    private int CharacterNum;
    private Network network;

    void Awake()
    {
        network = GameObject.Find("Network").GetComponent<Network>();
    }

    // Start is called before the first frame update
    void Start()
    {
        var characterdropdown = CharacterDropDown.transform.GetComponent<Dropdown>();
        characterdropdown.onValueChanged.AddListener(delegate { CharacterDropdownItemSelected(characterdropdown); });

        EnteranceToggle.GetComponent<Toggle>().isOn = false;
        EasyToggle.GetComponent<Toggle>().isOn = false;
        NormalToggle.GetComponent<Toggle>().isOn = false;
        DifficultToggle.GetComponent<Toggle>().isOn = false;

    }
    void Update()
    {
        if (EnteranceToggle.GetComponent<Toggle>().isOn == false && EasyToggle.GetComponent<Toggle>().isOn == false && NormalToggle.GetComponent<Toggle>().isOn == false && DifficultToggle.GetComponent<Toggle>().isOn == false)
        {
            ChoosingRankHint.SetActive(true);
            ConfirmBtn.transform.GetComponent<Button>().enabled = false;
        }
        else
        {
            ChoosingRankHint.SetActive(false);
            ConfirmBtn.transform.GetComponent<Button>().enabled = true;
        }
    }

    public void EnteranceToggle_sOn()
    {
        if (EnteranceToggle.GetComponent<Toggle>().isOn == true)
        {
            CharacterAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(4).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(5).gameObject.SetActive(true);
            CharacterAttribute.transform.GetChild(6).gameObject.SetActive(true);
            CharacterAttribute.transform.GetChild(7).gameObject.SetActive(true);
            CharacterAttribute.transform.GetChild(8).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(9).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(10).gameObject.SetActive(false);
            string content = Resources.Load<TextAsset>("Enterance_Text").ToString();
            Attribute.transform.GetChild(5).gameObject.GetComponent<Text>().text = content;
        }
    }

    public void EasyToggle_sOn()
    {
        if (EasyToggle.GetComponent<Toggle>().isOn == true)
        {
            CharacterAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(4).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(5).gameObject.SetActive(true);
            CharacterAttribute.transform.GetChild(6).gameObject.SetActive(true);
            CharacterAttribute.transform.GetChild(7).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(8).gameObject.SetActive(true);
            CharacterAttribute.transform.GetChild(9).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(10).gameObject.SetActive(false);
            string content = Resources.Load<TextAsset>("Easy_Text").ToString();
            Attribute.transform.GetChild(5).gameObject.GetComponent<Text>().text = content;
        }
    }

    public void NormalToggle_sOn()
    {
        if (NormalToggle.GetComponent<Toggle>().isOn == true)
        {
            CharacterAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(4).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(5).gameObject.SetActive(true);
            CharacterAttribute.transform.GetChild(6).gameObject.SetActive(true);
            CharacterAttribute.transform.GetChild(7).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(8).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(9).gameObject.SetActive(true);
            CharacterAttribute.transform.GetChild(10).gameObject.SetActive(false);
            string content = Resources.Load<TextAsset>("Normal_Text").ToString();
            Attribute.transform.GetChild(5).gameObject.GetComponent<Text>().text = content;
        }
    }
    public void DifficultToggle_sOn()
    {
        if (DifficultToggle.GetComponent<Toggle>().isOn == true)
        {
            CharacterAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(4).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(5).gameObject.SetActive(true);
            CharacterAttribute.transform.GetChild(6).gameObject.SetActive(true);
            CharacterAttribute.transform.GetChild(7).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(8).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(9).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(10).gameObject.SetActive(true);
            string content = Resources.Load<TextAsset>("Difficult_Text").ToString();
            Attribute.transform.GetChild(5).gameObject.GetComponent<Text>().text = content;
        }
    }

    public void CharacterDropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        CharacterNum = index;
        CharacterText.text = dropdown.options[index].text;
        if (CharacterText.text == "狐狸")
        {
            CharacterAttribute.transform.GetChild(0).gameObject.SetActive(true);
            CharacterAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(4).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(5).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(6).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(7).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(8).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(9).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(10).gameObject.SetActive(false);
        }
        else if (CharacterText.text == "無尾熊")
        {
            CharacterAttribute.transform.GetChild(1).gameObject.SetActive(true);
            CharacterAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(4).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(5).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(6).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(7).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(8).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(9).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(10).gameObject.SetActive(false);
        }
        else if (CharacterText.text == "袋鼠")
        {
            CharacterAttribute.transform.GetChild(2).gameObject.SetActive(true);
            CharacterAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(4).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(5).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(6).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(7).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(8).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(9).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(10).gameObject.SetActive(false);
        }
        else if (CharacterText.text == "鯨魚")
        {
            CharacterAttribute.transform.GetChild(3).gameObject.SetActive(true);
            CharacterAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(5).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(6).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(7).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(8).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(9).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(10).gameObject.SetActive(false);
        }
        else if (CharacterText.text == "貓頭鷹")
        {
            CharacterAttribute.transform.GetChild(4).gameObject.SetActive(true);
            CharacterAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(5).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(6).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(7).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(8).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(9).gameObject.SetActive(false);
            CharacterAttribute.transform.GetChild(10).gameObject.SetActive(false);
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
        GameMode mode = new GameMode
        {
            Difficulty = difficulty,
            Character = CharacterNum
        };
        network.SetMode(mode);
    }
}