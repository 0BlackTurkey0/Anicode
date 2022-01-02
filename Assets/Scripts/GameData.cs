using System;
using System.IO;
using System.Xml.Serialization;

public class GameData {
    private string _name;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    private DifficultyType _rank;

    public DifficultyType Rank {
        get { return _rank; }
        set { _rank = value; }
    }

    private int _money;

    public int Money {
        get { return _money; }
        set { _money = value; }
    }

    private bool[] _achievements;

    public bool[] Achievements {
        get { return _achievements; }
        set { _achievements = value; }
    }

    private bool[] _skins;

    public bool[] Skins {
        get { return _skins; }
        set { _skins = value; }
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

    private int _schedule_Single;

    public int Schedule_Single {
        get { return _schedule_Single; }
        set { _schedule_Single = value; }
    }

    //單人2種進度

    public GameData()
    {
        _name = "Guest";
        _rank = DifficultyType.NULL;
        _money = 0;
        _achievements = new bool[14];
        _skins = new bool[15];
        _isFullScreen = true;
        _voiceVolume = 1f;
        _isIntro_Single = new bool[4];
        _schedule_Simple = 0;
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
        if (gameData.Name.Length <= 6)
            _name = gameData.Name;
        if (Enum.IsDefined(typeof(DifficultyType), gameData.Rank))
            _rank = gameData.Rank;
        if (gameData.Money >= 0)
            _money = gameData.Money;
        if (gameData.Achievements.Length <= 14)
            _achievements = gameData.Achievements;
        if (gameData.Skins.Length <= 15)
            _skins = gameData.Skins;
        if (gameData.VoiceVolume >= 0 && gameData.VoiceVolume <= 1)
            _voiceVolume = gameData.VoiceVolume;
        _isIntro_Single = gameData.IsIntro_Single;
        if (gameData.Schedule_Simple >= 0 && gameData.Schedule_Simple <= 4)
            _schedule_Simple = gameData.Schedule_Simple;
        if (gameData.Schedule_Single >= 0 && gameData.Schedule_Single < (1 << 17))
            _schedule_Single = gameData.Schedule_Single;
    }
}
