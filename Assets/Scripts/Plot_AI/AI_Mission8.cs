using UnityEngine;
//每回合開始時額外造成當前回合數*20的真實傷害(常規戰鬥)(2)
public class AI_Mission8 : MonoBehaviour {
    private Game game;
    private bool preStageBattle = true;
    private int preProgramCounter = -1;

    private void OnEnable() {
        game = GameObject.Find("GameHandler").GetComponent<Game>();
    }

    private void AI_add_code() {
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

    private void Update() {
        if (!game.IsBattle) {
            if (preStageBattle) {
                AI_add_code();
                preStageBattle = false;
            }
        }
        else {
            if (!preStageBattle) {
                preStageBattle = true;
                preProgramCounter = -1;
                if (game.Players[0].CurrentHP - game.Round * 20 >= 0)
                    game.Players[0].CurrentHP -= game.Round * 20;
                else {
                    game.Players[0].CurrentHP = 0;
                    game.EndGame = true;
                    game.Winner = false;
                }
            }
            if (game.Players[0].ProgramCounter != (ushort)preProgramCounter) {
                preProgramCounter = game.Players[0].ProgramCounter;
            }
        }
    }
}