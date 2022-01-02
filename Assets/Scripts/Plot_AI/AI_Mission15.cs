using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//5回合內完成swap 4次(4)
public class AI_Mission15 : MonoBehaviour
{
    private Game game;
    private bool preStageBattle = true;
    private int preProgramCounter = -1;
    private bool Mission1 = false;
    private int cnt = 0;

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
        if (!game.IsBattle) {
            if (preStageBattle) {
                WinCheck();
                AI_add_code();
                preStageBattle = false;
            }
        }
        else {
            if (!preStageBattle) preStageBattle = true;
            if (game.Players[0].ProgramCounter != (ushort)preProgramCounter) {
                preProgramCounter = game.Players[0].ProgramCounter;

                Check();
            }

        }
    }

    private void WinCheck()
    {
        if (Mission1) {
            Debug.Log("You win");
            game.EndGame = true;
            game.Winner = true;
        }
        else if (game.Round == 6) {
            Debug.Log("You lose");
            game.EndGame = true;
            game.Winner = false;
        }
    }
    private void Check()
    {
        if (game.Players[0].Code[(ushort)preProgramCounter] != null) {
            if (game.Players[0].Code[(ushort)preProgramCounter].Type == InstructionType.Swap) {
                cnt++;
                if (cnt > 3) {
                    Mission1 = true;
                }
            }
        }

    }

}
