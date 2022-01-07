using System;
using System.IO;
using System.Xml.Serialization;

public class GameData {
    private string _name;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public DifficultyType Rank {
        get {
            if ((_schedule_Single & (1 << 16)) == (1 << 16))
                return DifficultyType.Hard;
            else if ((_schedule_Single & (1 << 12)) == (1 << 12))
                return DifficultyType.Normal;
            else if ((_schedule_Single & (1 << 8)) == (1 << 8))
                return DifficultyType.Easy;
            else if ((_schedule_Single & (1 << 4)) == (1 << 4))
                return DifficultyType.Start;
            else
                return DifficultyType.NULL;
        }
    }

    private int _money;

    public int Money {
        get { return _money; }
        set { _money = value; }
    }

    private bool[] _items;

    public bool[] Items
    {
        get { return _items; }
        set { _items = value; }
    }

    private bool _isFullScreen;

    public bool IsFullScreen {
        get { return _isFullScreen; }
        set { _isFullScreen = value; }
    }

    private float _voiceVolume;

    public float VoiceVolume {
        get { return _voiceVolume; }
        set { _voiceVolume = value; }
    }

    private bool _firstIntro_Single;
    public bool FirstIntro_Single
    {
        get { return _firstIntro_Single; }
        set { _firstIntro_Single = value; }
    }

    private bool[] _isIntro_Single;

    public bool[] IsIntro_Single {
        get { return _isIntro_Single; }
        set { _isIntro_Single = value; }
    }

    private int _schedule_Simple;

    public int Schedule_Simple {
        get { 
            if (_schedule_Simple == 4)
                return 3;
            return _schedule_Simple;
        }
        set { _schedule_Simple = value; }
    }

    private int _schedule_SimpleChange;

    public int Schedule_SimpleChange
    {
        get { return _schedule_SimpleChange; }
        set { _schedule_SimpleChange = value; }
    }

    private bool _iswinForSimple; //判斷是否打贏AI

    public bool IswinForSimple
    {
        get { return _iswinForSimple; }
        set { _iswinForSimple = value; }
    }

    private bool _simpleIsFinish; //判斷簡單AI是否完成過一輪

    public bool SimpleIsFinish
    {
        get { return _simpleIsFinish; }
        set { _simpleIsFinish = value; }
    }

    private int _schedule_Single;

    public int Schedule_Single {
        get { return _schedule_Single; }
        set { _schedule_Single = value; }
    }

    //單人2種進度

    public GameData()
    {
        _name = "Guest";
        _money = 0;
        _items = new bool[34];
        _isFullScreen = true;
        _voiceVolume = 1f;
        _firstIntro_Single = true;
        _isIntro_Single = new bool[4] { true, true, true, true };
        _schedule_Simple = 0;
        _schedule_SimpleChange = 0;
        _iswinForSimple = false;
        _schedule_Single = 0;
    }

    public void SaveData()
    {
        using (StreamWriter writer = new StreamWriter("./GameData.ac")) {
            var serializer = new XmlSerializer(GetType());
            serializer.Serialize(writer, this);
        }
    }

    public void LoadData()
    {
        using (StreamReader reader = new StreamReader("./GameData.ac")) {
            var serializer = new XmlSerializer(GetType());
            GameData gameData = serializer.Deserialize(reader) as GameData;
            UpdateData(gameData);
        }
    }

    private void UpdateData(GameData gameData)
    {
        if (gameData.Name.Length >= 1 && gameData.Name.Length <= 8)
            _name = gameData.Name;
        if (gameData.Money >= 0)
            _money = gameData.Money;
        if (gameData.Items.Length <= 34)
            Array.Copy(gameData.Items, _items, 34);
        if (gameData.VoiceVolume >= 0 && gameData.VoiceVolume <= 1)
            _voiceVolume = gameData.VoiceVolume;
        _firstIntro_Single = gameData.FirstIntro_Single;
        Array.Copy(gameData.IsIntro_Single, _isIntro_Single, 4);
        if (gameData.Schedule_Simple >= 0 && gameData.Schedule_Simple <= 4)
            _schedule_Simple = gameData.Schedule_Simple;
        if (gameData.Schedule_SimpleChange >= 0 && gameData.Schedule_SimpleChange <= 22)
            _schedule_SimpleChange = gameData.Schedule_SimpleChange;
        _iswinForSimple = gameData.IswinForSimple;
        _simpleIsFinish = gameData.SimpleIsFinish;
        if (gameData.Schedule_Single >= 0 && gameData.Schedule_Single < (1 << 17))
            _schedule_Single = gameData.Schedule_Single;
    }
}
