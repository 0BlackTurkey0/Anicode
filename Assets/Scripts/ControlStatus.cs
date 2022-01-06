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
    [SerializeField] GameObject ConfirmSurface;
    [SerializeField] GameObject ConfirmBtn;
    private int Money;
    private int Price;

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
        Money = applicationHandler.GameData.Money;

        MoneyText.text = applicationHandler.GameData.Money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BananaBtn_OnClick()
    {
        Price = 100;
    }
    public void GrapeBtn_OnClick()
    {

        Price = 100;
    }
    public void CherryBtn_OnClick()
    {

        Price = 100;
    }
    public void AvocadoBtn_OnClick()
    {
        Price = 100;
    }
    public void PearBtn_OnClick()
    {
        Price = 100;
    }
    public void OrangeBtn_OnClick()
    {
        Price = 100;
    }
    public void StrawberryBtn_OnClick()
    {
        Price = 100;
    }
    public void TomatoBtn_OnClick()
    {
        Price = 100;
    }
    public void BaconBtn_OnClick()
    {
        Price = 100;
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
        Price = 100;
        Purchase(20);
    }
    public void ConfirmBtn_OnClick(int num, int price)
    {

        applicationHandler.GameData.
    }
    public void Purchase(int num , int price)
    {
        ConfirmSurface.SetActive(true);
        ConfirmBtn.OnClick(num , price);
    }
}
