using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    private enum DifficultyType:ushort {Start, Easy, Normal, Hard};

    private Character[] Players;
    private DifficultyType Difficulty; 

    void Start() {
        Players = new Character[2];
    }
    
    void Update() {
        
    }
}
