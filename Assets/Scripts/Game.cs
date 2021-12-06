using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    public enum DifficultyType : ushort { Start, Easy, Normal, Hard };

    private Character[] _Players = new Character[2];
    public Character[] Players {
        get { return _Players; }
        set { _Players = value; }
    }

    private DifficultyType _Difficulty;
    public DifficultyType Difficulty {
        get { return _Difficulty; }
        set { _Difficulty = value; }
    }

    private ushort Round;
    private bool BattleStart;
    private bool BattleEnd;
    private bool Turn;
    private ushort CostLimit;
    private float time;
    
    [SerializeField] private GameObject Code_Area;
    [SerializeField] private GameObject Store;
    [SerializeField] private GameObject PrepareTime;
    [SerializeField] private GameObject CoverField;
    [SerializeField] private GameObject Instruction_Move;
    [SerializeField] private GameObject Instruction_Attack;
    [SerializeField] private GameObject Instruction_Assign;
    [SerializeField] private GameObject Instruction_If;
    [SerializeField] private GameObject Instruction_Loop;
    [SerializeField] private GameObject Instruction_Swap;

    void Awake() {
        //¼È©w
        _Players[0] = new Character(CharacterType.Fox);
        _Players[1] = new Character(CharacterType.Kangaroo);
        _Difficulty = DifficultyType.Start;
    }

    void Start() {
        Round = 1;
        BattleStart = false;
        BattleEnd = true;
        Turn = (_Players[0].speed > _Players[1].speed);
        CostLimit = 0;
        PrepareTime.SetActive(true);
        Store.SetActive(true);
        CoverField.SetActive(false);
        UpdateCode();
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
        time = Round * 10f + 40f;
        yield return new WaitForSeconds(time);
        Store.SetActive(false);
        PrepareTime.SetActive(false);
        CoverField.SetActive(true);
        BattleStart = true;
    }

    private IEnumerator RunCode() {
        UpdateCode();
        yield return new WaitForSeconds(1f);
        _Players[0].code.Init();
        _Players[1].code.Init();
        CostLimit = (ushort)(Round * 5);
        ushort[] TotalCost = new ushort[2] { 0, 0 };
        ushort[] ProgramCounter = new ushort[2] { 0, 0 };
        while (_Players[0].code[ProgramCounter[0]] != null || _Players[1].code[ProgramCounter[1]] != null) {
            int active = Turn ? 1 : 0;
            Instruction target = _Players[active].code[ProgramCounter[active]];
            if (target != null) {
                do {
                    TotalCost[active] += target.GetInstuctionCost();
                    if (TotalCost[active] <= CostLimit) {
                        bool condition = true;
                        condition = RunInstrucion(Turn, target);
                        ProgramCounter[active] = _Players[active].code.Next(condition);
                    }
                    else ProgramCounter[active] = _Players[active].code.Size;
                    target = _Players[active].code[ProgramCounter[active]];
                    yield return new WaitForSeconds(1f);
                } while (target != null && !(target.Type == InstructionType.Move || target.Type == InstructionType.Attack));
            }
            Turn = !Turn;
        }
        Round++;
        Store.SetActive(true);
        PrepareTime.SetActive(true);
        CoverField.SetActive(false);
        Turn = (_Players[0].speed > _Players[1].speed);
        BattleEnd = true;
    }

    private bool RunInstrucion(bool turn, Instruction target) {
        return true;
    }

    public void UpdateCode() {
        foreach (Transform child in Code_Area.transform) Destroy(child.gameObject);
        ushort ProgramCounter = 0;
        while (_Players[0].code[ProgramCounter] != null) {
            GameObject instruction;
            Instruction target = _Players[0].code[ProgramCounter];
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
            rect.sizeDelta = new Vector2(400 + 240 * _Players[0].code.GetLevel(ProgramCounter), rect.sizeDelta.y);
            instruction.transform.SetParent(Code_Area.transform, false);
            ProgramCounter++;
        }
        //_Players[0].code.Display();
    }

}
