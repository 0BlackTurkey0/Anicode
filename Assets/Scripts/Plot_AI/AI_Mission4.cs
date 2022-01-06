using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Boss:每回合恢復100HP(常規戰鬥)(1)
public class AI_Mission4 : MonoBehaviour
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
            game.Players[1].Code.Insert(InstructionType.Move, 0, 0, new int[1] { 1 });
            game.Players[1].Code.Insert(InstructionType.Move, 1, 0, new int[1] { 2 });
        }
        else {
            if (game.Round % 2 == 0) {
                if (Random.Range(0, 2) == 0) {
                    game.Players[1].Code.Insert(InstructionType.Move, (ushort)Random.Range(0, game.Players[1].Code.Size + 1), 0, new int[1] { Random.Range(0, 4) });
                    game.Players[1].Code.Insert(InstructionType.Move, (ushort)Random.Range(0, game.Players[1].Code.Size + 1), 0, new int[1] { Random.Range(0, 4) });
                    game.Players[1].Code.Insert(InstructionType.Move, (ushort)Random.Range(0, game.Players[1].Code.Size + 1), 0, new int[1] { Random.Range(0, 4) });
                }
                else {
                    game.Players[1].Code.Insert(InstructionType.Move, (ushort)Random.Range(0, game.Players[1].Code.Size + 1), 0, new int[1] { Random.Range(0, 4) });
                    game.Players[1].Code.Insert(InstructionType.Attack, (ushort)Random.Range(0, game.Players[1].Code.Size + 1), 0);
                }
            }
            else {
                if (Random.Range(0, 2) == 0) {
                    game.Players[1].Code.Insert(InstructionType.Move, (ushort)Random.Range(0, game.Players[1].Code.Size + 1), 0, new int[1] { Random.Range(0, 4) });
                    game.Players[1].Code.Insert(InstructionType.Move, (ushort)Random.Range(0, game.Players[1].Code.Size + 1), 0, new int[1] { Random.Range(0, 4) });
                }
                else {
                    game.Players[1].Code.Insert(InstructionType.Attack, (ushort)Random.Range(0, game.Players[1].Code.Size + 1), 0);
                }
            }
        }
    }

    private void Update()
    {
        if (!game.IsBattle)
        {
            if (preStageBattle)
            {
                AI_add_code();
                preStageBattle = false;
            }
        }
        else
        {
            if (!preStageBattle) {
                preStageBattle = true;
                preProgramCounter = -1;
                if (game.Players[1].CurrentHP + 100 <= game.Players[1].Hp)
                    game.Players[1].CurrentHP += 100;
                else
                    game.Players[1].CurrentHP = game.Players[1].Hp;
            }
            if (game.Players[0].ProgramCounter != (ushort)preProgramCounter)
            {
                preProgramCounter = game.Players[0].ProgramCounter;

            }

        }
    }
}
