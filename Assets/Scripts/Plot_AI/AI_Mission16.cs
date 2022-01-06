using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Boss:敵人攻擊力隨生命值減少逐漸上升(常規戰鬥)(4)

public class AI_Mission16 : MonoBehaviour
{
    private Game game;
    private bool preStageBattle = true;
    private int preProgramCounter = -1;

    private void OnEnable()
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
            if (Random.Range(0, 2) == 0) {
                game.Players[1].Code.Insert(InstructionType.If, (ushort)(game.Players[1].Code.Size + 1), 0, new int[4] { 0, 0, 0, 1 });
                game.Players[1].Code.Insert(InstructionType.Move, (ushort)(game.Players[1].Code.Size + 1), 1, new int[1] { Random.Range(0, 4) });
                game.Players[1].Code.Insert(InstructionType.Move, (ushort)(game.Players[1].Code.Size + 1), 1, new int[1] { Random.Range(0, 4) });
            }
            else {
                game.Players[1].Code.Insert(InstructionType.If, (ushort)(game.Players[1].Code.Size + 1), 0, new int[4] { 1, 0, 0, 1 });
                game.Players[1].Code.Insert(InstructionType.Attack, (ushort)(game.Players[1].Code.Size + 1), 1);
            }
        }
    }

    private void Update()
    {
        if (!game.IsBattle) {
            if (preStageBattle) {
                AI_add_code();
                preStageBattle = false;
            }
        }
        else {
            if (!preStageBattle)
            {
                preStageBattle = true;
                preProgramCounter = -1;
            }
            if (game.Players[0].ProgramCounter != (ushort)preProgramCounter) {
                preProgramCounter = game.Players[0].ProgramCounter;

                Check();
            }

        }
    }

    private void Check()
    {
        if (game.Players[0].Code[(ushort)preProgramCounter] != null) {
            if (game.Players[1].Code[(ushort)preProgramCounter].Type == InstructionType.Attack) {
                if (game.Players[1].CurrentHP < game.Players[1].Hp / 2) {
                    if (game.Players[0].CurrentHP - (game.Players[1].Hp - game.Players[1].CurrentHP) / 5 >= 0)
                        game.Players[0].CurrentHP -= (game.Players[1].Hp - game.Players[1].CurrentHP) / 5;
                    else {
                        game.Players[0].CurrentHP = 0;
                        game.EndGame = true;
                        game.Winner = false;
                    }
                }
            }
        }
    }

}
