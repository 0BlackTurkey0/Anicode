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

    void Awake() {
        DontDestroyOnLoad(gameObject);
        _gameData = new GameData();
        _gameData.LoadData();
        _gameData.SaveData();
    }

    void Start() {
        Screen.fullScreen = _gameData.IsFullScreen;
        SceneManager.LoadScene(0);
    }
    
    void Update() {
        gameObject.transform.GetChild(2).GetComponent<AudioSource>().volume = _gameData.VoiceVolume;
    }
}
