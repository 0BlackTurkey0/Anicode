using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType:ushort {Owl, Koala, Kangaroo, Whale, Fox};

public class Character {
    private CharacterType Type;

    private int HP;
    public int hp {
        get { return HP; }
        set { HP = value; }
    }

    private int[] Variable;
    public int[] variable {
        get { return Variable; }
        set { Variable = value; }
    }

    private int[] Food;
    public int[] food {
        get { return food; }
        set { food = value; }
    }

    private int Attack_Mag;
    public int attack_mag {
        get { return Attack_Mag; }
        set { Attack_Mag = value; }
    }

    private int Ability_Mag;
    public int ability_mag {
        get { return Ability_Mag; }
        set { Ability_Mag = value; }
    }

    private int Attack_Def;
    public int attack_def {
        get { return Attack_Def; }
        set { Attack_Def = value; }
    }

    private int Ability_Def;
    public int ability_def {
        get { return Ability_Def; }
        set { Ability_Def = value; }
    }

    private int Speed;
    public int speed {
        get { return Speed; }
        set { Speed = value; }
    }

    private Code _code;
    public Code code {
        get { return _code; }
        set { _code = value; }
    }

    public Character(CharacterType type) {
        Type = type;
        _code = new Code();
        Variable = new int[5];
        Food = new int[9];
        switch (type) {
            case CharacterType.Owl:
                HP = 200;
                Attack_Mag = 10;
                Ability_Mag = 3;
                Attack_Def = 1;
                Ability_Def = 1;
                Speed = 10;
                break;
            case CharacterType.Koala:
                HP = 280;
                Attack_Mag = 3;
                Ability_Mag = 7;
                Attack_Def = 10;
                Ability_Def = 10;
                Speed = 3;
                break;
            case CharacterType.Kangaroo:
                HP = 250;
                Attack_Mag = 5;
                Ability_Mag = 5;
                Attack_Def = 7;
                Ability_Def = 5;
                Speed = 5;
                break;
            case CharacterType.Whale:
                HP = 300;
                Attack_Mag = 1;
                Ability_Mag = 1;
                Attack_Def = 15;
                Ability_Def = 8;
                Speed = 1;
                break;
            case CharacterType.Fox:
                HP = 210;
                Attack_Mag = 7;
                Ability_Mag = 10;
                Attack_Def = 3;
                Ability_Def = 3;
                Speed = 7;
                break;
        }
    }
}
