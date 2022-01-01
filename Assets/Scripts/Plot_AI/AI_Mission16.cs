using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Boss:敵人攻擊力隨生命值減少逐漸上升(常規戰鬥)(4)

public class AI_Mission16 : MonoBehaviour
{
    private Game game;
    private bool preStageBattle = true;
    private int preProgramCounter = -1;

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
