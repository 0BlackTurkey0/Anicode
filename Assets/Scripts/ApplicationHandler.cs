using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationHandler : MonoBehaviour {
    private GameData _gameData;
    public GameData GameData {
        get { return _gameData; }
        set { _gameData = value; }
    }

    private bool _isDuel;
    public bool IsDuel {
        get { return _isDuel; }
        set { _isDuel = value; }
    }

    private int _challenge;
    public int Challenge {
        get { return _challenge; }
        set { _challenge = value; }
    }

    private CharacterType[] _charaType;
    public CharacterType[] CharaType {
        get { return _charaType; }
        set { _charaType = value; }
    }

    private DifficultyType _diffiType;
    public DifficultyType DiffiType {
        get { return _diffiType; }
        set { _diffiType = value; }
    }

    private bool isActiveAI;
    public bool IsActiveAI {
        get { return isActiveAI; }
        set { isActiveAI = value; }
    }

    private bool _isSimple; //判斷簡單還是劇情
    public bool IsSimple {
        get { return _isSimple; }
        set { _isSimple = value; }
    }

    private int _isSimpleSchedule;
    public int IsSimpleSchedule {
        get { return _isSimpleSchedule; }
        set { _isSimpleSchedule = value; }
    }

    private int _isschedule_SimpleChange;

    public int IsSchedule_SimpleChange {
        get { return _isschedule_SimpleChange; }
        set { _isschedule_SimpleChange = value; }
    }

    private int[] _characProperty;

    public int[] CharacProperty {
        get { return _characProperty; }
        set { _characProperty = value; }
    }

    private string _missonText;

    public string MissonText {
        get { return _missonText; }
        set { _missonText = value; }
    }

    void Awake() {
        DontDestroyOnLoad(gameObject);
        _gameData = new GameData();
        FileInfo file = new FileInfo(Application.dataPath + "/GameData.ac");
        if (file.Exists)
            _gameData.LoadData();
        else
            _gameData.SaveData();
        _charaType = new CharacterType[2];
    }

    void Start() {
        if (_gameData.IsFullScreen) Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        else Screen.fullScreenMode = FullScreenMode.Windowed;
        gameObject.transform.GetChild(2).GetComponent<AudioSource>().volume = _gameData.VoiceVolume;
        _isDuel = false;
        SceneManager.LoadScene("Lobby");
    }

    void Update() {
        if (!_isDuel) {
            if (IsSimple) {
                if (!isActiveAI && SceneManager.GetActiveScene().name == "Battle") {
                    if (_gameData.Schedule_Simple == 1 && _gameData.Schedule_SimpleChange == 2) {
                        gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        _missonText = "在四回合內，拖曳一個\"Move往上\"的指令至左側";
                    }
                    else if (_gameData.Schedule_Simple == 1 && _gameData.Schedule_SimpleChange == 6) {
                        gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                        _missonText = "在四回合內，拖曳一個\"Move往下\"的指令至左側";
                    }
                    else if (_gameData.Schedule_Simple == 1 && _gameData.Schedule_SimpleChange == 9) {
                        gameObject.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                        _missonText = "在四回合內，拖曳一個\"Move往左\"的指令至左側";
                    }
                    else if (_gameData.Schedule_Simple == 1 && _gameData.Schedule_SimpleChange == 12) {
                        gameObject.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
                        _missonText = "在四回合內，拖曳一個\"Move往右\"的指令至左側";
                    }
                    else if (_gameData.Schedule_Simple == 1 && _gameData.Schedule_SimpleChange == 16) {
                        gameObject.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
                        _missonText = "在四回合內，移動至對手旁邊並使用\"Attack\"的指令";
                    }
                    else if (_gameData.Schedule_Simple == 2 && _gameData.Schedule_SimpleChange == 3) {
                        gameObject.transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
                        _missonText = "在四回合內，拖曳一個\"Assign0+C=V1\"的指令至左側，用來將數值賦予進飼料罐";
                    }
                    else if (_gameData.Schedule_Simple == 2 && _gameData.Schedule_SimpleChange == 8) {
                        gameObject.transform.GetChild(0).GetChild(6).gameObject.SetActive(true);
                        _missonText = "在四回合內，拖曳一個\"AssignV1+V2=V3\"的指令至左側，用來對飼料罐的數值進行操作";
                    }
                    else if (_gameData.Schedule_Simple == 2 && _gameData.Schedule_SimpleChange == 11) {
                        gameObject.transform.GetChild(0).GetChild(7).gameObject.SetActive(true);
                        _missonText = "在四回合內，使用\"If我方生命<C\"的指令，並在If成立時使用\"AssignV2+C=V2\"的指令";
                    }
                    else if (_gameData.Schedule_Simple == 2 && _gameData.Schedule_SimpleChange == 15) {
                        gameObject.transform.GetChild(0).GetChild(8).gameObject.SetActive(true);
                        _missonText = "在四回合內，使用\"If雙方距離>C\"的指令，並在If成立時使用\"Move往左\"的指令";
                    }
                    else if (_gameData.Schedule_Simple == 2 && _gameData.Schedule_SimpleChange == 19) {
                        gameObject.transform.GetChild(0).GetChild(9).gameObject.SetActive(true);
                        _missonText = "在四回合內，使用\"IfV2<C\"的指令，並在If成立時使用\"AssignV2+C=V2\"和\"Move往右\"的指令";
                    }
                    else if (_gameData.Schedule_Simple == 3 && _gameData.Schedule_SimpleChange == 2) {
                        gameObject.transform.GetChild(0).GetChild(10).gameObject.SetActive(true);
                        _missonText = "在四回合內，使用\"LoopV3<3\"的指令，並在Loop內使用\"AssignV3+2=V3\"的指令";
                    }
                    else if (_gameData.Schedule_Simple == 3 && _gameData.Schedule_SimpleChange == 7) {
                        gameObject.transform.GetChild(0).GetChild(11).gameObject.SetActive(true);
                        _missonText = "在四回合內，使用\"LoopV5<10\"的指令，並在Loop內使用\"Move紅色\"和\"Move藍色\"的指令";
                    }
                    else if (_gameData.Schedule_Simple == 3 && _gameData.Schedule_SimpleChange == 11) {
                        gameObject.transform.GetChild(0).GetChild(12).gameObject.SetActive(true);
                        _missonText = "在四回合內，使用\"LoopV2<2\"的指令，並在Loop內使用\"AssignV2+1=V2\"和\"Move黃色\"的指令";
                    }
                    else if (_gameData.Schedule_Simple == 3 && _gameData.Schedule_SimpleChange == 15) {
                        gameObject.transform.GetChild(0).GetChild(13).gameObject.SetActive(true);
                        _missonText = "在四回合內，使用\"If我方生命>V1\"的指令，並在If成立時使用\"Attack\"的指令";
                    }
                    else if (_gameData.Schedule_Simple == 3 && _gameData.Schedule_SimpleChange == 19) {
                        gameObject.transform.GetChild(0).GetChild(14).gameObject.SetActive(true);
                        _missonText = "在四回合內，先使用\"AssignV1+C=V1\"的指令，再使用\"Loop雙方距離<2\"的指令，並在Loop內使用\"Move綠色\"的指令";
                    }
                    isActiveAI = true;
                }
                else if (isActiveAI && SceneManager.GetActiveScene().name != "Battle") {
                    for (int i = 0;i < 15;i++)
                        gameObject.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
                    isActiveAI = false;
                }
            }
            else {
                if (!isActiveAI && SceneManager.GetActiveScene().name == "Battle") {
                    gameObject.transform.GetChild(1).GetChild(_challenge).gameObject.SetActive(true);
                    switch (_challenge + 1) {
                        case 1:
                            _missonText = "經過地圖上所有的點";
                            break;
                        case 2:
                            _missonText = "在六回合內，經過地圖上角落的四個點";
                            break;
                        case 3:
                            _missonText = "攻擊到對手五次";
                            break;
                        case 4:
                            _missonText = "取得勝利(將對手生命值歸零)，對手效果 : 每回合恢復100血量";
                            break;
                        case 5:
                            _missonText = "取得勝利(將對手生命值歸零)";
                            break;
                        case 6:
                            _missonText = "使用If和Assign指令各兩次";
                            break;
                        case 7:
                            _missonText = "使用Assign指令，Assign五個介於雙方血量數值之間的不同數";
                            break;
                        case 8:
                            _missonText = "取得勝利(將對手生命值歸零)，玩家效果 : 每回合開始時受到當前回合數*20的真實傷害";
                            break;
                        case 9:
                            _missonText = "取得勝利(將對手生命值歸零)";
                            break;
                        case 10:
                            _missonText = "使用雙層Loop指令";
                            break;
                        case 11:
                            _missonText = "四回合內共使用Loop指令十次";
                            break;
                        case 12:
                            _missonText = "取得勝利(將對手生命值歸零)，玩家效果 : 每次攻擊時扣除自身10%的生命值";
                            break;
                        case 13:
                            _missonText = "取得勝利(將對手生命值歸零)";
                            break;
                        case 14:
                            _missonText = "將飼料罐由小到大排列";
                            break;
                        case 15:
                            _missonText = "五回合內共使用Swap指令四次";
                            break;
                        case 16:
                            _missonText = "取得勝利(將對手生命值歸零)，對手效果 : 攻擊力隨生命值減少逐漸上升";
                            break;
                    }
                    isActiveAI = true;
                }
                else if (isActiveAI && SceneManager.GetActiveScene().name != "Battle") {
                    gameObject.transform.GetChild(1).GetChild(_challenge).gameObject.SetActive(false);
                    isActiveAI = false;
                }
            }
        }
    }
}