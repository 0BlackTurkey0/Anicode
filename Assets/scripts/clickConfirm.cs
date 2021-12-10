using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickConfirm : MonoBehaviour
{
    public GameObject confirmButton;
    

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickConfirmButton(int a)
    {
        confirmButton.SetActive(true);
        confirmButton.GetComponent<selectedBuy>().label = a;
       
    }

    
}
