using System.Collections;
using System.Collections.Generic;
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
    public bool IsActiveAI
    {
        get { return isActiveAI; }
        set { isActiveAI = value; }
    }

    private bool _isSimple; //判斷簡單還是劇情
    public bool IsSimple
    {
        get { return _isSimple; }
        set { _isSimple = value; }
    }

    private int _isSimpleSchedule;
    public int IsSimpleSchedule
    {
        get { return _isSimpleSchedule; }
        set { _isSimpleSchedule = value; }
    }

    private int _isschedule_SimpleChange;

    public int IsSchedule_SimpleChange
    {
        get { return _isschedule_SimpleChange; }
        set { _isschedule_SimpleChange = value; }
    }

    private int[] _characProperty;

    public int[] CharacProperty
    {
        get { return _characProperty; }
        set { _characProperty = value; }
    }

    void Awake() {
        DontDestroyOnLoad(gameObject);
        _gameData = new GameData();
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/GameData.ac");
        if (dir.Exists)
            _gameData.LoadData();
        else
            _gameData.SaveData();
        _charaType = new CharacterType[2];
    }

    void Start() {
        Screen.fullScreen = _gameData.IsFullScreen;
        gameObject.transform.GetChild(2).GetComponent<AudioSource>().volume = _gameData.VoiceVolume;
        _isDuel = false;
        SceneManager.LoadScene("Lobby");
    }

    void Update()
    {
        if (!_isDuel)
        {
            if (IsSimple)
            {
                if (!isActiveAI && SceneManager.GetActiveScene().name == "Battle")
                {
                    if (_gameData.Schedule_Simple == 1 && _gameData.Schedule_SimpleChange == 2)
                    {
                        gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    }
                    else if (_gameData.Schedule_Simple == 1 && _gameData.Schedule_SimpleChange == 6)
                    {
                        gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                    }
                    else if (_gameData.Schedule_Simple == 1 && _gameData.Schedule_SimpleChange == 9)
                    {
                        gameObject.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                    }
                    else if (_gameData.Schedule_Simple == 1 && _gameData.Schedule_SimpleChange == 12)
                    {
                        gameObject.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
                    }
                    else if (_gameData.Schedule_Simple == 1 && _gameData.Schedule_SimpleChange == 16)
                    {
                        gameObject.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
                    }
                    else if (_gameData.Schedule_Simple == 2 && _gameData.Schedule_SimpleChange == 3)
                    {
                        gameObject.transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
                    }
                    else if (_gameData.Schedule_Simple == 2 && _gameData.Schedule_SimpleChange == 8)
                    {
                        gameObject.transform.GetChild(0).GetChild(6).gameObject.SetActive(true);
                    }
                    else if (_gameData.Schedule_Simple == 2 && _gameData.Schedule_SimpleChange == 11)
                    {
                        gameObject.transform.GetChild(0).GetChild(7).gameObject.SetActive(true);
                    }
                    else if (_gameData.Schedule_Simple == 2 && _gameData.Schedule_SimpleChange == 15)
                    {
                        gameObject.transform.GetChild(0).GetChild(8).gameObject.SetActive(true);
                    }
                    else if (_gameData.Schedule_Simple == 2 && _gameData.Schedule_SimpleChange == 19)
                    {
                        gameObject.transform.GetChild(0).GetChild(9).gameObject.SetActive(true);
                    }
                    else if (_gameData.Schedule_Simple == 3 && _gameData.Schedule_SimpleChange == 2)
                    {
                        gameObject.transform.GetChild(0).GetChild(10).gameObject.SetActive(true);
                    }
                    else if (_gameData.Schedule_Simple == 3 && _gameData.Schedule_SimpleChange == 7)
                    {
                        gameObject.transform.GetChild(0).GetChild(11).gameObject.SetActive(true);
                    }
                    else if (_gameData.Schedule_Simple == 3 && _gameData.Schedule_SimpleChange == 11)
                    {
                        gameObject.transform.GetChild(0).GetChild(12).gameObject.SetActive(true);
                    }
                    else if (_gameData.Schedule_Simple == 3 && _gameData.Schedule_SimpleChange == 15)
                    {
                        gameObject.transform.GetChild(0).GetChild(13).gameObject.SetActive(true);
                    }
                    else if (_gameData.Schedule_Simple == 3 && _gameData.Schedule_SimpleChange == 19)
                    {
                        gameObject.transform.GetChild(0).GetChild(14).gameObject.SetActive(true);
                    }
                    isActiveAI = true;
                }
                else if (isActiveAI && SceneManager.GetActiveScene().name != "Battle")
                {
                    for (int i = 0; i < 15; i++)
                        gameObject.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
                    isActiveAI = false;
                }
            }
            else
            {
                if (!isActiveAI && SceneManager.GetActiveScene().name == "Battle")
                {
                    gameObject.transform.GetChild(1).GetChild(_challenge).gameObject.SetActive(true);
                    isActiveAI = true;
                }
                else if (isActiveAI && SceneManager.GetActiveScene().name != "Battle")
                {
                    gameObject.transform.GetChild(1).GetChild(_challenge).gameObject.SetActive(false);
                    isActiveAI = false;
                }
            }
        }
    }
}
