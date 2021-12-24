using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DifficultyType : ushort { Start, Easy, Normal, Hard };

public class Game : MonoBehaviour {

    public Character[] Players;

    private DifficultyType Difficulty;

    private float[,] Location;

    private int[,] Neighbor;

    private int[,] Distance;

    private ushort Round;
    private bool IsGuest;
    private bool BattleStart;
    private bool BattleEnd;
    private bool Turn;
    private bool EndGame;
    private bool Winner;
    private ushort CostLimit;
    private float time;
    private ushort PurchaseCount;

    [SerializeField] private Sprite[] Skin;
    [SerializeField] private Sprite[] Map;
    [SerializeField] private GameObject BattleGround;
    [SerializeField] private GameObject Character1;
    [SerializeField] private GameObject Character2;
    [SerializeField] private GameObject[] Code_Area;
    [SerializeField] private GameObject PlayerCode;
    [SerializeField] private GameObject EnemyCode;
    [SerializeField] private GameObject PlayerVariable;
    [SerializeField] private GameObject EnemyVariable;
    [SerializeField] private GameObject PlayerFood;
    [SerializeField] private GameObject EnemyFood;
    [SerializeField] private GameObject PlayerHP;
    [SerializeField] private GameObject EnemyHP;
    [SerializeField] private GameObject Store;
    [SerializeField] private GameObject PrepareTime;
    [SerializeField] private GameObject RoundText;
    [SerializeField] private GameObject CoverField;
    [SerializeField] private GameObject Purchase;
    [SerializeField] private GameObject[] Commodity;
    [SerializeField] private GameObject Instruction_Move_SE;
    [SerializeField] private GameObject Instruction_Move_N;
    [SerializeField] private GameObject Instruction_Move_H;
    [SerializeField] private GameObject Instruction_Attack;
    [SerializeField] private GameObject Instruction_Assign_EN;
    [SerializeField] private GameObject Instruction_Assign_H;
    [SerializeField] private GameObject Instruction_If;
    [SerializeField] private GameObject Instruction_Loop;
    [SerializeField] private GameObject Instruction_Swap;

    void Awake() {
        //IsGuest = "" 傳入是否為申請對戰者 ""
        Players = new Character[2];
        CharacterType[] characterType = new CharacterType[2];
        //characterType = "" 取得角色資訊 ""
        //Difficulty = "" 取得難度 ""
        characterType[0] = CharacterType.Whale; //暫定
        characterType[1] = CharacterType.Kangaroo; //暫定
        Difficulty = DifficultyType.Hard; //暫定
        Players[0] = new Character(characterType[0]);
        Character1.GetComponent<Image>().sprite = Skin[(int)characterType[0]];
        Players[1] = new Character(characterType[1]);
        Character2.GetComponent<Image>().sprite = Skin[(int)characterType[1]];
        BattleGround.GetComponent<Image>().sprite = Map[(int)Difficulty];
        switch (Difficulty) {
            case DifficultyType.Start:
                Commodity[0].SetActive(true);
                Commodity[1].SetActive(false);
                Commodity[2].SetActive(false);
                Commodity[3].SetActive(true);
                Commodity[4].SetActive(false);
                Commodity[5].SetActive(false);
                Commodity[6].SetActive(false);
                Commodity[7].SetActive(false);
                Commodity[8].SetActive(false);
                Location = new float[16, 2] {
                    {-149, 277},
                    {-52, 277},
                    {48, 277},
                    {134, 277},
                    {-166, 194},
                    {-57, 194},
                    {46, 194},
                    {155, 194},
                    {-191, 96},
                    {-65, 96},
                    {55, 96},
                    {172, 96},
                    {-200, -21},
                    {-80, -21},
                    {52, -21},
                    {184, -21}
                };
                Neighbor = new int[16, 4] { // 0:上, 1:下, 2:左, 3:右 
                    {0, 4, 0, 1},
                    {1, 5, 0, 2},
                    {2, 6, 1, 3},
                    {3, 7, 2, 3},
                    {0, 8, 4, 5},
                    {1, 9, 4, 6},
                    {2, 10, 5, 7},
                    {3, 11, 6, 7},
                    {4, 12, 8, 9},
                    {5, 13, 8, 10},
                    {6, 14, 9, 11},
                    {7, 15, 10, 11},
                    {8, 12, 12, 13},
                    {9, 13, 12, 14},
                    {10, 14, 13, 15},
                    {11, 15, 14, 15}
                };
                Distance = new int[16, 16] {
                    {0, 1, 2, 3, 1, 2, 3, 4, 2, 3, 4, 5, 3, 4, 5, 6},
                    {1, 0, 1, 2, 2, 1, 2, 3, 3, 2, 3, 4, 4, 3, 4, 5},
                    {2, 1, 0, 1, 3, 2, 1, 2, 4, 3, 2, 3, 5, 4, 3, 4},
                    {3, 2, 1, 0, 4, 3, 2, 1, 5, 4, 3, 2, 6, 5, 4, 3},
                    {1, 2, 3, 4, 0, 1, 2, 3, 1, 2, 3, 4, 2, 3, 4, 5},
                    {2, 1, 2, 3, 1, 0, 1, 2, 2, 1, 2, 3, 3, 2, 3, 4},
                    {3, 2, 1, 2, 2, 1, 0, 1, 3, 2, 1, 2, 4, 3, 2, 3},
                    {4, 3, 2, 1, 3, 2, 1, 0, 4, 3, 2, 1, 5, 4, 3, 2},
                    {2, 3, 4, 5, 1, 2, 3, 4, 0, 1, 2, 3, 1, 2, 3, 4},
                    {3, 2, 3, 4, 2, 1, 2, 3, 1, 0, 1, 2, 2, 1, 2, 3},
                    {4, 3, 2, 3, 3, 2, 1, 2, 2, 1, 0, 1, 3, 2, 1, 2},
                    {5, 4, 3, 2, 4, 3, 2, 1, 3, 2, 1, 0, 4, 3, 2, 1},
                    {3, 4, 5, 6, 2, 3, 4, 5, 1, 2, 3, 4, 0, 1, 2, 3},
                    {4, 3, 4, 5, 3, 2, 3, 4, 2, 1, 2, 3, 1, 0, 1, 2},
                    {5, 4, 3, 4, 4, 3, 2, 3, 3, 2, 1, 2, 2, 1, 0, 1},
                    {6, 5, 4, 3, 5, 4, 3, 2, 4, 3, 2, 1, 3, 2, 1, 0}
                };
                if (!IsGuest) {
                    Players[0].Pos = 12;
                    Players[1].Pos = 3;
                }
                else {
                    Players[0].Pos = 3;
                    Players[1].Pos = 12;
                }
                break;
            case DifficultyType.Easy:
                Commodity[0].SetActive(true);
                Commodity[1].SetActive(false);
                Commodity[2].SetActive(false);
                Commodity[3].SetActive(true);
                Commodity[4].SetActive(true);
                Commodity[5].SetActive(false);
                Commodity[6].SetActive(true);
                Commodity[7].SetActive(false);
                Commodity[8].SetActive(false);
                Location = new float[16, 2] {
                    {-141, 253},
                    {-42, 253},
                    {60, 253},
                    {153, 253},
                    {-158, 180},
                    {-56, 180},
                    {46, 180},
                    {151, 180},
                    {-174, 104},
                    {-72, 104},
                    {41, 104},
                    {154, 104},
                    {-211, 14},
                    {-92, 14},
                    {41, 14},
                    {157, 14}
                };
                Neighbor = new int[16, 4] { // 0:上, 1:下, 2:左, 3:右 
                    {12, 4, 3, 1},
                    {13, 5, 0, 2},
                    {14, 6, 1, 3},
                    {15, 7, 2, 0},
                    {0, 8, 7, 5},
                    {1, 9, 4, 6},
                    {2, 10, 5, 7},
                    {3, 11, 6, 4},
                    {4, 12, 11, 9},
                    {5, 13, 8, 10},
                    {6, 14, 9, 11},
                    {7, 15, 10, 8},
                    {8, 0, 15, 13},
                    {9, 1, 12, 14},
                    {10, 2, 13, 15},
                    {11, 3, 14, 12}
                };
                Distance = new int[16, 16] {
                    {0, 1, 2, 1, 1, 2, 3, 2, 2, 3, 4, 3, 1, 2, 3, 4},
                    {1, 0, 1, 2, 2, 1, 2, 3, 3, 2, 3, 4, 2, 1, 2, 3},
                    {2, 1, 0, 1, 3, 2, 1, 2, 4, 3, 2, 3, 3, 2, 1, 2},
                    {1, 2, 1, 0, 2, 3, 2, 1, 3, 4, 3, 2, 4, 3, 2, 1},
                    {1, 2, 3, 2, 0, 1, 2, 1, 1, 2, 3, 2, 2, 3, 4, 3},
                    {2, 1, 2, 3, 1, 0, 1, 2, 2, 1, 2, 3, 3, 2, 3, 4},
                    {3, 2, 1, 2, 2, 1, 0, 1, 3, 2, 1, 2, 4, 3, 2, 3},
                    {2, 3, 2, 1, 1, 2, 1, 0, 2, 3, 2, 1, 3, 4, 3, 2},
                    {2, 3, 4, 3, 1, 2, 3, 2, 0, 1, 2, 1, 1, 2, 3, 2},
                    {3, 2, 3, 4, 2, 1, 2, 3, 1, 0, 1, 2, 2, 1, 2, 3},
                    {4, 3, 2, 3, 3, 2, 1, 2, 2, 1, 0, 1, 3, 2, 1, 2},
                    {3, 4, 3, 2, 2, 3, 2, 1, 1, 2, 1, 0, 2, 3, 2, 1},
                    {1, 2, 3, 4, 2, 3, 4, 3, 1, 2, 3, 2, 0, 1, 2, 1},
                    {2, 1, 2, 3, 3, 2, 3, 4, 2, 1, 2, 3, 1, 0, 1, 2},
                    {3, 2, 1, 2, 4, 3, 2, 3, 3, 2, 1, 2, 2, 1, 0, 1},
                    {4, 3, 2, 1, 3, 4, 3, 2, 2, 3, 2, 1, 1, 2, 1, 0}
                };
                if (!IsGuest) {
                    Players[0].Pos = 12;
                    Players[1].Pos = 3;
                }
                else {
                    Players[0].Pos = 3;
                    Players[1].Pos = 12;
                }
                break;
            case DifficultyType.Normal:
                Commodity[0].SetActive(false);
                Commodity[1].SetActive(true);
                Commodity[2].SetActive(false);
                Commodity[3].SetActive(true);
                Commodity[4].SetActive(true);
                Commodity[5].SetActive(false);
                Commodity[6].SetActive(true);
                Commodity[7].SetActive(true);
                Commodity[8].SetActive(false);
                Location = new float[6, 2] {
                    {-3, 284},
                    {-102, 185},
                    {99, 185},
                    {-244, 52},
                    {7, 54},
                    {251, 46},
                };
                Neighbor = new int[6, 4] { // 0:紅, 1:黃, 2:綠, 3:藍 
                    {1, 2, 5, 3},
                    {0, 4, 3, 2},
                    {5, 0, 4, 1},
                    {4, 5, 1, 0},
                    {3, 1, 2, 5},
                    {2, 3, 0, 4},
                };
                Distance = new int[6, 6] {
                    {0, 1, 1, 1, 2, 1},
                    {1, 0, 1, 1, 1, 2},
                    {1, 1, 0, 2, 1, 1},
                    {1, 1, 2, 0, 1, 1},
                    {2, 1, 1, 1, 0, 1},
                    {1, 2, 1, 1, 1, 0}
                };
                if (!IsGuest) {
                    Players[0].Pos = 4;
                    Players[1].Pos = 0;
                }
                else {
                    Players[0].Pos = 0;
                    Players[1].Pos = 4;
                }
                break;
            case DifficultyType.Hard:
                Commodity[0].SetActive(false);
                Commodity[1].SetActive(false);
                Commodity[2].SetActive(true);
                Commodity[3].SetActive(true);
                Commodity[4].SetActive(false);
                Commodity[5].SetActive(true);
                Commodity[6].SetActive(true);
                Commodity[7].SetActive(true);
                Commodity[8].SetActive(true);
                Location = new float[12, 2] {
                    {-216, 41},
                    {-240, 159},
                    {11, -53},
                    {-91, 159},
                    {4, 96},
                    {-137, 245},
                    {184, 25},
                    {-4, 205},
                    {98, 158},
                    {-7, 287},
                    {227, 158},
                    {122, 247}
                };
                Neighbor = new int[12, 4] { // 0:紅順, 1:紅逆, 2:藍順, 3:藍逆 
                    {1, 2, 1, 3},
                    {5, 0, 3, 0},
                    {0, 6, 4, 6},
                    {7, 4, 0, 1},
                    {3, 8, 6, 2},
                    {9, 1, 9, 7},
                    {2, 10, 2, 4},
                    {8, 3, 5, 9},
                    {4, 7, 11, 10},
                    {11, 5, 7, 5},
                    {6, 11, 8, 11},
                    {10, 9, 10, 8}
                };
                Distance = new int[12, 12] {
                    {0, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 4},
                    {1, 0, 2, 1, 2, 1, 3, 2, 3, 2, 4, 3},
                    {1, 2, 0, 2, 1, 3, 1, 3, 2, 4, 2, 3},
                    {1, 1, 2, 0, 1, 2, 2, 1, 2, 2, 3, 3},
                    {2, 2, 1, 1, 0, 3, 1, 2, 1, 3, 2, 2},
                    {2, 1, 3, 2, 3, 0, 4, 1, 2, 1, 3, 2},
                    {2, 3, 1, 2, 1, 4, 0, 3, 2, 3, 1, 2},
                    {2, 2, 3, 1, 2, 1, 3, 0, 1, 1, 2, 2},
                    {3, 3, 2, 2, 1, 2, 2, 1, 0, 2, 1, 1},
                    {3, 2, 4, 2, 3, 1, 3, 1, 2, 0, 2, 1},
                    {3, 4, 2, 3, 2, 3, 1, 2, 1, 2, 0, 1},
                    {4, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 0}
                };
                if (!IsGuest) {
                    Players[0].Pos = 0;
                    Players[1].Pos = 11;
                }
                else {
                    Players[0].Pos = 11;
                    Players[1].Pos = 0;
                }
                break;
        }
    }

    void Start() {
        Round = 1;
        UpdateHP();
        UpdateLocation();
        UpdateVariable();
        UpdateFood();
        BattleStart = false;
        BattleEnd = true;
        EndGame = false;
        CostLimit = 0;
        PurchaseCount = 0;
        PrepareTime.SetActive(true);
        Store.SetActive(true);
        CoverField.SetActive(false);
        EnemyCode.SetActive(false);
        PlayerVariable.SetActive(false);
        EnemyVariable.SetActive(false);
        PlayerFood.SetActive(false);
        EnemyFood.SetActive(false);
        PlayerHP.SetActive(false);
        EnemyHP.SetActive(false);
        UpdateCode(0);
    }

    void Update() {
        if (!EndGame) {
            if (BattleEnd) {
                StartCoroutine(PrepareCode());
                BattleEnd = false;
            }
            if (BattleStart) {
                StartCoroutine(RunCode());
                BattleStart = false;
            }
            if (PrepareTime.activeSelf) {
                time -= Time.deltaTime;
                if (time > 0) PrepareTime.GetComponent<Text>().text = time.ToString("0.0");
                else PrepareTime.GetComponent<Text>().text = "0.0";
            }
        }
        else {
            // "" 回到大廳Scene ""
        }
    }

    private IEnumerator PrepareCode() {
        PlayerCode.GetComponent<RectTransform>().anchoredPosition = new Vector3(350, 0);
        PlayerCode.GetComponent<RectTransform>().sizeDelta = new Vector2(650, 1000);
        EnemyCode.SetActive(false);
        Store.SetActive(true);
        PrepareTime.SetActive(true);
        CoverField.SetActive(false);
        Purchase.SetActive(true);
        PlayerVariable.SetActive(false);
        EnemyVariable.SetActive(false);
        PlayerFood.SetActive(false);
        EnemyFood.SetActive(false);
        PlayerHP.SetActive(false);
        EnemyHP.SetActive(false);
        RoundText.GetComponent<Text>().text = "Round " + Round.ToString();
        PurchaseCount = 0;
        Purchase.transform.GetChild(0).GetComponent<Text>().text = PurchaseCount.ToString() + " / 5";
        PlayerCode.transform.GetChild(2).gameObject.SetActive(false);
        time = Round * 15f + 20f;
        yield return new WaitForSeconds(time);
        BattleStart = true;
    }

    private IEnumerator RunCode() {
        CostLimit = (ushort)(Round * 5);
        Turn = (Players[0].speed < Players[1].speed);
        PlayerCode.GetComponent<RectTransform>().anchoredPosition = new Vector2(350, -250);
        PlayerCode.GetComponent<RectTransform>().sizeDelta = new Vector2(650, 500);
        EnemyCode.SetActive(true);
        Store.SetActive(false);
        PrepareTime.SetActive(false);
        CoverField.SetActive(true);
        Purchase.SetActive(false);
        if (Difficulty != DifficultyType.Start) {
            PlayerVariable.SetActive(true);
            EnemyVariable.SetActive(true);
        }
        if (Difficulty == DifficultyType.Hard) {
            PlayerFood.SetActive(true);
            EnemyFood.SetActive(true);
        }
        PlayerHP.SetActive(true);
        EnemyHP.SetActive(true);
        PlayerCode.transform.GetChild(2).gameObject.SetActive(true);
        UpdateCode(0);
        // 傳出己方資料
        // Code Enemycode = "" 傳入對方資料 "";
        // Players[1].code = Enemycode;
        Players[1].code.Insert(InstructionType.Move, 0, 0, new int[1] { 0 }); //暫定每回合+1 Move
        UpdateCode(1);
        Players[0].Reset();
        Players[1].Reset();
        UpdateCost(false);
        UpdateCost(true);
        UpdateVariable();
        if (Difficulty == DifficultyType.Hard) {
            if (!IsGuest) {
                for (int i = 0; i < 10; i++) {
                    int number = Random.Range(1, 15);
                    Players[0].food[i] = number;
                    Players[1].food[i] = number;
                }
                // "" 送出food資料 ""
            }
            else {
                // "" 取得food資料 ""
            }
        }
        else {
            for (int i = 0; i < 10; i++) {
                Players[0].food[i] = 0;
                Players[1].food[i] = 0;
            }
        }
        UpdateFood();
        yield return new WaitForSeconds(1f);
        Players[0].ProgramCounter = Players[0].code.Next(true);
        Players[1].ProgramCounter = Players[1].code.Next(true);
        while ((Players[0].code[Players[0].ProgramCounter] != null || Players[1].code[Players[1].ProgramCounter] != null) && !EndGame) {
            int active = Turn ? 1 : 0;
            Instruction target = Players[active].code[Players[active].ProgramCounter];
            if (target != null) {
                do {
                    target = Players[active].code[Players[active].ProgramCounter];
                    Players[active].TotalCost += target.GetInstuctionCost();
                    UpdateCost(Turn);
                    if (Players[active].TotalCost <= CostLimit) {
                        bool condition = true;
                        condition = RunInstrucion(active, target);
                        Players[active].ProgramCounter = Players[active].code.Next(condition);
                    }
                    else {
                        Players[active].currentHP -= 5;
                        UpdateHP();
                        if (Players[active].currentHP <= 0) {
                            EndGame = true;
                            Winner = active == 0 ? false : true;
                        }
                        Players[active].ProgramCounter = Players[active].code.Size;
                    }
                    yield return new WaitForSeconds(1f);
                } while (Players[active].code[Players[active].ProgramCounter] != null && !(target.Type == InstructionType.Move || target.Type == InstructionType.Attack) && !EndGame);
            }
            Turn = !Turn;
        }
        Round++;
        BattleEnd = true;
    }

    private bool RunInstrucion(int active, Instruction target) {
        int s1, s2;
        switch (target.Type) {
            case InstructionType.Move:
                if (Neighbor[Players[active].Pos, target.Arguments[0]] != Players[1 - active].Pos) {
                    Players[active].Pos = Neighbor[Players[active].Pos, target.Arguments[0]];
                    UpdateLocation();
                }
                return true;
            case InstructionType.Attack:
                bool near = false;
                for (int i = 0; i < 4; i++)
                    if (Players[active].Pos == Neighbor[Players[1 - active].Pos, i])
                        near = true;
                if (near) {
                    Players[1 - active].currentHP -= (int)((Players[active].attack_mag * 5f) * (10f / (10f + Players[1 - active].attack_def)) + Players[active].ability_mag * (Players[active].food[Players[active].FoodCounter++] + Round) / 3f * (10f / (10f + Players[1 - active].ability_def)));
                    if (Difficulty == DifficultyType.Hard) {
                        if (IsSorted(active)) Players[1 - active].currentHP -= 25;
                    }
                    Players[active].FoodCounter %= 10;
                    UpdateHP();
                    if (Players[1 - active].currentHP <= 0) {
                        EndGame = true;
                        Winner = active == 0 ? true : false;
                    }
                }
                return true;
            case InstructionType.Assign:
                if (target.Arguments[1] != 6) {
                    if (target.Arguments[1] == 0) s1 = 0;
                    else s1 = Players[active].variable[target.Arguments[1] - 1];
                    if (target.Arguments[2] == 0) s2 = target.Arguments[3];
                    else s2 = Players[active].variable[target.Arguments[2] - 1];
                    switch (target.Arguments[0]) {
                        case 0: // +
                            Players[active].variable[target.Arguments[4]] = s1 + s2;
                            break;
                        case 1: // -
                            Players[active].variable[target.Arguments[4]] = s1 - s2;
                            break;
                        case 2: // *
                            Players[active].variable[target.Arguments[4]] = s1 * s2;
                            break;
                        case 3: // /
                            if (s2 != 0)
                                Players[active].variable[target.Arguments[4]] = s1 / s2;
                            break;
                        case 4: // %
                            if (s2 != 0)
                                Players[active].variable[target.Arguments[4]] = s1 % s2;
                            break;
                    }
                }
                else {
                    if (target.Arguments[2] == 0) {
                        s2 = target.Arguments[3];
                        if (target.Arguments[3] > 9) s2 = 9;
                        else if (target.Arguments[3] < 0) s2 = 0;
                        Players[active].variable[target.Arguments[4]] = Players[active].food[s2];
                    }
                    else {
                        s2 = Players[active].variable[target.Arguments[2] - 1];
                        if (Players[active].variable[target.Arguments[2] - 1] > 9) s2 = 9;
                        else if (Players[active].variable[target.Arguments[2] - 1] < 0) s2 = 0;
                        Players[active].variable[target.Arguments[4]] = Players[active].food[s2];
                    }
                }
                if (Players[active].variable[target.Arguments[4]] > 999) Players[active].variable[target.Arguments[4]] = 999;
                if (Players[active].variable[target.Arguments[4]] < -99) Players[active].variable[target.Arguments[4]] = -99;
                UpdateVariable();
                return true;
            case InstructionType.If:
                if (target.Arguments[1] == 0) s1 = Distance[Players[0].Pos, Players[1].Pos];
                else if (target.Arguments[1] == 1) s1 = Players[active].currentHP;
                else if (target.Arguments[1] == 2) s1 = Players[1 - active].currentHP;
                else s1 = Players[active].variable[target.Arguments[1] - 3];
                if (target.Arguments[2] == 0) s2 = target.Arguments[3];
                else s2 = Players[active].variable[target.Arguments[2] - 1];
                switch (target.Arguments[0]) {
                    case 0: // ==
                        return (s1 == s2);
                    case 1: // !=
                        return (s1 != s2);
                    case 2: // >
                        return (s1 > s2);
                    case 3: // <
                        return (s1 < s2);
                    case 4: // >=
                        return (s1 >= s2);
                    case 5: // <=
                        return (s1 <= s2);
                }
                return true;
            case InstructionType.Loop:
                if (target.Arguments[1] == 0) s1 = Distance[Players[0].Pos, Players[1].Pos];
                else if (target.Arguments[1] == 1) s1 = Players[active].currentHP;
                else if (target.Arguments[1] == 2) s1 = Players[1 - active].currentHP;
                else s1 = Players[active].variable[target.Arguments[1] - 3];
                if (target.Arguments[2] == 0) s2 = target.Arguments[3];
                else s2 = Players[active].variable[target.Arguments[2] - 1];
                switch (target.Arguments[0])
                {
                    case 0: // ==
                        return (s1 == s2);
                    case 1: // !=
                        return (s1 != s2);
                    case 2: // >
                        return (s1 > s2);
                    case 3: // <
                        return (s1 < s2);
                    case 4: // >=
                        return (s1 >= s2);
                    case 5: // <=
                        return (s1 <= s2);
                }
                return true;
            case InstructionType.Swap:
                if (target.Arguments[0] == 0) s1 = target.Arguments[1];
                else s1 = Players[active].variable[target.Arguments[0] - 1];
                if (target.Arguments[2] == 0) s2 = target.Arguments[3];
                else s2 = Players[active].variable[target.Arguments[2] - 1];
                if (s1 > 9) s1 = 9;
                else if (s1 < 0) s1 = 0;
                if (s2 > 9) s2 = 9;
                else if (s2 < 0) s2 = 0;
                int tmp = Players[active].food[s1];
                Players[active].food[s1] = Players[active].food[s2];
                Players[active].food[s2] = tmp;
                UpdateFood();
                return true;
        }
        return true;
    }

    public void UpdateCode(ushort num) {
        foreach (Transform child in Code_Area[num].transform) Destroy(child.gameObject);
        ushort ProgramCounter = 0;
        while (Players[num].code[ProgramCounter] != null) {
            GameObject instruction;
            Instruction target = Players[num].code[ProgramCounter];
            switch (target.Type) {
                case InstructionType.Move:
                    switch (Difficulty) {
                        case DifficultyType.Start:
                            instruction = Instantiate(Instruction_Move_SE);
                            break;
                        case DifficultyType.Easy:
                            instruction = Instantiate(Instruction_Move_SE);
                            break;
                        case DifficultyType.Normal:
                            instruction = Instantiate(Instruction_Move_N);
                            break;
                        case DifficultyType.Hard:
                            instruction = Instantiate(Instruction_Move_H);
                            break;
                        default:
                            instruction = Instantiate(Instruction_Move_SE);
                            break;
                    }
                    instruction.transform.GetChild(0).GetChild(2).GetComponent<Dropdown>().value = target.Arguments[0];
                    break;
                case InstructionType.Attack:
                    instruction = Instantiate(Instruction_Attack);
                    break;
                case InstructionType.Assign:
                    if (Difficulty == DifficultyType.Hard)
                        instruction = Instantiate(Instruction_Assign_H);
                    else
                        instruction = Instantiate(Instruction_Assign_EN);
                    for (int i = 0; i < 3; i++)
                        instruction.transform.GetChild(0).GetChild(i + 2).GetComponent<Dropdown>().value = target.Arguments[i];
                    instruction.transform.GetChild(0).GetChild(5).GetComponent<InputField>().text = target.Arguments[3].ToString();
                    instruction.transform.GetChild(0).GetChild(6).GetComponent<Dropdown>().value = target.Arguments[4];
                    break;
                case InstructionType.If:
                    instruction = Instantiate(Instruction_If);
                    for (int i = 0; i < 3; i++)
                        instruction.transform.GetChild(0).GetChild(i + 2).GetComponent<Dropdown>().value = target.Arguments[i];
                    instruction.transform.GetChild(0).GetChild(5).GetComponent<InputField>().text = target.Arguments[3].ToString();
                    break;
                case InstructionType.Loop:
                    instruction = Instantiate(Instruction_Loop);
                    for (int i = 0; i < 3; i++)
                        instruction.transform.GetChild(0).GetChild(i + 2).GetComponent<Dropdown>().value = target.Arguments[i];
                    instruction.transform.GetChild(0).GetChild(5).GetComponent<InputField>().text = target.Arguments[3].ToString();
                    break;
                case InstructionType.Swap:
                    instruction = Instantiate(Instruction_Swap);
                    instruction.transform.GetChild(0).GetChild(2).GetComponent<Dropdown>().value = target.Arguments[0];
                    instruction.transform.GetChild(0).GetChild(3).GetComponent<InputField>().text = target.Arguments[1].ToString();
                    instruction.transform.GetChild(0).GetChild(4).GetComponent<Dropdown>().value = target.Arguments[2];
                    instruction.transform.GetChild(0).GetChild(5).GetComponent<InputField>().text = target.Arguments[3].ToString();
                    break;
                default:
                    instruction = null;
                    break;
            }
            RectTransform rect = instruction.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(400 + 240 * Players[num].code.GetLevel(ProgramCounter), rect.sizeDelta.y);
            instruction.transform.SetParent(Code_Area[num].transform, false);
            ProgramCounter++;
        }
        //_Players[num].code.Display();
    }

    private void UpdateCost(bool num) {
        if (!num)
            PlayerCode.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = Players[0].TotalCost.ToString() + " / " + CostLimit.ToString();
        else
            EnemyCode.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = Players[1].TotalCost.ToString() + " / "  + CostLimit.ToString();
    }

    private void UpdateLocation() {
        Character1.GetComponent<RectTransform>().anchoredPosition = new Vector2(Location[Players[0].Pos, 0], Location[Players[0].Pos, 1]);
        Character2.GetComponent<RectTransform>().anchoredPosition = new Vector2(Location[Players[1].Pos, 0], Location[Players[1].Pos, 1]);
    }

    private void UpdateHP() {
        PlayerHP.transform.GetChild(0).GetComponent<Text>().text = "HP: " + Players[0].currentHP.ToString();
        EnemyHP.transform.GetChild(0).GetComponent<Text>().text = "HP: " + Players[1].currentHP.ToString();
    }

    private void UpdateVariable() {
        for (int i = 0; i < 5; i++) {
            PlayerVariable.transform.GetChild(i).GetComponent<Text>().text = Players[0].variable[i].ToString();
            EnemyVariable.transform.GetChild(i).GetComponent<Text>().text = Players[1].variable[i].ToString();
        }
    }

    private void UpdateFood() {
        for (int i = 0; i < 10; i++) {
            PlayerFood.transform.GetChild(i).GetComponent<Text>().text = Players[0].food[i].ToString();
            EnemyFood.transform.GetChild(i).GetComponent<Text>().text = Players[1].food[i].ToString();
        }
    }

    private bool IsSorted(int num) {
        for (int i = 0; i < 9; i++) {
            if (Players[num].food[i] > Players[num].food[i + 1])
                return false;
        }
        return true;
    }

    public int IncPurchaseCount() {
        if (PurchaseCount < 5) PurchaseCount++;
        else return -1;
        Purchase.transform.GetChild(0).GetComponent<Text>().text = PurchaseCount.ToString() + " / 5";
        return PurchaseCount;
    }
}
