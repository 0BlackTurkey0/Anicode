using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private Game game;
    private bool endBattle = true;

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
            if (endBattle)
            {
                AI_add_code();
                endBattle = false;
            }
        }
        else
        {
            check();
        }
    }

    private void check()
    {
        ushort size = game.Players[1].Code.Size;
        for(ushort i = 0; i < size; i++)
        {
            if (game.Players[0].Code[i].Type == InstructionType.Move)
            {
                game.EndGame = true;
                game.Winner = false;
            }
        }
    }

}
