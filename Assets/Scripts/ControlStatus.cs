using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlStatus : MonoBehaviour
{
    [SerializeField] Text PlayerName;
    [SerializeField] Text MoneyText;
    [SerializeField] Text LevelText;
    [SerializeField] GameObject BananaBtn;
    [SerializeField] GameObject GrapeBtn;
    [SerializeField] GameObject CherryBtn;
    [SerializeField] GameObject AvocadoBtn;
    [SerializeField] GameObject PearBtn;
    [SerializeField] GameObject OrangeBtn;
    [SerializeField] GameObject StrawberryBtn;
    [SerializeField] GameObject TomatoBtn;
    [SerializeField] GameObject BaconBtn;
    [SerializeField] GameObject ChickenBtn;
    [SerializeField] GameObject TroutBtn;
    [SerializeField] GameObject FishBtn;
    [SerializeField] GameObject BlowfishBtn;
    [SerializeField] GameObject MeatBtn;
    [SerializeField] GameObject SchnitzelBtn;
    [SerializeField] GameObject HamBtn;
    [SerializeField] GameObject JemBtn;
    [SerializeField] GameObject PotionBtn;
    [SerializeField] GameObject ScrollBtn;
    [SerializeField] GameObject SwordBtn;
    [SerializeField] GameObject HerbBtn;

    private ApplicationHandler applicationHandler;

    void Awake()
    {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerName.text = applicationHandler.GameData.Name;
        LevelText.text = "���� : ";
        switch (applicationHandler.GameData.Rank)
        {
            case DifficultyType.NULL:
                LevelText.text += "�L";
                break;
            case DifficultyType.Start:
                LevelText.text += "�J��";
                break;
            case DifficultyType.Easy:
                LevelText.text += "²��";
                break;
            case DifficultyType.Normal:
                LevelText.text += "���q";
                break;
            case DifficultyType.Hard:
                LevelText.text += "�x��";
                break;
        }

        MoneyText.text = applicationHandler.GameData.Money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BananaBtn_OnClick()
    {

    }
    public void GrapeBtn_OnClick()
    {
    }
    public void CherryBtn_OnClick()
    {
    }
    public void AvocadoBtn_OnClick()
    {
    }
    public void PearBtn_OnClick()
    {
    }
    public void OrangeBtn_OnClick()
    {
    }
    public void StrawberryBtn_OnClick()
    {
    }
    public void TomatoBtn_OnClick()
    {
    }
    public void BaconBtn_OnClick()
    {
    }
    public void ChickenBtn_OnClick()
    {
    }
    public void TroutBtn_OnClick()
    {
    }
    public void FishBtn_OnClick()
    {
    }
    public void BlowfishBtn_OnClick()
    {
    }
    public void MeatBtn_OnClick()
    {
    }
    public void SchnitzelBtn_OnClick()
    {
    }
    public void HamBtn_OnClick()
    {
    }
    public void JemBtn_OnClick()
    {
    }
    public void PotionBtn_OnClick()
    {
    }
    public void ScrollBtn_OnClick()
    {
    }
    public void SwordBtn_OnClick()
    {
    }
    public void HerbBtn_OnClick()
    {
    }
    public void 
}
