using System;

public class Data
{
    public string Type { get; set; }
    public string Name { get; set; }
    public DateTime Time { get; set; }
    public int Rank { get; set; }
    public GameMode Mode { get; set; } = new GameMode();
    public string Code { get; set; }
}

public class SYS    //系統狀態
{
    public const string CHALLENGE = "CHALLENGE";
    public const string ACCEPT = "ACCEPT";
    public const string DENY = "DENY";
    public const string GAME = "GAME";
}

public class MSG    //封包類型
{
    public const string REQUEST = "REQ";        //廣播
    public const string RESPONSE = "RES";       //回應
    public const string STATUS = "STA";         //狀態
    public const string CHALLENGE = "CHA";      //發起挑戰
    public const string ACCEPT = "ACC";         //接受挑戰
    public const string DENY = "DEN";           //拒絕挑戰
    public const string CONNECT = "CON";        //確認對手連線狀態
    public const string MODE = "MOD";           //戰鬥前設定
    public const string GAME = "GAM";           //遊戲數據
}

public class GameMode
{
    public bool[] Difficulty { get; set; } = new bool[4];    //簡單:0 初級:1 中級:2 困難:3
    public string Character { get; set; } = null;
}