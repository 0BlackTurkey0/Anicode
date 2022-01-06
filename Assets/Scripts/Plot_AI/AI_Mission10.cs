using UnityEngine;
//3回合內使用雙層loop完成一次行動
public class AI_Mission10:MonoBehaviour
{
    private Game game;
    private bool preStageBattle = true;
    private int preProgramCounter = -1;
    private bool Mission1 = false;
    //private bool Mission2 = false;
    private int loop_Level1 = 0;
    private int loop_Level2 = 0;
    private bool loop_Flag1 = false;
    private bool loop_Flag2 = false;

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
                WinCheck();
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