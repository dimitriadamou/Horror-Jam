// GENERATED AUTOMATICALLY FROM 'Assets/Input/Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""b9846749-d6dd-4938-bba9-f3e693028d20"",
            ""actions"": [
                {
                    ""name"": ""AnyKey"",
                    ""type"": ""Button"",
                    ""id"": ""1a3f8e20-829f-4373-a2e9-945dd57d3ec2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Space"",
                    ""type"": ""Button"",
                    ""id"": ""ec57cee6-3a28-4b77-8e0f-67f2c1a4caa1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Backspace"",
                    ""type"": ""Button"",
                    ""id"": ""41173644-b14d-4f7a-9d5d-0e9f9442d879"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shift"",
                    ""type"": ""Button"",
                    ""id"": ""b939e537-582f-46e1-ab9d-84d97cea5776"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cf6574a1-dc04-4f21-82dc-4000e32e91f9"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b7d0d22-c3ac-4e3f-9fe3-6da56ad7c4e4"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Space"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2db33131-329f-4823-a05b-5178eec87fc9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Space"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""54ab6f30-9177-437e-b438-722355d278cb"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Backspace"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88d0a5e6-d7b1-464b-83b5-c52a1605304c"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Shift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // GamePlay
        m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
        m_GamePlay_AnyKey = m_GamePlay.FindAction("AnyKey", throwIfNotFound: true);
        m_GamePlay_Space = m_GamePlay.FindAction("Space", throwIfNotFound: true);
        m_GamePlay_Backspace = m_GamePlay.FindAction("Backspace", throwIfNotFound: true);
        m_GamePlay_Shift = m_GamePlay.FindAction("Shift", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // GamePlay
    private readonly InputActionMap m_GamePlay;
    private IGamePlayActions m_GamePlayActionsCallbackInterface;
    private readonly InputAction m_GamePlay_AnyKey;
    private readonly InputAction m_GamePlay_Space;
    private readonly InputAction m_GamePlay_Backspace;
    private readonly InputAction m_GamePlay_Shift;
    public struct GamePlayActions
    {
        private @GameInput m_Wrapper;
        public GamePlayActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @AnyKey => m_Wrapper.m_GamePlay_AnyKey;
        public InputAction @Space => m_Wrapper.m_GamePlay_Space;
        public InputAction @Backspace => m_Wrapper.m_GamePlay_Backspace;
        public InputAction @Shift => m_Wrapper.m_GamePlay_Shift;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void SetCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
            {
                @AnyKey.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAnyKey;
                @AnyKey.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAnyKey;
                @AnyKey.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAnyKey;
                @Space.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSpace;
                @Space.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSpace;
                @Space.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSpace;
                @Backspace.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnBackspace;
                @Backspace.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnBackspace;
                @Backspace.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnBackspace;
                @Shift.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnShift;
                @Shift.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnShift;
                @Shift.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnShift;
            }
            m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AnyKey.started += instance.OnAnyKey;
                @AnyKey.performed += instance.OnAnyKey;
                @AnyKey.canceled += instance.OnAnyKey;
                @Space.started += instance.OnSpace;
                @Space.performed += instance.OnSpace;
                @Space.canceled += instance.OnSpace;
                @Backspace.started += instance.OnBackspace;
                @Backspace.performed += instance.OnBackspace;
                @Backspace.canceled += instance.OnBackspace;
                @Shift.started += instance.OnShift;
                @Shift.performed += instance.OnShift;
                @Shift.canceled += instance.OnShift;
            }
        }
    }
    public GamePlayActions @GamePlay => new GamePlayActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IGamePlayActions
    {
        void OnAnyKey(InputAction.CallbackContext context);
        void OnSpace(InputAction.CallbackContext context);
        void OnBackspace(InputAction.CallbackContext context);
        void OnShift(InputAction.CallbackContext context);
    }
}
