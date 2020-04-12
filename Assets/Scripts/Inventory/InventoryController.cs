using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/*********** DEBUG ITEM FOR TESTING *****************/

class DebugItem : IItem
{
    public string Name { get { return name; } }

    public Sprite Icon{ get { return icon; } }

    private string name;
    private Sprite icon;

    public DebugItem(string n, Sprite t) { name = n; icon = t; }
}

/************** END DEBUG ITEM *********************/


public class InventoryController : MonoBehaviour
{
    public class Stack
    {
        public IItem _item;
        public int _nbItem;

        public Stack()
        {
            _nbItem = 0;
        }
    }

    // prefab of slot
    [SerializeField] private GameObject _pslot;
    private PlayerActions _playerActions;

    private Stack _selectedStack = null;

    //*************** INVENTORY *****************\\

    //Logic
    [SerializeField] private Vector2 _inventorySize;

    //UI
    [SerializeField] private RectTransform _uiInventory;
    [SerializeField] private GridLayoutGroup _inventoryLayout;

    private List<InventorySlot> _inventorySlots;

    private float _inventorySlotSize;

    //*************** BAR ***************\\
    
    //Logic
    [SerializeField] private int _barLenght;

    private int _idSelected;
    public int SlotSelected { get { return _idSelected; } }

    private double _lastSelectorUpdate = 0;

    //private List<Stack> _bar;

    //UI
    [SerializeField] private RectTransform _uiBar;
    [SerializeField] private RectTransform _uiSelector;

    private List<InventorySlot> _barSlots;

    private float _barSlotSize;

    [Header("DEBUG")]
    public Sprite item0, item1, item2;


    void Awake()
    {
        Debug.Assert(_barLenght != 0, "Inventory Bar can't have 0 as lenght");
        Debug.Assert(_pslot != null, "Inventory need a slot prefab");
        Debug.Assert(_uiBar != null, "Inventory need its bar");
        Debug.Assert(_uiSelector != null, "Inventory Bar need a selector");

        _playerActions = new PlayerActions();
        _playerActions.Inventory.MoveBarSelector.performed += UpdateSelectorPosition;
        _playerActions.Inventory.ToggleInventory.performed += ToggleInventory;
        _barSlots = new List<InventorySlot>(_barLenght);
        _inventorySlots = new List<InventorySlot>((int)(_inventorySize.x * _inventorySize.y));

        
        _idSelected = 0;
    }

    [ContextMenu("Populate")]
    void DebugPopulate()
    {
        DebugItem di0 = new DebugItem("Item0",item0);
        DebugItem di1 = new DebugItem("Item1", item1);
        DebugItem di2 = new DebugItem("Item2", item2);

        _barSlots[3].stack._item = di0; _barSlots[3].stack._nbItem = 5; _barSlots[3].UpdateSlot();
        _inventorySlots[0].stack._item = di1; _inventorySlots[0].stack._nbItem = 1; _inventorySlots[0].UpdateSlot();
        _inventorySlots[10].stack._item = di2; _inventorySlots[10].stack._nbItem = 111; _inventorySlots[10].UpdateSlot();
        _inventorySlots[22].stack._item = di2; _inventorySlots[22].stack._nbItem = 3; _inventorySlots[22].UpdateSlot();
    }

    void Start()
    {
        /***************** BAR ***********************/
        _barSlotSize = _uiBar.rect.width / _barLenght ;

        for (int i = 0; i < _barLenght; ++i)
        {
            GameObject slot = Instantiate(_pslot, _uiBar);

            _barSlots.Add(slot.GetComponent<InventorySlot>());
            _barSlots[i].clicked = SlotClicked;
            _barSlots[i].stack = new Stack();
            _barSlots[i].RectTransform.sizeDelta = new Vector2(_barSlotSize, _barSlotSize);
        }


        _uiSelector.sizeDelta = new Vector2(_barSlotSize, _barSlotSize);
        Invoke("UpdateSelectorPosition", Time.fixedDeltaTime);

        /****************** INVENTORY ***********************/
        _inventorySlotSize = _uiInventory.rect.width / _inventorySize.x- _inventorySize.x * 0.5f;
        _inventoryLayout.cellSize = new Vector2(_inventorySlotSize, _inventorySlotSize);
        for (int i = 0; i < _inventorySize.x* _inventorySize.y; ++i)
        {
            GameObject slot = Instantiate(_pslot, _uiInventory);

            _inventorySlots.Add(slot.GetComponent<InventorySlot>());
            _inventorySlots[i].clicked = SlotClicked;
            _inventorySlots[i].stack = new Stack();
            _inventorySlots[i].RectTransform.sizeDelta = new Vector2(_inventorySlotSize, _inventorySlotSize);
        }
    }

    private void UpdateSelectorPosition(InputAction.CallbackContext context)
    {
        if (context.time - _lastSelectorUpdate < Time.fixedDeltaTime) return;
        _lastSelectorUpdate = context.time;
        if (context.ReadValue<Vector2>().y < 0)
            _idSelected++;
        else
            _idSelected--;

        _idSelected = Mathf.Clamp(_idSelected, 0, _barLenght - 1);
        UpdateSelectorPosition();
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        _uiInventory.gameObject.SetActive(!_uiInventory.gameObject.activeInHierarchy);
    }

    private void UpdateSelectorPosition()
    {
        _uiSelector.position = _barSlots[_idSelected].RectTransform.position;
    }

    private void UpdateMouseCursor()
    {
        if(_selectedStack != null)
        {
            Sprite icon = _selectedStack._item.Icon;
            Texture2D texture = new Texture2D((int)icon.textureRect.width, (int)icon.textureRect.height);
            texture.SetPixels(icon.texture.GetPixels((int)icon.textureRect.x, (int)icon.textureRect.y, (int)icon.textureRect.width, (int)icon.textureRect.height));
            texture.Apply();
            Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    private void SlotClicked(InventorySlot slot)
    {
        if(_selectedStack == null)
        {
            if (slot.stack._nbItem == 0) return;
            _selectedStack = slot.stack;
            slot.stack = new Stack();
        }
        else
        {
            if(slot.stack._nbItem == 0)
            {
                slot.stack = _selectedStack;
                _selectedStack = null;
            }
            else
            {
                if(slot.stack._item == _selectedStack._item)
                {
                    slot.stack._nbItem += _selectedStack._nbItem;
                    _selectedStack = null;
                }
                else
                {
                    Stack tmp = _selectedStack;
                    _selectedStack = slot.stack;
                    slot.stack = tmp;
                }
            }
        }

        slot.UpdateSlot();
        UpdateMouseCursor();

    }

    void OnEnable()
    {
        _playerActions.Enable();
    }

    void OnDisable()
    {
        _playerActions.Disable();
    }
}
