﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalStore : MonoBehaviour
{
    [SerializeField] GameObject ConfirmSurface;
    [SerializeField] GameObject FailSurface;
    [SerializeField] GameObject Content;
    [SerializeField] GameObject Chosen;
    private int Money;
    private int Price = 100;
    private int index;

    private ApplicationHandler applicationHandler;

    void Awake()
    {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Money = applicationHandler.GameData.Money;
        for(int i=0; i<34; i++)
        {
            if (applicationHandler.GameData.Items[i])
            {
                Chosen = Content.transform.GetChild(i).gameObject;
                Chosen.transform.GetChild(0).gameObject.SetActive(false);
                Chosen.transform.GetChild(1).gameObject.SetActive(false);
                Chosen.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }     

    // Update is called once per frame
    void Update()
    {
    }
    public void Btn_OnClick(int index_tmp)
    {
        index = index_tmp;
        Chosen = Content.transform.GetChild(index).gameObject;
        ConfirmSurface.SetActive(true);
    }
    public void FailBtn_OnClick()
    {
        FailSurface.SetActive(false);
    }
    public void ConfirmBtn_OnClick()
    {
        if (Money > Price)
        {
            Debug.Log("confirm");
            ConfirmSurface.SetActive(false);
            Chosen.gameObject.GetComponent<Button>().enabled = false;
            Chosen.transform.GetChild(0).gameObject.SetActive(false);
            Chosen.transform.GetChild(1).gameObject.SetActive(false);
            Chosen.transform.GetChild(2).gameObject.SetActive(true);
            applicationHandler.GameData.Money -= Price;
            applicationHandler.GameData.Items[index] = true;
            applicationHandler.GameData.SaveData();
        }
        else
        {
            Debug.Log("fail");
            FailSurface.SetActive(true);
            ConfirmSurface.SetActive(false);
        }
    }
}