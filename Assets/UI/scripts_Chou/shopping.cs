using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class shopping : MonoBehaviour
{
    public GameObject kangarooframe;
    public GameObject kangaroo_icon;
   // public GameObject big-image-frame;

    // Start is called before the first frame update
    void Start()
    {
        kangaroo_icon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {    

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.ReadDefaultValue().x,Mouse.current.position.ReadDefaultValue().y,0));   
            RaycastHit hit;   
            if (Physics.Raycast(ray, out hit))  
            {
                if (hit.collider.gameObject.name == "previex-button")   
                {
                    kangaroo_icon.SetActive(true);
                }

            }
        }
    }

   

}
