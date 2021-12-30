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

    public GameData() {
        _name = "Guest";
        _rank = DifficultyType.NULL;
        _money = 0;
        _achievements = new bool[14];
        _skins = new bool[15];
        _isFullScreen = true;
        _voiceVolume = 1f;
    }

    public void SaveData() {
        using (StreamWriter writer = new StreamWriter("./GameData.ac")) {
            var serializer = new XmlSerializer(GetType());
            serializer.Serialize(writer, this);
        }
    }

    public void LoadData() {
        using (StreamReader reader = new StreamReader("./GameData.ac")) {
            var serializer = new XmlSerializer(GetType());
            GameData gameData = (GameData)serializer.Deserialize(reader);
            UpdateData(gameData);
        }
    }

    private void UpdateData(GameData gameData) {
        if (gameData._name.Length <= 6)
            _name = gameData._name;
        _rank = gameData._rank;
        if (gameData._money >= 0)
            _money = gameData._money;
        if (gameData._achievements.Length <= 14)
            _achievements = gameData._achievements;
        if (gameData._skins.Length <= 15)
            _skins = gameData._skins;
        if (gameData._voiceVolume >= 0 && gameData._voiceVolume <= 1)
            _voiceVolume = gameData._voiceVolume;
    }
}
