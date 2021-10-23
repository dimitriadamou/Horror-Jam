using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Game State", menuName = "Horror Jam/SO/Game State", order = 0)]
public class GameState : ScriptableObject {
    public event UnityAction <bool> GamePauseChanged = delegate {};
    public event UnityAction OnGameLose = delegate {};
    public event UnityAction OnGameWin = delegate {};

    public bool _paused = true;
    public bool Paused {
        set {
            _paused = value;
            GamePauseChanged.Invoke(value);
        }
        get {
            return this._paused;
        }
    }
    public bool Intro = false; 

    public void LoseGame()
    {
        OnGameLose.Invoke();
    }
    public void WinGame()
    {
        OnGameWin.Invoke();
    }
}