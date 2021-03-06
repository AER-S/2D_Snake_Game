//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.1.1
//     from Assets/InputMaster.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputMaster : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Snake"",
            ""id"": ""91535858-5c34-41eb-94b0-a56f7f7613a9"",
            ""actions"": [
                {
                    ""name"": ""Movments"",
                    ""type"": ""Value"",
                    ""id"": ""4805968c-ba27-4c85-9c86-c4740996e429"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""23ebcd64-ac80-487d-910f-8d1cc27ef314"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""DPAD"",
                    ""id"": ""d2a36103-7ccb-4b34-b297-196436247e23"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movments"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""48858174-0678-47fc-9fed-dd866eb26c0f"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movments"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5753680f-0384-46a7-b6c7-27dff25f2b38"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movments"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""10617c2d-1a99-411f-83fc-e6f52c3f0ad5"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movments"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""eb95ac7b-f644-402e-8943-1dc6b3cfaf60"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movments"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ZQSD"",
                    ""id"": ""dae6e55a-9bc9-40b1-9633-1b046e2a41ae"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movments"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""bdd96144-f0e4-4867-aaf1-c9d015769a3e"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movments"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4e6ba1b3-cbce-472b-bfc8-fc4a80c747f1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movments"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""89db3bcd-1340-4d27-9545-fe235a33a407"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movments"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""498826a3-a63f-4a65-bf09-cd0b102aa2e3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movments"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Two Modifiers"",
                    ""id"": ""05f9f4ce-2796-47ba-80bd-6f89305f9352"",
                    ""path"": ""TwoModifiers"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movments"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0f29432-51d1-4fb7-9351-d908cdca768e"",
                    ""path"": ""<Keyboard>/#(P)"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pause"",
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
        // Snake
        m_Snake = asset.FindActionMap("Snake", throwIfNotFound: true);
        m_Snake_Movments = m_Snake.FindAction("Movments", throwIfNotFound: true);
        m_Snake_Pause = m_Snake.FindAction("Pause", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Snake
    private readonly InputActionMap m_Snake;
    private ISnakeActions m_SnakeActionsCallbackInterface;
    private readonly InputAction m_Snake_Movments;
    private readonly InputAction m_Snake_Pause;
    public struct SnakeActions
    {
        private @InputMaster m_Wrapper;
        public SnakeActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movments => m_Wrapper.m_Snake_Movments;
        public InputAction @Pause => m_Wrapper.m_Snake_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Snake; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SnakeActions set) { return set.Get(); }
        public void SetCallbacks(ISnakeActions instance)
        {
            if (m_Wrapper.m_SnakeActionsCallbackInterface != null)
            {
                @Movments.started -= m_Wrapper.m_SnakeActionsCallbackInterface.OnMovments;
                @Movments.performed -= m_Wrapper.m_SnakeActionsCallbackInterface.OnMovments;
                @Movments.canceled -= m_Wrapper.m_SnakeActionsCallbackInterface.OnMovments;
                @Pause.started -= m_Wrapper.m_SnakeActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_SnakeActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_SnakeActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_SnakeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movments.started += instance.OnMovments;
                @Movments.performed += instance.OnMovments;
                @Movments.canceled += instance.OnMovments;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public SnakeActions @Snake => new SnakeActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface ISnakeActions
    {
        void OnMovments(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
