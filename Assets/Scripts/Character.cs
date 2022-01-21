using System;

public enum CharacterType : ushort { Owl, Koala, Kangaroo, Whale, Fox, Customized };

public class Character {

    private CharacterType _type;
    public CharacterType Type {
        get { return _type; }
        set { _type = value; }
    }

    private int _pos;
    public int Pos {
        get { return _pos; }
        set { _pos = value; }
    }

    private int _currentHP;
    public int CurrentHP {
        get { return _currentHP; }
        set { _currentHP = value; }
    }

    private int[] _variable;
    public int[] Variable {
        get { return _variable; }
        set { _variable = value; }
    }

    private int[] _food;
    public int[] Food {
        get { return _food; }
        set { _food = value; }
    }

    private int _hp;
    public int Hp {
        get { return _hp; }
    }

    private int _attack_Mag;
    public int Attack_Mag {
        get { return _attack_Mag; }
    }

    private int _ability_Mag;
    public int Ability_Mag {
        get { return _ability_Mag; }
    }

    private int _attack_Def;
    public int Attack_Def {
        get { return _attack_Def; }
    }

    private int _ability_Def;
    public int Ability_Def {
        get { return _ability_Def; }
    }

    private int _speed;
    public int Speed {
        get { return _speed; }
    }

    private Code _code;
    public Code Code {
        get { return _code; }
        set { _code = value; }
    }

    private ushort _totalCost;
    public ushort TotalCost {
        get { return _totalCost; }
        set { _totalCost = value; }
    }

    private ushort _foodCounter;
    public ushort FoodCounter {
        get { return _foodCounter; }
        set { _foodCounter = value; }
    }

    private ushort _programCounter;
    public ushort ProgramCounter {
        get { return _programCounter; }
        set { _programCounter = value; }
    }

    public Character(CharacterType type, int[] states = null)
    {
        _type = type;
        _code = new Code();
        _variable = new int[5];
        _food = new int[10];
        _totalCost = 0;
        _foodCounter = 0;
        _programCounter = 0;
        switch (type) {
            case CharacterType.Owl:
                _hp = 200;
                _attack_Mag = 10;
                _ability_Mag = 3;
                _attack_Def = 1;
                _ability_Def = 1;
                _speed = 10;
                break;
            case CharacterType.Koala:
                _hp = 280;
                _attack_Mag = 3;
                _ability_Mag = 7;
                _attack_Def = 10;
                _ability_Def = 10;
                _speed = 3;
                break;
            case CharacterType.Kangaroo:
                _hp = 250;
                _attack_Mag = 5;
                _ability_Mag = 5;
                _attack_Def = 7;
                _ability_Def = 5;
                _speed = 5;
                break;
            case CharacterType.Whale:
                _hp = 300;
                _attack_Mag = 1;
                _ability_Mag = 1;
                _attack_Def = 15;
                _ability_Def = 8;
                _speed = 1;
                break;
            case CharacterType.Fox:
                _hp = 210;
                _attack_Mag = 7;
                _ability_Mag = 10;
                _attack_Def = 3;
                _ability_Def = 3;
                _speed = 7;
                break;
            case CharacterType.Customized:
                if (states.Length != 6) throw new ArgumentException("Pass parameter with wrong size in Character.cs");
                _hp = states[0];
                _attack_Mag = states[1];
                _ability_Mag = states[2];
                _attack_Def = states[3];
                _ability_Def = states[4];
                _speed = states[5];
                break;
        }
        _currentHP = _hp;
    }

    public void Reset()
    {
        _variable = new int[5];
        _food = new int[10];
        _totalCost = 0;
        _foodCounter = 0;
        _programCounter = 0;
        _code.Init();
    }
}