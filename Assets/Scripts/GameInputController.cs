using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "GameInputController", menuName = "Horror Jam/SO/Game Input Controller", order = 0)]
public class GameInputController : ScriptableObject, GameInput.IGamePlayActions {
    private GameInput gameInput;
    
    public UnityAction<char> OnKeyPress = delegate { };
    private void OnEnable() {
        if(gameInput == null) {
            gameInput = new GameInput();

            gameInput.GamePlay.SetCallbacks(this);

            Keyboard.current.onTextInput += OnTextInput;

        }
    }

    public void EnableGameInput()
    {
        gameInput.GamePlay.Enable();
    }

    private void OnDisable()
    {

    }

    public void OnBackspace(InputAction.CallbackContext context)
    {

    }

    private void OnTextInput(char input) 
    {
        if(OnKeyPress != null) OnKeyPress.Invoke(input);
    }
    public void OnAnyKey(InputAction.CallbackContext context)
    {
        
    }

    public void OnSpace(InputAction.CallbackContext context)
    {

    }

    public void OnShift(InputAction.CallbackContext context)
    {

    }

}