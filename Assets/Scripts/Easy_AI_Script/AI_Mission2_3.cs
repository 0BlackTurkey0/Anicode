using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Mission2_3 : MonoBehaviour
{
    private Game game;
    private bool preStageBattle = true;
    private int preProgramCounter = -1;
    private bool Mission1 = false;
    private bool Mission2 = false;

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
            if (preStageBattle)
            {
                if (Mission1 && Mission2)
                {
                    Debug.Log("You win");
                    game.EndGame = true;
                    game.Winner = false;
                }

                else if (game.Round == 4)
                {
                    Debug.Log("You lose");
                    game.EndGame = true;
                    game.Winner = true;
                }
                AI_add_code();
                preStageBattle = false;
            }
        }
        else
        {
            if (!preStageBattle) preStageBattle = true;
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
            if (game.Players[0].Code[(ushort)preProgramCounter].Type == InstructionType.If)
            {
                //assign 我方生命 < C
                if (game.Players[0].Code[(ushort)preProgramCounter].Arguments[0] == 3 && game.Players[0].Code[(ushort)preProgramCounter].Arguments[1] == 1 && game.Players[0].Code[(ushort)preProgramCounter].Arguments[2] == 0)
                    Mission1 = true;
            }
            if (game.Players[0].Code[(ushort)preProgramCounter].Type == InstructionType.Assign)
            {
                //assign v2 + C =  v2
                if (game.Players[0].Code[(ushort)preProgramCounter].Arguments[0] == 0 && game.Players[0].Code[(ushort)preProgramCounter].Arguments[1] == 2 && game.Players[0].Code[(ushort)preProgramCounter].Arguments[2] == 0 && game.Players[0].Code[(ushort)preProgramCounter].Arguments[4] == 1)
                    Mission2 = true;
            }
        }

        Debug.Log("Mission1 : ");
        Debug.Log(Mission1);
        Debug.Log("Mission2 : ");
        Debug.Log(Mission2);
    }
}
