using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum DifficultyType : ushort { Start, Easy, Normal, Hard, NULL };

public class Game : MonoBehaviour
{

    public Character[] Players;

    private DifficultyType _difficulty;
    public DifficultyType Difficulty
    {
        get { return _difficulty; }
    }

    private float[,] _location;
    public float[,] Location
    {
        get { return _location; }
    }

    private int[,] _neighbor;
    public int[,] Neighbor
    {
        get { return _neighbor; }
    }

    private int[,] _distance;
    public int[,] Distance
    {
        get { return _distance; }
    }

    private ushort _round;
    public ushort Round
    {
        get { return _round; }
    }

    private bool _isGuest;
    public bool IsGuest
    {
        get { return _isGuest; }
    }

    private bool _isDuel;
    public bool _IsDuel
    {
        get { return _isDuel; }
    }

    private bool _isSimple;
    public bool IsSimple
    {
        get { return _isSimple; }
        set { _isSimple = value; }
    }

    private bool _turn;
    public bool Turn
    {
        get { return _turn; }
    }

    private bool _endGame;
    public bool EndGame
    {
        get { return _endGame; }
        set { _endGame = value; }
    }

    private bool _winner;
    public bool Winner
    {
        get { return _winner; }
        set { _winner = value; }
    }

    private ushort _costLimit;
    public ushort CostLimit
    {
        get { return _costLimit; }
    }

    private ushort _purchaseCount;
    public ushort PurchaseCount
    {
        get { return _purchaseCount; }
    }

    private float _time;
    public float Time
    {
        get { return _time; }
    }

    private bool _isBattle;
    public bool IsBattle
    {
        get { return _isBattle; }
        set { _isBattle = value; }
    }

    private bool _battleStart;
    private bool _battleEnd;

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
    private ApplicationHandler applicationHandler;
    private Network networkHandler;


    void Awake()
    {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
        CharacterType[] characterType = new CharacterType[2];
        _isDuel = applicationHandler.IsDuel;
        //_isDuel = false; //暫定
        if (_isDuel)
        {
            networkHandler = GameObject.Find("Network").GetComponent<Network>();
            _isGuest = networkHandler.isGuest;
            _difficulty = (DifficultyType)networkHandler.finalDifficulty;
            characterType[0] = (CharacterType)networkHandler.playerMode.Character;
            characterType[1] = (CharacterType)networkHandler.challengerMode.Character;
        }
        else
        {
            IsSimple = applicationHandler.IsSimple;
            _difficulty = applicationHandler.DiffiType;
            characterType[0] = applicationHandler.CharaType[0];
            characterType[1] = applicationHandler.CharaType[1];
        }
        Players = new Character[2];
        Players[0] = new Character(characterType[0]);
        Character1.GetComponent<Image>().sprite = Skin[(int)characterType[0]];
        Players[1] = new Character(characterType[1]);
        Character2.GetComponent<Image>().sprite = Skin[(int)characterType[1]];
        BattleGround.GetComponent<Image>().sprite = Map[(int)_difficulty];
        switch (_difficulty)
        {
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
                _location = new float[16, 2] {
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
                _neighbor = new int[16, 4] { // 0:上, 1:下, 2:左, 3:右 
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
                _distance = new int[16, 16] {
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
                if (!_isGuest)
                {
                    Players[0].Pos = 12;
                    Players[1].Pos = 3;
                }
                else
                {
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
                _location = new float[16, 2] {
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
                _neighbor = new int[16, 4] { // 0:上, 1:下, 2:左, 3:右 
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
                _distance = new int[16, 16] {
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
                if (!_isGuest)
                {
                    Players[0].Pos = 12;
                    Players[1].Pos = 3;
                }
                else
                {
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
                _location = new float[6, 2] {
                    {-3, 284},
                    {-102, 185},
                    {99, 185},
                    {-244, 52},
                    {7, 54},
                    {251, 46},
                };
                _neighbor = new int[6, 4] { // 0:紅, 1:黃, 2:綠, 3:藍 
                    {1, 2, 5, 3},
                    {0, 4, 3, 2},
                    {5, 0, 4, 1},
                    {4, 5, 1, 0},
                    {3, 1, 2, 5},
                    {2, 3, 0, 4},
                };
                _distance = new int[6, 6] {
                    {0, 1, 1, 1, 2, 1},
                    {1, 0, 1, 1, 1, 2},
                    {1, 1, 0, 2, 1, 1},
                    {1, 1, 2, 0, 1, 1},
                    {2, 1, 1, 1, 0, 1},
                    {1, 2, 1, 1, 1, 0}
                };
                if (!_isGuest)
                {
                    Players[0].Pos = 4;
                    Players[1].Pos = 0;
                }
                else
                {
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
                _location = new float[12, 2] {
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
                _neighbor = new int[12, 4] { // 0:紅順, 1:紅逆, 2:藍順, 3:藍逆 
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
                _distance = new int[12, 12] {
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
                if (!_isGuest)
                {
                    Players[0].Pos = 0;
                    Players[1].Pos = 11;
                }
                else
                {
                    Players[0].Pos = 11;
                    Players[1].Pos = 0;
                }
                break;
        }
    }

    void Start()
    {
        _isBattle = false;
        _round = 1;
        UpdateHP();
        UpdateLocation();
        UpdateVariable();
        UpdateFood();
        _battleStart = false;
        _battleEnd = true;
        _endGame = false;
        _costLimit = 0;
        _purchaseCount = 0;
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

    void Update()
    {
        if (!_endGame)
        {
            if (_battleEnd)
            {
                StartCoroutine(PrepareCode());
                _isBattle = false;
                _battleEnd = false;
            }
            if (_battleStart)
            {
                StartCoroutine(RunCode());
                _isBattle = true;
                _battleStart = false;
            }
            if (PrepareTime.activeSelf)
            {
                _time -= UnityEngine.Time.deltaTime;
                if (_time > 0) PrepareTime.GetComponent<Text>().text = _time.ToString("0.0");
                else PrepareTime.GetComponent<Text>().text = "0.0";
            }
        }
        else
        {
            if (_isSimple)//判斷是否是單人模式
            {
                {
                    //簡易、劇情單人模式依據勝負更新進度以及SCENE跳轉 - applicationHandler
                    if (_winner == true)
                    {
                        applicationHandler.GameData.IswinForSimple = true;
                        applicationHandler.GameData.SaveData();
                        applicationHandler.IsSimple = false;
                    }
                    else
                    {
                        applicationHandler.GameData.IswinForSimple = false;
                        applicationHandler.GameData.SaveData();
                        applicationHandler.IsSimple = false;
                    }
                    SceneManager.LoadScene(7);
                }
            }
            else//單人劇情模式
            {
                if (_winner)
                {
                    if (0 < applicationHandler.Challenge && applicationHandler.Challenge <= 16)
                    {
                        applicationHandler.GameData.Schedule_Single |= 1 << (applicationHandler.Challenge + 1);
                        applicationHandler.GameData.SaveData();
                    }
                }
                SceneManager.LoadScene(10);
            }
            
        }
        if (_isDuel && !networkHandler.isConnect)
        {
            // "" 斷線畫面      ""
            SceneManager.LoadScene(0);
        }
    }

    private IEnumerator PrepareCode()
    {
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
        RoundText.GetComponent<Text>().text = "Round " + _round.ToString();
        _purchaseCount = 0;
        Purchase.transform.GetChild(0).GetComponent<Text>().text = _purchaseCount.ToString() + " / 5";
        PlayerCode.transform.GetChild(2).gameObject.SetActive(false);
        _time = _round * 5f + 5f;
        yield return new WaitForSeconds(_time);
        _battleStart = true;
    }

    private IEnumerator RunCode()
    {
        _costLimit = (ushort)(_round * 5);
        _turn = (Players[0].Speed < Players[1].Speed);
        PlayerCode.GetComponent<RectTransform>().anchoredPosition = new Vector2(350, -250);
        PlayerCode.GetComponent<RectTransform>().sizeDelta = new Vector2(650, 500);
        EnemyCode.SetActive(true);
        Store.SetActive(false);
        PrepareTime.SetActive(false);
        CoverField.SetActive(true);
        Purchase.SetActive(false);
        if (_difficulty != DifficultyType.Start)
        {
            PlayerVariable.SetActive(true);
            EnemyVariable.SetActive(true);
        }
        if (_difficulty == DifficultyType.Hard)
        {
            PlayerFood.SetActive(true);
            EnemyFood.SetActive(true);
        }
        PlayerHP.SetActive(true);
        EnemyHP.SetActive(true);
        PlayerCode.transform.GetChild(2).gameObject.SetActive(true);
        UpdateCode(0);
        if (_isDuel)
        {
            if (!_isGuest && _difficulty == DifficultyType.Hard)
            {
                for (int i = 0; i < 10; i++)
                {
                    int number = Random.Range(1, 15);
                    Players[0].Food[i] = number;
                    Players[1].Food[i] = number;
                }
            }
            Debug.Log(1);
            networkHandler.SendGameData(Players[0].Code);
            Debug.Log(2);
            while (!networkHandler.isCodeReceive)
            {

            }
            Debug.Log(3);
            networkHandler.isCodeReceive = false;
            Players[1].Code = new Code(networkHandler.challengerCode);

            if (!_isGuest)
            {
                networkHandler.SendGameFood(Players[0].Food);
                Debug.Log(4);
            }
            else
            {
                Debug.Log(5);
                while (!networkHandler.isFoodReceive)
                {

                }
                networkHandler.isFoodReceive = false;
                Players[0].Food = networkHandler.challengerFood;
                Players[1].Food = networkHandler.challengerFood;
            }
            Debug.Log(6);
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                Players[0].Food[i] = 0;
                Players[1].Food[i] = 0;
            }
        }
        UpdateCode(1);
        Players[0].Reset();
        Players[1].Reset();
        UpdateCost(false);
        UpdateCost(true);
        UpdateVariable();
        UpdateFood();
        yield return new WaitForSeconds(1f);
        Players[0].ProgramCounter = Players[0].Code.Next(true);
        Players[1].ProgramCounter = Players[1].Code.Next(true);
        while ((Players[0].Code[Players[0].ProgramCounter] != null || Players[1].Code[Players[1].ProgramCounter] != null) && !_endGame)
        {
            int active = _turn ? 1 : 0;
            Instruction target = Players[active].Code[Players[active].ProgramCounter];
            if (target != null)
            {
                do
                {
                    target = Players[active].Code[Players[active].ProgramCounter];
                    Players[active].TotalCost += target.GetInstuctionCost();
                    UpdateCost(_turn);
                    if (Players[active].TotalCost <= _costLimit)
                    {
                        bool condition = true;
                        condition = RunInstrucion(active, target);
                        Players[active].ProgramCounter = Players[active].Code.Next(condition);
                    }
                    else
                    {
                        Players[active].CurrentHP -= 5;
                        UpdateHP();
                        if (Players[active].CurrentHP <= 0)
                        {
                            _endGame = true;
                            _winner = (active == 0) ? false : true;
                        }
                        Players[active].ProgramCounter = Players[active].Code.Size;
                    }
                    yield return new WaitForSeconds(1f);
                } while (Players[active].Code[Players[active].ProgramCounter] != null && !(target.Type == InstructionType.Move || target.Type == InstructionType.Attack) && !_endGame);
            }
            _turn = !_turn;
        }
        _round++;
        _battleEnd = true;
    }

    private bool RunInstrucion(int active, Instruction target)
    {
        int s1, s2;
        switch (target.Type)
        {
            case InstructionType.Move:
                if (_neighbor[Players[active].Pos, target.Arguments[0]] != Players[1 - active].Pos)
                {
                    Players[active].Pos = _neighbor[Players[active].Pos, target.Arguments[0]];
                    UpdateLocation();
                }
                return true;
            case InstructionType.Attack:
                bool near = false;
                for (int i = 0; i < 4; i++)
                    if (Players[active].Pos == _neighbor[Players[1 - active].Pos, i])
                        near = true;
                if (near)
                {
                    Players[1 - active].CurrentHP -= (int)((Players[active].Attack_Mag * 5f) * (10f / (10f + Players[1 - active].Attack_Def)) + Players[active].Ability_Mag * (Players[active].Food[Players[active].FoodCounter++] + _round) / 3f * (10f / (10f + Players[1 - active].Ability_Def)));
                    if (_difficulty == DifficultyType.Hard)
                    {
                        if (IsSorted(active)) Players[1 - active].CurrentHP -= 25;
                    }
                    Players[active].FoodCounter %= 10;
                    UpdateHP();
                    if (Players[1 - active].CurrentHP <= 0)
                    {
                        _endGame = true;
                        _winner = (active == 0) ? true : false;
                    }
                }
                return true;
            case InstructionType.Assign:
                if (target.Arguments[1] != 6)
                {
                    if (target.Arguments[1] == 0) s1 = 0;
                    else s1 = Players[active].Variable[target.Arguments[1] - 1];
                    if (target.Arguments[2] == 0) s2 = target.Arguments[3];
                    else s2 = Players[active].Variable[target.Arguments[2] - 1];
                    switch (target.Arguments[0])
                    {
                        case 0: // +
                            Players[active].Variable[target.Arguments[4]] = s1 + s2;
                            break;
                        case 1: // -
                            Players[active].Variable[target.Arguments[4]] = s1 - s2;
                            break;
                        case 2: // *
                            Players[active].Variable[target.Arguments[4]] = s1 * s2;
                            break;
                        case 3: // /
                            if (s2 != 0)
                                Players[active].Variable[target.Arguments[4]] = s1 / s2;
                            break;
                        case 4: // %
                            if (s2 != 0)
                                Players[active].Variable[target.Arguments[4]] = s1 % s2;
                            break;
                    }
                }
                else
                {
                    if (target.Arguments[2] == 0)
                    {
                        s2 = target.Arguments[3];
                        if (target.Arguments[3] > 9) s2 = 9;
                        else if (target.Arguments[3] < 0) s2 = 0;
                        Players[active].Variable[target.Arguments[4]] = Players[active].Food[s2];
                    }
                    else
                    {
                        s2 = Players[active].Variable[target.Arguments[2] - 1];
                        if (Players[active].Variable[target.Arguments[2] - 1] > 9) s2 = 9;
                        else if (Players[active].Variable[target.Arguments[2] - 1] < 0) s2 = 0;
                        Players[active].Variable[target.Arguments[4]] = Players[active].Food[s2];
                    }
                }
                if (Players[active].Variable[target.Arguments[4]] > 999) Players[active].Variable[target.Arguments[4]] = 999;
                if (Players[active].Variable[target.Arguments[4]] < -99) Players[active].Variable[target.Arguments[4]] = -99;
                UpdateVariable();
                return true;
            case InstructionType.If:
                if (target.Arguments[1] == 0) s1 = _distance[Players[0].Pos, Players[1].Pos];
                else if (target.Arguments[1] == 1) s1 = Players[active].CurrentHP;
                else if (target.Arguments[1] == 2) s1 = Players[1 - active].CurrentHP;
                else s1 = Players[active].Variable[target.Arguments[1] - 3];
                if (target.Arguments[2] == 0) s2 = target.Arguments[3];
                else s2 = Players[active].Variable[target.Arguments[2] - 1];
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
            case InstructionType.Loop:
                if (target.Arguments[1] == 0) s1 = _distance[Players[0].Pos, Players[1].Pos];
                else if (target.Arguments[1] == 1) s1 = Players[active].CurrentHP;
                else if (target.Arguments[1] == 2) s1 = Players[1 - active].CurrentHP;
                else s1 = Players[active].Variable[target.Arguments[1] - 3];
                if (target.Arguments[2] == 0) s2 = target.Arguments[3];
                else s2 = Players[active].Variable[target.Arguments[2] - 1];
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
                else s1 = Players[active].Variable[target.Arguments[0] - 1];
                if (target.Arguments[2] == 0) s2 = target.Arguments[3];
                else s2 = Players[active].Variable[target.Arguments[2] - 1];
                if (s1 > 9) s1 = 9;
                else if (s1 < 0) s1 = 0;
                if (s2 > 9) s2 = 9;
                else if (s2 < 0) s2 = 0;
                int tmp = Players[active].Food[s1];
                Players[active].Food[s1] = Players[active].Food[s2];
                Players[active].Food[s2] = tmp;
                UpdateFood();
                return true;
        }
        return true;
    }

    public void UpdateCode(ushort num)
    {
        foreach (Transform child in Code_Area[num].transform) Destroy(child.gameObject);
        ushort programCounter = 0;
        while (Players[num].Code[programCounter] != null)
        {
            GameObject instruction;
            Instruction target = Players[num].Code[programCounter];
            switch (target.Type)
            {
                case InstructionType.Move:
                    switch (_difficulty)
                    {
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
                    if (_difficulty == DifficultyType.Hard)
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
            rect.sizeDelta = new Vector2(400 + 240 * Players[num].Code.GetLevel(programCounter), rect.sizeDelta.y);
            instruction.transform.SetParent(Code_Area[num].transform, false);
            programCounter++;
        }
        //_Players[num].code.Display();
    }

    private void UpdateCost(bool num)
    {
        if (!num)
            PlayerCode.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = Players[0].TotalCost.ToString() + " / " + _costLimit.ToString();
        else
            EnemyCode.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = Players[1].TotalCost.ToString() + " / " + _costLimit.ToString();
    }

    private void UpdateLocation()
    {
        Character1.GetComponent<RectTransform>().anchoredPosition = new Vector2(_location[Players[0].Pos, 0], _location[Players[0].Pos, 1]);
        Character2.GetComponent<RectTransform>().anchoredPosition = new Vector2(_location[Players[1].Pos, 0], _location[Players[1].Pos, 1]);
    }

    private void UpdateHP()
    {
        PlayerHP.transform.GetChild(0).GetComponent<Text>().text = "HP: " + Players[0].CurrentHP.ToString();
        EnemyHP.transform.GetChild(0).GetComponent<Text>().text = "HP: " + Players[1].CurrentHP.ToString();
    }

    private void UpdateVariable()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerVariable.transform.GetChild(i).GetComponent<Text>().text = Players[0].Variable[i].ToString();
            EnemyVariable.transform.GetChild(i).GetComponent<Text>().text = Players[1].Variable[i].ToString();
        }
    }

    private void UpdateFood()
    {
        for (int i = 0; i < 10; i++)
        {
            PlayerFood.transform.GetChild(i).GetComponent<Text>().text = Players[0].Food[i].ToString();
            EnemyFood.transform.GetChild(i).GetComponent<Text>().text = Players[1].Food[i].ToString();
        }
    }

    private bool IsSorted(int num)
    {
        for (int i = 0; i < 9; i++)
        {
            if (Players[num].Food[i] > Players[num].Food[i + 1])
                return false;
        }
        return true;
    }

    public int IncPurchaseCount()
    {
        if (_purchaseCount < 5) _purchaseCount++;
        else return -1;
        Purchase.transform.GetChild(0).GetComponent<Text>().text = _purchaseCount.ToString() + " / 5";
        return _purchaseCount;
    }
}