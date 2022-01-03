using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Mission1_5 : MonoBehaviour
{
    private Game game;
    private bool preStageBattle = true;
    private int preProgramCounter = -1;
    private bool Mission1 = false;

    private void Start()
    {
        game = GameObject.Find("GameHandler").gameObject.GetComponent<Game>();
    }
    private void AI_add_code()
    {
        game.Players[1].Code.Insert(InstructionType.Move, 0, 0, new int[1] { 0 });
    }

    private void Update()
    {
        Debug.Log(game.Round);
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
            if (!preStageBattle) preStageBattle = true;
            if (game.Players[0].ProgramCounter != (ushort)preProgramCounter)
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
        //Attack¦¨¥\
        if (game.Players[0].Code[(ushort)preProgramCounter] != null)
        {
            if (game.Players[0].Code[(ushort)preProgramCounter].Type == InstructionType.Attack)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (game.Neighbor[game.Players[0].Pos, i] == game.Players[1].Pos)
                    {
                        Mission1 = true;

                        break;
                    }
                }
            }
        }
           
        Debug.Log(Mission1);
    }
}
