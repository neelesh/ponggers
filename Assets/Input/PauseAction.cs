// GENERATED AUTOMATICALLY FROM 'Assets/Input/PauseAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PauseAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PauseAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PauseAction"",
    ""maps"": [
        {
            ""name"": ""PauseActionMap"",
            ""id"": ""65629f15-7441-4266-ae11-d08578aaa2be"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""8e0afa6f-45cb-4461-b508-02bd0897146e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d6e23725-df13-49d6-82e1-be77844ee65f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3f71f3a-0a55-4300-9903-4bc0c5817fe1"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PauseActionMap
        m_PauseActionMap = asset.FindActionMap("PauseActionMap", throwIfNotFound: true);
        m_PauseActionMap_Pause = m_PauseActionMap.FindAction("Pause", throwIfNotFound: true);
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

    // PauseActionMap
    private readonly InputActionMap m_PauseActionMap;
    private IPauseActionMapActions m_PauseActionMapActionsCallbackInterface;
    private readonly InputAction m_PauseActionMap_Pause;
    public struct PauseActionMapActions
    {
        private @PauseAction m_Wrapper;
        public PauseActionMapActions(@PauseAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_PauseActionMap_Pause;
        public InputActionMap Get() { return m_Wrapper.m_PauseActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseActionMapActions set) { return set.Get(); }
        public void SetCallbacks(IPauseActionMapActions instance)
        {
            if (m_Wrapper.m_PauseActionMapActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_PauseActionMapActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PauseActionMapActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PauseActionMapActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PauseActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PauseActionMapActions @PauseActionMap => new PauseActionMapActions(this);
    public interface IPauseActionMapActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
}
