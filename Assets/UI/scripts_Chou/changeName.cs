using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeName : MonoBehaviour
{
    public Text playerName;
    public void EnterPlayerName(Text enterText)
    {
        playerName.text = enterText.text;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     /*   if (Input.GetKeyDown("space"))
        {  
            GetComponent<Text>().text = "" + System.Console.ReadLine();    
        }*/
    }
}
