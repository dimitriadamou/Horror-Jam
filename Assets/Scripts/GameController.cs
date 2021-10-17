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
        OnWrongPress.Callback += OnWrongPressEvent;
    }

    private void OnDestroy() {
        OnWrongPress.Callback -= OnWrongPressEvent;
    }
    private void OnWrongPressEvent()
    {
        playerHealth.Value = playerHealth.Value - 5;
    }
}
