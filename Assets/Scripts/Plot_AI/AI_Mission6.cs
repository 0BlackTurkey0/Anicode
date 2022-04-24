using UnityEngine;
//完成assign，if各2次(2)
public class AI_Mission6 : MonoBehaviour {
    private Game game;
    private bool preStageBattle = true;
    private int preProgramCounter = -1;
    private bool Mission1 = false;
    private bool Mission2 = false;
    private int cnt_if = 0;
    private int cnt_assign = 0;

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
            if (game.Players[0].ProgramCounter != (ushort)preProgramCounter) {
                preProgramCounter = game.Players[0].ProgramCounter;
                Check();
            }
        }
    }

    private void WinCheck() {
        if (Mission1 && Mission2) {
            Debug.Log("You win");
            game.EndGame = true;
            game.Winner = true;
        }
    }

    private void Check() {
        if (game.Players[0].Code[(ushort)preProgramCounter] != null) {
            if (game.Players[0].Code[(ushort)preProgramCounter].Type == InstructionType.If) {
                cnt_if++;
                if (cnt_if > 1)
                    Mission1 = true;
            }
        }
        if (game.Players[0].Code[(ushort)preProgramCounter] != null) {
            if (game.Players[0].Code[(ushort)preProgramCounter].Type == InstructionType.Assign) {
                cnt_assign++;
                if (cnt_assign > 1)
                    Mission2 = true;
            }
        }
    }
}