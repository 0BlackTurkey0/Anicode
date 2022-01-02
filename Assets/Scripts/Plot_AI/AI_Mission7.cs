using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//assign 5個介於雙方血量數值之間的不同數(2)
public class AI_Mission7 : MonoBehaviour
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
        if (game.Round == 1) {
            game.Players[1].Code.Insert(InstructionType.If, 0, 0, new int[4] { 0, 0, 0, 1 });
            game.Players[1].Code.Insert(InstructionType.Move, 1, 1, new int[1] { 1 });
            game.Players[1].Code.Insert(InstructionType.Move, 2, 1, new int[1] { 2 });
        }
        else {
            if (UnityEngine.Random.Range(0, 2) == 0) {
                game.Players[1].Code.Insert(InstructionType.If, (ushort)(game.Players[1].Code.Size + 1), 0, new int[4] { 0, 0, 0, 1 });
                game.Players[1].Code.Insert(InstructionType.Move, (ushort)(game.Players[1].Code.Size + 1), 1, new int[1] { UnityEngine.Random.Range(0, 4) });
                game.Players[1].Code.Insert(InstructionType.Move, (ushort)(game.Players[1].Code.Size + 1), 1, new int[1] { UnityEngine.Random.Range(0, 4) });
            }
            else {
                game.Players[1].Code.Insert(InstructionType.If, (ushort)(game.Players[1].Code.Size + 1), 0, new int[4] { 1, 0, 0, 1 });
                game.Players[1].Code.Insert(InstructionType.Attack, (ushort)(game.Players[1].Code.Size + 1), 1);
            }
        }
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
        if (Mission1)
        {
            Debug.Log("You win");
            game.EndGame = true;
            game.Winner = true;
        }
    }
    private void Check()
    {
        bool flag = true;
        if (game.Players[0].Code[(ushort)preProgramCounter] != null) {
            for (int i = 0;i < 5;i++) {
                if (!(game.Players[0].Variable[i] >= Math.Min(game.Players[0].CurrentHP, game.Players[1].CurrentHP) && game.Players[0].Variable[i] <= Math.Max(game.Players[0].CurrentHP, game.Players[1].CurrentHP))) {
                    flag = false;
                    break;
                }
                for (int j = 0;j < i;j++) {
                    if (game.Players[0].Variable[i] == game.Players[0].Variable[j]) {
                        flag = false;
                        break;
                    }
                }
            }
            if (flag)
                Mission1 = true;
        }
    }
}


















    