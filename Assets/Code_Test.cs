using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code_Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Test");
        Code test = new Code();
        test.Insert(InstructionType.Assign, 5, 3);
        test.Insert(InstructionType.Attack, 8, 6);
        test.Insert(InstructionType.Loop, 2, 0);
        test.Insert(InstructionType.If, 3, 3);
        test.Insert(InstructionType.Assign, 9, 2);
        test.Insert(InstructionType.Attack, 5, 0);
        test.Display();

        test.Delete(2);
        test.Display();

        test.Delete(1);
        test.Display();

        test.Delete(2);
        test.Display();

        test.Insert(InstructionType.Attack, 2, 1);
        test.Display();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
