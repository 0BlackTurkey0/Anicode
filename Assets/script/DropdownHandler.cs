using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text CharactorText;
    [SerializeField] Text RankText;
    [SerializeField] Text TaskText;
    [SerializeField] GameObject CharactorDropDown;
    [SerializeField] GameObject CharactorAttribute;
    [SerializeField] GameObject RankDropDown;
    [SerializeField] GameObject TaskDropDown;
    [SerializeField] GameObject ChooseTask;
    void Start()
    {
        //TextBox.text = " ";
        var charactordropdown = CharactorDropDown.transform.GetComponent<Dropdown>();
        charactordropdown.options.Clear();
        List<string> Charactoritems = new List<string>();
        Charactoritems.Add("狐狸");
        Charactoritems.Add("無尾熊");
        Charactoritems.Add("袋鼠");
        Charactoritems.Add("貓頭鷹");
        Charactoritems.Add("鯨魚");
        foreach(var Charactoritem in Charactoritems)
        {
            charactordropdown.options.Add(new Dropdown.OptionData(){ text = Charactoritem });
        }
        CharactorDropdownItemSelected(charactordropdown);
        charactordropdown.onValueChanged.AddListener(delegate{ CharactorDropdownItemSelected(charactordropdown);});



        var rankdropdown = RankDropDown.transform.GetComponent<Dropdown>();
        rankdropdown.options.Clear();
        List<string> Rankitems = new List<string>();
        Rankitems.Add("入門");
        Rankitems.Add("簡單");
        Rankitems.Add("普通");
        Rankitems.Add("困難");
        foreach(var Rankitem in Rankitems)
        {
            rankdropdown.options.Add(new Dropdown.OptionData(){ text = Rankitem });
        }
        RankDropdownItemSelected(rankdropdown);
        rankdropdown.onValueChanged.AddListener(delegate{ RankDropdownItemSelected(rankdropdown);});



        var taskdropdown = TaskDropDown.transform.GetComponent<Dropdown>();
        taskdropdown.options.Clear();
        List<string> Taskitems = new List<string>();
        Taskitems.Add("由小到大排序'");
        Taskitems.Add("LCD");
        Taskitems.Add("GCD");
        Taskitems.Add("etc");
        foreach(var Taskitem in Taskitems)
        {
            taskdropdown.options.Add(new Dropdown.OptionData(){ text = Taskitem });
        }
        TaskDropdownItemSelected(taskdropdown);
        taskdropdown.onValueChanged.AddListener(delegate{ TaskDropdownItemSelected(taskdropdown);});

        ChooseTask.SetActive(false);
        TaskDropDown.SetActive(false);
    }


    public void CharactorDropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        CharactorText.text = dropdown.options[index].text;
        if(CharactorText.text == "狐狸")
        {
            CharactorAttribute.transform.GetChild(0).gameObject.SetActive(true);
            CharactorAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(4).gameObject.SetActive(false);
        }
        else if (CharactorText.text =="無尾熊")
        {
            CharactorAttribute.transform.GetChild(1).gameObject.SetActive(true);
            CharactorAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(4).gameObject.SetActive(false);
        }
        else if (CharactorText.text=="袋鼠")
        {
            CharactorAttribute.transform.GetChild(2).gameObject.SetActive(true);
            CharactorAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(4).gameObject.SetActive(false);
        }
        else if (CharactorText.text=="鯨魚")
        {
            CharactorAttribute.transform.GetChild(3).gameObject.SetActive(true);
            CharactorAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(0).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(4).gameObject.SetActive(false);
        }
        else if (CharactorText.text=="貓頭鷹")
        {
            CharactorAttribute.transform.GetChild(4).gameObject.SetActive(true);
            CharactorAttribute.transform.GetChild(1).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(2).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(3).gameObject.SetActive(false);
            CharactorAttribute.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void RankDropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        RankText.text = dropdown.options[index].text;
        if(RankText.text == "困難")
        {
            ChooseTask.SetActive(true);
            TaskDropDown.SetActive(true);
        }
    }
    public void TaskDropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        TaskText.text = dropdown.options[index].text;
    }
}
