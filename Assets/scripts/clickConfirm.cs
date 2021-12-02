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
        this.GetComponent<Button>().onClick.AddListener(clickConfirmButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void clickConfirmButton()
    {
        confirmButton.SetActive(true);
        confirmButton.GetComponent<selectedBuy>().label = 0;
    }
}
