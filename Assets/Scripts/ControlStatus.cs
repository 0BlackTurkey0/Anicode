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
        LevelText.text = "階級 : ";
        switch (applicationHandler.GameData.Rank)
        {
            case DifficultyType.NULL:
                LevelText.text += "無";
                break;
            case DifficultyType.Start:
                LevelText.text += "入門";
                break;
            case DifficultyType.Easy:
                LevelText.text += "簡單";
                break;
            case DifficultyType.Normal:
                LevelText.text += "普通";
                break;
            case DifficultyType.Hard:
                LevelText.text += "困難";
                break;
        }

        MoneyText.text = applicationHandler.GameData.Money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
