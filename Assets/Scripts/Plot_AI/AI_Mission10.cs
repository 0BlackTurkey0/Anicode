using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//3回合內使用雙層loop完成一次行動
public class AI_Mission10:MonoBehaviour
{
    private Game game;
    private bool preStageBattle = true;
    private int preProgramCounter = -1;
    private bool Mission1 = false;
    private bool Mission2 = false;
    private int loop_Level1 = 0;
    private int loop_Level2 = 0;
    private bool loop_Flag1 = false;
    private bool loop_Flag2 = false;

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

        else if (game.Round == 4) {
            Debug.Log("You lose");
            game.EndGame = true;
            game.Winner = false;
        }
    }
    private void Check()
    {
        if (game.Players[0].Code[(ushort)preProgramCounter] != null) {
            if (game.Players[0].Code[(ushort)preProgramCounter].Type == InstructionType.Loop && loop_Flag1 == false) {
                loop_Flag1 = true;
                loop_Level1 = game.Players[0].Code.GetLevel((ushort)preProgramCounter);
            }
        }

        if (game.Players[0].Code[(ushort)preProgramCounter] != null) {
            if (game.Players[0].Code[(ushort)preProgramCounter].Type == InstructionType.Loop && loop_Flag1 == true) {
                if (loop_Level2 > loop_Level1) {
                    loop_Flag2 = true;
                    loop_Level2 = game.Players[0].Code.GetLevel((ushort)preProgramCounter);
                }
                else {
                    loop_Flag1 = false;
                }
            }
        }
        if (game.Players[0].Code[(ushort)preProgramCounter] != null) {
            if (loop_Flag1 && loop_Flag2) {
                if (game.Players[0].Code.GetLevel((ushort)preProgramCounter) > loop_Level2)
                    Mission1 = true;
                else if (game.Players[0].Code.GetLevel((ushort)preProgramCounter) <= loop_Level1) {
                    loop_Flag1 = false;
                    loop_Flag2 = false;
                }
                else {
                    loop_Flag2 = false;
                }
            }
        }
    }
}