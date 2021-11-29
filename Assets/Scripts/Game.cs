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
    
    [SerializeField] private GameObject Code_Area;
    [SerializeField] private GameObject Instruction_Prefab;

    void Awake() {
        _Players[0] = new Character(CharacterType.Fox);
        _Players[1] = new Character(CharacterType.Kangaroo);
    }

    void Start() {
        UpdateCode();
    }
    
    void Update() {
        
    }

    public void UpdateCode() {
        foreach (Transform child in Code_Area.transform) Destroy(child.gameObject);
        ushort ProgramCounter = 0;
        while (_Players[0].code[ProgramCounter] != null) {
            GameObject instruction = Instantiate(Instruction_Prefab);
            RectTransform rect = instruction.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(200 + 120 * _Players[0].code.GetLevel(ProgramCounter), rect.sizeDelta.y);
            instruction.transform.SetParent(Code_Area.transform, false);
            Text typeString = instruction.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            switch (_Players[0].code[ProgramCounter].Type) {
                case InstructionType.Move:
                    typeString.text = "Move";
                    break;
                case InstructionType.Loop:
                    typeString.text = "Loop";
                    break;
                case InstructionType.If:
                    typeString.text = "If";
                    break;
                case InstructionType.Assign:
                    typeString.text = "Assign";
                    break;
                case InstructionType.Attack:
                    typeString.text = "Attack";
                    break;
            }
            ProgramCounter++;
        }
        //_Players[0].code.Display();
    }

}
