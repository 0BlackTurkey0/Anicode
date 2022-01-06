using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Mission2_1 : MonoBehaviour
{
    private Game game;
    private bool preStageBattle = true;
    private int preProgramCounter = -1;
    private bool Mission1 = false;

    private void OnEnable()
    {
        game = GameObject.Find("GameHandler").gameObject.GetComponent<Game>();
    }
    private void AI_add_code()
    {
        game.Players[1].Code.Insert(InstructionType.Move, 0, 0, new int[1] { 0 });
        game.Players[1].Code.Insert(InstructionType.Move, 0, 0, new int[1] { 2 });
    }

    private void Update()
    {
        if (!game.IsBattle)
        {
            if (preStageBattle)
            {
                WinCheck();
                AI_add_code();
                preStageBattle = false;
            }
        }
        else
        {
            if (!preStageBattle)
            {
                preStageBattle = true;
                preProgramCounter = -1;
            }
            if (game.Players[0].ProgramCounter != preProgramCounter)
            {
                preProgramCounter = game.Players[0].ProgramCounter;
                Check();
            }

        }
    }

    private void WinCheck()
    {
        if (Mission1)
        {
            Debug.Log("You win");
            game.EndGame = true;
            game.Winner = true;
        }

        else if (game.Round == 4)
        {
            Debug.Log("You lose");
            game.EndGame = true;
            game.Winner = false;
        }
    }

    private void Check()
    {
        if (game.Players[0].Code[(ushort)preProgramCounter] != null)
        {
            if (game.Players[0].Code[(ushort)preProgramCounter].Type == InstructionType.Assign)
            {
                //assign 0+C=v1
                if (game.Players[0].Code[(ushort)preProgramCounter].Arguments[0] == 0 && game.Players[0].Code[(ushort)preProgramCounter].Arguments[1] == 0 && game.Players[0].Code[(ushort)preProgramCounter].Arguments[2] == 0 && game.Players[0].Code[(ushort)preProgramCounter].Arguments[4] == 0)
                    Mission1 = true;
            }
        }
        Debug.Log(Mission1);
    }
}
