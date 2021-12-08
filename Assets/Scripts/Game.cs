using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    public enum DifficultyType : ushort { Start, Easy, Normal, Hard };

    public Character[] Players;

    private DifficultyType Difficulty;

    private ushort Round;
    private bool BattleStart;
    private bool BattleEnd;
    private bool Turn;
    private ushort CostLimit;
    private float time;
    private ushort PurchaseCount;
    
    [SerializeField] private GameObject[] Code_Area;
    [SerializeField] private GameObject PlayerCode;
    [SerializeField] private GameObject EnemyCode;
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
        //�ȩw
        Players = new Character[2];
        Players[0] = new Character(CharacterType.Fox);
        Players[1] = new Character(CharacterType.Kangaroo);
        Difficulty = DifficultyType.Start;
    }

    void Start() {
        Round = 1;
        BattleStart = false;
        BattleEnd = true;
        CostLimit = 0;
        PurchaseCount = 0;
        PrepareTime.SetActive(true);
        Store.SetActive(true);
        CoverField.SetActive(false);
        EnemyCode.SetActive(false);
        UpdateCode(0);
    }
    
    void Update() {
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

    private IEnumerator PrepareCode() {
        PlayerCode.GetComponent<RectTransform>().anchoredPosition = new Vector3(350, 0);
        PlayerCode.GetComponent<RectTransform>().sizeDelta = new Vector2(650, 1000);
        EnemyCode.SetActive(false);
        Store.SetActive(true);
        PrepareTime.SetActive(true);
        CoverField.SetActive(false);
        Purchase.SetActive(true);
        PurchaseCount = 0;
        Purchase.transform.GetChild(0).GetComponent<Text>().text = PurchaseCount.ToString() + " / 5";
        PlayerCode.transform.GetChild(2).gameObject.SetActive(false);
        time = Round * 10f + 0f;
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
        PlayerCode.transform.GetChild(2).gameObject.SetActive(true);
        UpdateCode(0);
        // �ǥX�v����
        // Code Enemycode = "" �ǤJ����� "";
        // Players[1].code = Enemycode;
        Players[1].code.Insert(InstructionType.Move, 0, 0, new int[1] { 0 }); //�ȩw�C�^�X+1 Move
        UpdateCode(1);
        Players[0].Reset();
        Players[1].Reset();
        UpdateCost(false);
        UpdateCost(true);
        yield return new WaitForSeconds(1f);
        Players[0].ProgramCounter = Players[0].code.Next(true);
        Players[1].ProgramCounter = Players[1].code.Next(true);
        while (Players[0].code[Players[0].ProgramCounter] != null || Players[1].code[Players[1].ProgramCounter] != null) {
            int active = Turn ? 1 : 0;
            Instruction target = Players[active].code[Players[active].ProgramCounter];
            if (target != null) {
                do {
                    target = Players[active].code[Players[active].ProgramCounter];
                    Players[active].TotalCost += target.GetInstuctionCost();
                    UpdateCost(Turn);
                    if (Players[active].TotalCost <= CostLimit) {
                        bool condition = true;
                        condition = RunInstrucion(Turn, target);
                        Players[active].ProgramCounter = Players[active].code.Next(condition);
                    }
                    else Players[active].ProgramCounter = Players[active].code.Size;
                    yield return new WaitForSeconds(1f);
                } while (Players[active].code[Players[active].ProgramCounter] != null && !(target.Type == InstructionType.Move || target.Type == InstructionType.Attack));
            }
            Turn = !Turn;
        }
        Round++;
        BattleEnd = true;
    }

    private bool RunInstrucion(bool turn, Instruction target) {
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
            rect.sizeDelta = new Vector2(400 + 240 * Players[0].code.GetLevel(ProgramCounter), rect.sizeDelta.y);
            instruction.transform.SetParent(Code_Area[num].transform, false);
            ProgramCounter++;
        }
        //_Players[num].code.Display();
    }

    public void UpdateCost(bool num) {
        if (!num)
            PlayerCode.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = Players[0].TotalCost.ToString() + " / " + CostLimit.ToString();
        else
            EnemyCode.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = Players[1].TotalCost.ToString() + " / "  + CostLimit.ToString();
    }

    public int IncPurchaseCount() {
        if (PurchaseCount < 5) PurchaseCount++;
        else return -1;
        Purchase.transform.GetChild(0).GetComponent<Text>().text = PurchaseCount.ToString() + " / 5";
        return PurchaseCount;
    }
}
