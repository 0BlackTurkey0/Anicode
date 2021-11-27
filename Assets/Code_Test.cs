using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code_Test : MonoBehaviour
{
    public int tmp;
    public bool con;
    Code test;
    // Start is called before the first frame update
    void Start()
    {
        test = new Code();
        con = true;
        Debug.Log("Start Test");
        test.Insert(InstructionType.Assign, 5, 3);
        test.Insert(InstructionType.Attack, 8, 6);
        test.Insert(InstructionType.Loop, 2, 0, new int[2] { 2, 2 });
        test.Insert(InstructionType.Loop, 3, 1, new int[2] { 5, 5 });
        test.Insert(InstructionType.If, 4, 3);
        test.Insert(InstructionType.Assign, 9, 2);
        test.Insert(InstructionType.Attack, 6, 0);
        test.Display();
        /*
        test.Delete(2);
        test.Display();

        test.Delete(1);
        test.Display();

        test.Delete(2);
        test.Display();

        test.Insert(InstructionType.Attack, 2, 1);
        test.Display();
        */
    }

    // Update is called once per frame
    void Update()
    {
        if ((tmp = test.Next(con)) >= 0)
        {
            Instruction target = test.GetInstruction((ushort)tmp);
            Debug.Log(tmp.ToString() + ":" + target.GetInstuctionType().ToString());
            if (target.GetInstuctionType() == InstructionType.Loop)
                if (target.Arguments[1] == 0)
                {
                    target.Arguments[1] = target.Arguments[0];
                    con = false;
                }
                else
                {
                    target.Arguments[1]--;
                    con = true;
                }
            else con = true;
        }
    }
}
