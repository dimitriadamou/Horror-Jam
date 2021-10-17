using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameInputController inputController;
    [SerializeField] SharedInt playerHealth;
    [SerializeField] TMPro.TMP_Text textTyper;
    [SerializeField] TMPro.TMP_Text targetText;
    [SerializeField] NoArgEvent OnWrongPress;

    private void Awake() {
        playerHealth.Value = 100; //default to 100.
    }

    private void OnEnable() {
        //inputController.EnableGameInput();
        UnityEngine.InputSystem.Keyboard.current.onTextInput += OnKeyPress;
        //inputController.OnKeyPress += OnKeyPress;
    }

    private void OnKeyPress(char key) 
    {
        if(targetText.text[0] == key) {
            targetText.text = targetText.text.Substring(1);
        } else {
            OnWrongPress.FireEvent();
            playerHealth.Value = playerHealth.Value - 5;
        }
    }
}
