using System.Collections;
using System.Collections.Generic;

public enum CharacterType:ushort {Owl, Koala, Kangaroo, Whale, Fox};

public class Character {
    private CharacterType Type;
    private int HP;
    private int[] Food = new int[9];
    private int Attack_Mag;
    private int Ability_Mag;
    private int Attack_Def;
    private int Ability_Def;
    private int Speed;

    public Character(CharacterType type) {
        Type = type;
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
