using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Mission2_2 : MonoBehaviour
{
    private Game game;
    private bool endBattle = true;
    private int preProgramCounter = -1;
    private bool Mission1 = false;

    private void Start()
    {
        game = GameObject.Find("GameHandler").gameObject.GetComponent<Game>();
    }
    private void AI_add_code()
    {
        game.Players[1].Code.Insert(InstructionType.Move, 0, 0, new int[1] { 3 });
        game.Players[1].Code.Insert(InstructionType.Move, 0, 0, new int[1] { 1 });
    }

    private void Update()
    {
        if (!game.IsBattle)
        {
            if (endBattle)
            {
                AI_add_code();
                endBattle = false;
            }
        }
        else
        {
            if (game.Players[0].ProgramCounter != (ushort)preProgramCounter)
            {
                preProgramCounter = game.Players[0].ProgramCounter;
                Check();
            }

        }
    }

    private void Check()
    {
        if (game.Players[0].Code[(ushort)preProgramCounter] != null)
        {
            if (game.Players[0].Code[(ushort)preProgramCounter].Type == InstructionType.Assign)
            {
                //assign v1+v2=v3
                if (game.Players[0].Code[(ushort)preProgramCounter].Arguments[0] == 0 && game.Players[0].Code[(ushort)preProgramCounter].Arguments[1] == 1 && game.Players[0].Code[(ushort)preProgramCounter].Arguments[2] == 2 && game.Players[0].Code[(ushort)preProgramCounter].Arguments[4] == 2)
                    Mission1 = true;
            }
        }

        Debug.Log(Mission1);

        if (Mission1)
        {
            Debug.Log("You win");
            game.EndGame = true;
            game.Winner = false;
        }

        if (game.Round == 4)
        {
            Debug.Log("You lose");
            game.EndGame = true;
            game.Winner = true;
        }
    }
}
