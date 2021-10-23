using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameController : MonoBehaviour
{
    [SerializeField] GameInputController inputController;
    [SerializeField] SharedInt playerHealth;

    [SerializeField] SpriteRenderer atticShadow;
    [SerializeField] TMPro.TMP_Text textTyper;
    [SerializeField] TMPro.TMP_Text hajimimashita;
    [SerializeField] TMPro.TMP_Text targetText;
    [SerializeField] NoArgEvent OnWrongPress;

    [SerializeField] GameState gameState;
    [SerializeField] PlayableDirector playableDirector;

    [SerializeField] GameObject loseUI;
    [SerializeField] GameObject winUI;
    private void Awake() {
        playerHealth.Value = 100; //default to 100.
        OnWrongPress.Callback += OnWrongPressEvent;
        gameState.GamePauseChanged += OnGamePauseChange;
        gameState.OnGameLose += OnGameLose;
        gameState.OnGameWin += OnGameWin;
    }

    public void OnGameLose()
    {
        gameState.Paused = true;
        loseUI.SetActive(true);
    }

    public void OnGameWin()
    {
        gameState.Paused = true;
        winUI.SetActive(true);
    }

    public void OnGoToBed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void OnGamePauseChange(bool state)
    {
        if(!state) {
            hajimimashita.gameObject.SetActive(false);
            GameObject.Destroy(hajimimashita);
            playableDirector.gameObject.SetActive(true);
            
            playableDirector.Play();
            atticShadow.color = new Color(
                atticShadow.color.r,                
                atticShadow.color.g,
                atticShadow.color.b,
                0  
            );
        }

        if(state) {
            playableDirector.Pause();
        }
    }
    private void OnDestroy() {
        OnWrongPress.Callback -= OnWrongPressEvent;
        gameState.GamePauseChanged -= OnGamePauseChange;
    }
    private void OnWrongPressEvent()
    {
        playerHealth.Value = playerHealth.Value - 5;
    }
}
