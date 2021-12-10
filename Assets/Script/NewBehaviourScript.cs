using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 0.1f;

    void Start()
    {
        print("Start");
    }

    void FixedUpdate()
    {
        Movement();
    }
  
    void Movement()
    {
        float facedirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.gameObject.transform.position += new Vector3(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.gameObject.transform.position -= new Vector3(speed, 0, 0);
        }
        if (facedirection != 0)
         {
             transform.localScale = new Vector3(facedirection*6, 9, 1);
         }
    }
}
