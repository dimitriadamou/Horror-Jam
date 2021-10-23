using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game State", menuName = "Horror Jam/SO/Game State", order = 0)]
public class GameState : ScriptableObject {
    public bool Paused = false;  
    public bool Intro = false; 
}