using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    public enum DifficultyType : ushort { Start, Easy, Normal, Hard };

    public Character[] Players;

    private DifficultyType Difficulty;

    private float[,] Location = new float[12, 2] {
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

    private int[,] Neighbor = new int[12, 4] { // 0:紅順, 1:紅逆, 2:藍順, 3:藍逆 
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

    private int[,] Distance = new int[12, 12] { 
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
    [SerializeField] private GameObject CoverField;
    [SerializeField] private GameObject Purchase;
    [SerializeField] private GameObject Instruction_Move;
    [SerializeField] private GameObject Instruction_Attack;
    [SerializeField] private GameObject Instruction_Assign;
    [SerializeField] private GameObject Instruction_If;
    [SerializeField] private GameObject Instruction_Loop;
    [SerializeField] private GameObject Instruction_Swap;

    void Awake() {
        //IsGuest = "" 傳入是否為被接受對戰者 ""
        //暫定
        Players = new Character[2];
        Players[0] = new Character(CharacterType.Fox);
        Players[1] = new Character(CharacterType.Kangaroo);
        Difficulty = DifficultyType.Hard;
    }

    void Start() {
        Round = 1;
        if (!IsGuest) {
            Players[0].Pos = 0;
            Players[1].Pos = 11;
        }
        else {
            Players[0].Pos = 11;
            Players[1].Pos = 0;
        }
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
        PlayerVariable.SetActive(true);
        EnemyVariable.SetActive(true);
        PlayerFood.SetActive(true);
        EnemyFood.SetActive(true);
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
        if (!IsGuest) {
            for (int i = 0; i < 10; i++) {
                int number = Random.Range(1, 15);
                Players[0].food[i] = number;
                Players[1].food[i] = number;
            }
        }
        else {
            // "" 取得food資料 ""
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
                    Players[1 - active].currentHP -= (int)((Players[active].attack_mag * 5f) * (10f / (10f + Players[1 - active].attack_def)) + Players[active].ability_mag * Players[active].food[Players[active].FoodCounter++] / 3f * (10f / (10f + Players[1 - active].ability_def)));
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
                            Players[active].variable[target.Arguments[4]] = s1 / s2;
                            break;
                        case 4: // %
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
                    instruction = Instantiate(Instruction_Move);
                    instruction.transform.GetChild(0).GetChild(2).GetComponent<Dropdown>().value = target.Arguments[0];
                    break;
                case InstructionType.Attack:
                    instruction = Instantiate(Instruction_Attack);
                    break;
                case InstructionType.Assign:
                    instruction = Instantiate(Instruction_Assign);
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

    public int IncPurchaseCount() {
        if (PurchaseCount < 5) PurchaseCount++;
        else return -1;
        Purchase.transform.GetChild(0).GetComponent<Text>().text = PurchaseCount.ToString() + " / 5";
        return PurchaseCount;
    }
}
