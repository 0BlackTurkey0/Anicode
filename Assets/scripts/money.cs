using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class money : MonoBehaviour
{
    int Money = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Money++;
            GetComponent<Text>().text = "" + Money;
        }
    }
}
