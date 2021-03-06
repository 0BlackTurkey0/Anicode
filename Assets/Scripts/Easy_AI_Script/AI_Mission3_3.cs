using UnityEngine;

public class AI_Mission3_3 : MonoBehaviour {
    private Game game;
    private bool preStageBattle = true;
    private int preProgramCounter = -1;
    private bool Mission1 = false;
    private bool Mission2 = false;
    private bool Mission3 = false;
    private bool loop_Flag = false;
    private int loop_Level = 0;
    private int assign_Level = 0;
    private int move_Level = 0;

    private void OnEnable() {
        game = GameObject.Find("GameHandler").GetComponent<Game>();
    }

    private void AI_add_code() {
        game.Players[1].Code.Insert(InstructionType.Move, 0, 0, new int[1] { 3 });
        game.Players[1].Code.Insert(InstructionType.Move, 0, 0, new int[1] { 1 });
    }

    private void Update() {
        if (!game.IsBattle) {
            if (preStageBattle) {
                WinCheck();
                AI_add_code();
                preStageBattle = false;
            }
        }
        else {
            if (!preStageBattle) {
                preStageBattle = true;
                preProgramCounter = -1;
            }
            if (game.Players[0].ProgramCounter != preProgramCounter) {
                preProgramCounter = game.Players[0].ProgramCounter;
                Check();
            }
        }
    }

    private void WinCheck() {
        if (Mission1 && Mission2 && Mission3) {
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

    private void Check() {
        //在loop裡面包assign和move
        if (game.Players[0].Code[(ushort)preProgramCounter] != null) {
            //loop v2 < 2
            if (game.Players[0].Code[(ushort)preProgramCounter].Equals(new Instruction(InstructionType.Loop, new int[4] { 3, 4, 0, 2 })) && loop_Flag == false) {
                Mission1 = true;
                loop_Flag = true;
                loop_Level = game.Players[0].Code.GetLevel((ushort)preProgramCounter);
            }
            //assign v2 + 1 = v2
            if (game.Players[0].Code[(ushort)preProgramCounter].Equals(new Instruction(InstructionType.Assign, new int[5] { 0, 2, 0, 1, 1 }))) {
                assign_Level = game.Players[0].Code.GetLevel((ushort)preProgramCounter);
                if (assign_Level > loop_Level && loop_Flag)
                    Mission2 = true;
                else
                    loop_Flag = false;
            }
            if (game.Players[0].Code[(ushort)preProgramCounter].Equals(new Instruction(InstructionType.Move, new int[1] { 1 }))) {
                //move 黃色
                move_Level = game.Players[0].Code.GetLevel((ushort)preProgramCounter);
                if (move_Level > loop_Level && loop_Flag)
                    Mission3 = true;
                else
                    loop_Flag = false;
            }
        }
    }
}