// GENERATED AUTOMATICALLY FROM 'Assets/PlayerActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""2c26f9dd-d7d8-45d2-b29e-80f0ab5d20b1"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""42b339dd-88d2-4a94-8e06-a27f5cf4888a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""ef16ea1c-d6fe-43fb-aacf-96c4f1375bea"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3a0abad7-4257-42b1-807f-87af6104e4fc"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""252b05ba-979b-48e1-b418-7ed58899b0ee"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3e0ae67e-95d0-4bb7-bc88-ab10839a848f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""93b9dd42-bee2-4102-b0ab-520302a119a4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""047a8ab9-e1eb-46f8-b337-14c9b1338e1b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b1f728f9-9730-4f41-8ebf-808b0fbe131a"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3e4468f3-6a7a-4f51-a132-97616c30a30c"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e6fa8fa1-2b5f-4d68-9024-b47b896deec2"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""be66b094-3374-49de-9c0d-1eb66099e15b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Inventory"",
            ""id"": ""66ad5227-55de-448d-b880-59160da5d3ed"",
            ""actions"": [
                {
                    ""name"": ""MoveBarSelector"",
                    ""type"": ""PassThrough"",
                    ""id"": ""eedae05e-0251-44c5-a4c3-71fbbb5cb551"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleInventory"",
                    ""type"": ""Button"",
                    ""id"": ""feb1f2e0-62be-4288-ac00-2049897614fb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d96b2bf2-be94-428e-852e-adcf7ba9704b"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBarSelector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""840f2935-db67-43bf-afdc-e1d9a556a362"",
                    ""path"": ""<Keyboard>/#(E)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerAction"",
            ""id"": ""eef7055c-3624-40e7-8246-619303b82373"",
            ""actions"": [
                {
                    ""name"": ""DefaultAction"",
                    ""type"": ""Button"",
                    ""id"": ""8eca532c-1702-48f7-ba44-87f0a29d87fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""dbe71c55-5459-45a2-8d5e-66133a0f6cbb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CloseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""e1e1ea1c-44db-4f78-aa13-ee0053c769ae"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SkipTime"",
                    ""type"": ""Button"",
                    ""id"": ""a1d8099a-56b4-4797-a481-5b8d2c3c3bc4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a785815c-486e-4473-adaa-460e66381393"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DefaultAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98376e65-b56a-4fdb-9c8e-037ca473fc65"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DefaultAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6be75c61-5cb1-4361-a5bd-e462f5fa8fa7"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""19091320-b6ff-4a0d-b722-110a682b97f1"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a348655f-80d9-4c45-b1fd-2942ac85886b"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkipTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a8528a6-ce41-4483-af89-42ad50b1f08b"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkipTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf58dd83-70f7-4476-b082-192e49b325af"",
                    ""path"": ""<Keyboard>/rightShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkipTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_Move = m_PlayerMovement.FindAction("Move", throwIfNotFound: true);
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_MoveBarSelector = m_Inventory.FindAction("MoveBarSelector", throwIfNotFound: true);
        m_Inventory_ToggleInventory = m_Inventory.FindAction("ToggleInventory", throwIfNotFound: true);
        // PlayerAction
        m_PlayerAction = asset.FindActionMap("PlayerAction", throwIfNotFound: true);
        m_PlayerAction_DefaultAction = m_PlayerAction.FindAction("DefaultAction", throwIfNotFound: true);
        m_PlayerAction_Pause = m_PlayerAction.FindAction("Pause", throwIfNotFound: true);
        m_PlayerAction_CloseMenu = m_PlayerAction.FindAction("CloseMenu", throwIfNotFound: true);
        m_PlayerAction_SkipTime = m_PlayerAction.FindAction("SkipTime", throwIfNotFound: true);
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

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Move;
    public struct PlayerMovementActions
    {
        private @PlayerActions m_Wrapper;
        public PlayerMovementActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerMovement_Move;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // Inventory
    private readonly InputActionMap m_Inventory;
    private IInventoryActions m_InventoryActionsCallbackInterface;
    private readonly InputAction m_Inventory_MoveBarSelector;
    private readonly InputAction m_Inventory_ToggleInventory;
    public struct InventoryActions
    {
        private @PlayerActions m_Wrapper;
        public InventoryActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveBarSelector => m_Wrapper.m_Inventory_MoveBarSelector;
        public InputAction @ToggleInventory => m_Wrapper.m_Inventory_ToggleInventory;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterface != null)
            {
                @MoveBarSelector.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnMoveBarSelector;
                @MoveBarSelector.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnMoveBarSelector;
                @MoveBarSelector.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnMoveBarSelector;
                @ToggleInventory.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnToggleInventory;
                @ToggleInventory.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnToggleInventory;
                @ToggleInventory.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnToggleInventory;
            }
            m_Wrapper.m_InventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveBarSelector.started += instance.OnMoveBarSelector;
                @MoveBarSelector.performed += instance.OnMoveBarSelector;
                @MoveBarSelector.canceled += instance.OnMoveBarSelector;
                @ToggleInventory.started += instance.OnToggleInventory;
                @ToggleInventory.performed += instance.OnToggleInventory;
                @ToggleInventory.canceled += instance.OnToggleInventory;
            }
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);

    // PlayerAction
    private readonly InputActionMap m_PlayerAction;
    private IPlayerActionActions m_PlayerActionActionsCallbackInterface;
    private readonly InputAction m_PlayerAction_DefaultAction;
    private readonly InputAction m_PlayerAction_Pause;
    private readonly InputAction m_PlayerAction_CloseMenu;
    private readonly InputAction m_PlayerAction_SkipTime;
    public struct PlayerActionActions
    {
        private @PlayerActions m_Wrapper;
        public PlayerActionActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @DefaultAction => m_Wrapper.m_PlayerAction_DefaultAction;
        public InputAction @Pause => m_Wrapper.m_PlayerAction_Pause;
        public InputAction @CloseMenu => m_Wrapper.m_PlayerAction_CloseMenu;
        public InputAction @SkipTime => m_Wrapper.m_PlayerAction_SkipTime;
        public InputActionMap Get() { return m_Wrapper.m_PlayerAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionActions instance)
        {
            if (m_Wrapper.m_PlayerActionActionsCallbackInterface != null)
            {
                @DefaultAction.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnDefaultAction;
                @DefaultAction.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnDefaultAction;
                @DefaultAction.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnDefaultAction;
                @Pause.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnPause;
                @CloseMenu.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnCloseMenu;
                @CloseMenu.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnCloseMenu;
                @CloseMenu.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnCloseMenu;
                @SkipTime.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnSkipTime;
                @SkipTime.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnSkipTime;
                @SkipTime.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnSkipTime;
            }
            m_Wrapper.m_PlayerActionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @DefaultAction.started += instance.OnDefaultAction;
                @DefaultAction.performed += instance.OnDefaultAction;
                @DefaultAction.canceled += instance.OnDefaultAction;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @CloseMenu.started += instance.OnCloseMenu;
                @CloseMenu.performed += instance.OnCloseMenu;
                @CloseMenu.canceled += instance.OnCloseMenu;
                @SkipTime.started += instance.OnSkipTime;
                @SkipTime.performed += instance.OnSkipTime;
                @SkipTime.canceled += instance.OnSkipTime;
            }
        }
    }
    public PlayerActionActions @PlayerAction => new PlayerActionActions(this);
    public interface IPlayerMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
    public interface IInventoryActions
    {
        void OnMoveBarSelector(InputAction.CallbackContext context);
        void OnToggleInventory(InputAction.CallbackContext context);
    }
    public interface IPlayerActionActions
    {
        void OnDefaultAction(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnCloseMenu(InputAction.CallbackContext context);
        void OnSkipTime(InputAction.CallbackContext context);
    }
}
