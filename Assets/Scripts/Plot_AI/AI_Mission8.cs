using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�C�^�X�}�l���B�~�y����e�^�X��*20���u��ˮ`(�`�W�԰�)(2)
public class AI_Mission8 : MonoBehaviour
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
                if(game.Players[0].CurrentHP - game.Round * 20 >= 0)
                    game.Players[0].CurrentHP -= game.Round * 20;
                else {
                    game.Players[0].CurrentHP = 0;
                    game.EndGame = true;
                    game.Winner = false;
                }
            }
            if (game.Players[0].ProgramCounter != (ushort)preProgramCounter)
            {
                preProgramCounter = game.Players[0].ProgramCounter;

            }

        }
    }

}
