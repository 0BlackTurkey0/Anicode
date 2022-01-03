using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlStatus : MonoBehaviour
{
    [SerializeField] Text PlayerName;
    [SerializeField] Text MoneyText;
    [SerializeField] Text LevelText;

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
}
