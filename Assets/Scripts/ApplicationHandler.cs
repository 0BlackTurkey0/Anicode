using System.Collections;
using System.Collections.Generic;
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

    void Awake() {
        DontDestroyOnLoad(gameObject);
        _gameData = new GameData();
        _gameData.LoadData();
        _gameData.SaveData();
        CharaType = new CharacterType[2];
    }

    void Start() {
        Screen.fullScreen = _gameData.IsFullScreen;
        SceneManager.LoadScene(0);
    }
    
    void Update() {
        if (!isActiveAI && SceneManager.GetActiveScene().name == "Battle") {
            gameObject.transform.GetChild(1).GetChild(_challenge).gameObject.SetActive(true);
            isActiveAI = true;
        }
        if (isActiveAI && SceneManager.GetActiveScene().name != "Battle") {
            gameObject.transform.GetChild(1).GetChild(_challenge).gameObject.SetActive(false);
            isActiveAI = false;
        }
        gameObject.transform.GetChild(2).GetComponent<AudioSource>().volume = _gameData.VoiceVolume;
    }
}
