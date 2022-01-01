using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//完成assign，if各2次(2)
public class AI_Mission6 : MonoBehaviour
{
    private Game game;
    private bool preStageBattle = true;
    private int preProgramCounter = -1;
    private bool Mission1 = false;
    private bool Mission2 = false;
    private int cnt_if = 0;
    private int cnt_assign = 0;

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
        if (Mission1 && Mission2)
        {
            Debug.Log("You win");
            game.EndGame = true;
            game.Winner = true;
        }
    }
    private void Check()
    {
        if (game.Players[0].Code[(ushort)preProgramCounter] != null) {
            if (game.Players[0].Code[(ushort)preProgramCounter].Type == InstructionType.If) {
                cnt_if++;
                if (cnt_if > 1) {
                    Mission1 = true;
                }
            }
        }

        if (game.Players[0].Code[(ushort)preProgramCounter] != null) {
            if (game.Players[0].Code[(ushort)preProgramCounter].Type == InstructionType.Assign) {
                cnt_assign++;
                if (cnt_assign > 1) {
                    Mission2 = true;
                }
            }
        }
    }

}
