using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class InventoryController : MonoBehaviour
{
    public class Stack
    {
        public Item _item;
        public int _nbItem;

        public Stack()
        {
            _nbItem = 0;
        }
    }

    // prefab of slot
    [SerializeField] private GameObject _pslot;
    private PlayerActions _playerActions;

    private Stack _selectedStack;

    //*************** INVENTORY *****************\\

    //Logic
    [SerializeField] private Vector2 _inventorySize;

    [SerializeField] private int _maxItemPerStack;

    //UI
    [SerializeField] private RectTransform _uiInventory;
    [SerializeField] private GridLayoutGroup _inventoryLayout;

    private List<InventorySlot> _inventorySlots;

    //private float _inventorySlotSize;

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

    //private float _barSlotSize;

    [Header("DEBUG")]
    public ItemDatabase itemDatabase;


    void Awake()
    {
        Debug.Assert(_barLenght != 0, "Inventory Bar can't have 0 as lenght");
        Debug.Assert(_pslot != null, "Inventory need a slot prefab");
        Debug.Assert(_uiBar != null, "Inventory need its bar");
        Debug.Assert(_uiSelector != null, "Inventory Bar need a selector");
        Debug.Assert(_maxItemPerStack != 0, "max item per stack not set");

        _playerActions = new PlayerActions();
        _playerActions.Inventory.MoveBarSelector.performed += UpdateSelectorPosition;
        _playerActions.Inventory.ToggleInventory.performed += ToggleInventory;
        _barSlots = new List<InventorySlot>(_barLenght);
        _inventorySlots = new List<InventorySlot>((int)(_inventorySize.x * _inventorySize.y));


        _selectedStack = new Stack();

        _idSelected = 0;
    }

#if UNITY_EDITOR

    [ContextMenu("Populate")]
    void DebugPopulate()
    {
        /*DebugItem di0 = new DebugItem("Item0",item0);
        DebugItem di1 = new DebugItem("Item1", item1);
        DebugItem di2 = new DebugItem("Item2", item2);*/

        Debug.Log("Populating the inventory");

        _barSlots[3].stack._item = itemDatabase.GetItemByID("vegetable_salad"); _barSlots[3].stack._nbItem = 80; _barSlots[3].UpdateSlot();
        _inventorySlots[0].stack._item = itemDatabase.GetItemByID("vegetable_pumpkin"); _inventorySlots[0].stack._nbItem = 99; _inventorySlots[0].UpdateSlot();
        _inventorySlots[10].stack._item = itemDatabase.GetItemByID("fish_salmon"); ; _inventorySlots[10].stack._nbItem = 99; _inventorySlots[10].UpdateSlot();
        _inventorySlots[22].stack._item = itemDatabase.GetItemByID("vegetable_salad"); _inventorySlots[22].stack._nbItem = 50; _inventorySlots[22].UpdateSlot();
        _inventorySlots[33].stack._item = itemDatabase.GetItemByID("vegetable_pumpkin"); _inventorySlots[33].stack._nbItem = 50; _inventorySlots[33].UpdateSlot();

    }

    [ContextMenu("Add 50 pumpkins")]
    void Add50Pumpkin()
    {
        AddItem(itemDatabase.GetItemByID("vegetable_pumpkin"),50);
    }
    [ContextMenu("Add 99 pumpkins")]
    void Add99Pumpkin()
    {
        AddItem(itemDatabase.GetItemByID("vegetable_pumpkin"), 99);
    }
    [ContextMenu("Add 10 pumpkins")]
    void Add10Pumpkin()
    {
        AddItem(itemDatabase.GetItemByID("vegetable_pumpkin"), 10);
    }

#endif
    void Start()
    {
        //***************** BAR ***********************\\
        //_barSlotSize = _uiBar.rect.width / _barLenght ;

        for (int i = 0; i < _barLenght; ++i)
        {
            GameObject slot = Instantiate(_pslot, _uiBar);

            _barSlots.Add(slot.GetComponent<InventorySlot>());
            _barSlots[i].clicked = SlotClicked;
            _barSlots[i].stack = new Stack();
           // _barSlots[i].RectTransform.sizeDelta = new Vector2(_barSlotSize, _barSlotSize);
        }


        //_uiSelector.sizeDelta = new Vector2(_barSlotSize, _barSlotSize);
        Invoke("UpdateSelectorPosition", Time.fixedDeltaTime);

        //****************** INVENTORY ***********************\\
        //_inventorySlotSize = _uiInventory.rect.width / _inventorySize.x- _inventorySize.x * 0.5f;
        //_inventoryLayout.cellSize = new Vector2(_inventorySlotSize, _inventorySlotSize);
        for (int i = 0; i < _inventorySize.x* _inventorySize.y; ++i)
        {
            GameObject slot = Instantiate(_pslot, _uiInventory);

            _inventorySlots.Add(slot.GetComponent<InventorySlot>());
            _inventorySlots[i].clicked = SlotClicked;
            _inventorySlots[i].stack = new Stack();
           // _inventorySlots[i].RectTransform.sizeDelta = new Vector2(_inventorySlotSize, _inventorySlotSize);
        }
    }

    //return 0 if ok, the number of item that could'nt be added else
    private int AddItemToSlot(Item item, int nbItem, InventorySlot slot)
    {
        if (slot.stack._nbItem + nbItem > _maxItemPerStack)
        {
            int rest = slot.stack._nbItem + nbItem - _maxItemPerStack;
            slot.stack._nbItem = _maxItemPerStack;
            slot.UpdateSlot();
            return rest;
        }
        else
        {
            slot.stack._nbItem += nbItem;
            slot.UpdateSlot();
            return 0;
        }
    }

    /*** callabable method to add or remove item ***/

    /// <summary>return 0 if ok, the number of item that could'nt be added else</summary>
    public int AddItem(Item item, int nbItem)
    {
        foreach (InventorySlot slot in _inventorySlots)
        {
            if(slot.stack._nbItem == 0)
            {
                slot.stack._item = item;
                if ((nbItem = AddItemToSlot(item, nbItem, slot)) == 0) return 0;
                else continue;
            }
            else
            {
                if(slot.stack._item == item)
                {
                    if ((nbItem = AddItemToSlot(item, nbItem, slot)) == 0) return 0;
                    else continue;
                }
            }
        }
        foreach (InventorySlot slot in _barSlots)
        {
            if (slot.stack._nbItem == 0)
            {
                slot.stack._item = item;
                if ((nbItem = AddItemToSlot(item, nbItem, slot)) == 0) return 0;
                else continue;
            }
            else
            {
                if (slot.stack._item == item)
                {
                    if ((nbItem = AddItemToSlot(item, nbItem, slot)) == 0) return 0;
                    else continue;
                }
            }
        }
        return nbItem;
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
        if(_selectedStack != null &&_selectedStack._nbItem!=0)
        {
            Sprite icon = _selectedStack._item.Icon;
            Texture2D texture = new Texture2D((int)icon.textureRect.width, (int)icon.textureRect.height);
            texture.SetPixels(icon.texture.GetPixels((int)icon.textureRect.x, (int)icon.textureRect.y, (int)icon.textureRect.width, (int)icon.textureRect.height));
            texture.Apply();
            /*texture.Resize((int)_inventoryLayout.cellSize.x, (int)_inventoryLayout.cellSize.y);
            texture.Apply();*/
            Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    /*** Inventory transfer methods ***/

    private void TransferItemFromSelectedStack(InventorySlot slot, int nbItem)
    {
        if (slot.stack._nbItem + nbItem > _maxItemPerStack)
        {
            int rest = slot.stack._nbItem + nbItem - _maxItemPerStack;
            slot.stack._nbItem = _maxItemPerStack;
            _selectedStack._nbItem = rest;
        }
        else
        {
            slot.stack._nbItem += nbItem;
            _selectedStack._nbItem -= nbItem; 
        }
    }

    private void TransferItemToSelectedStack(InventorySlot slot, int nbItem)
    {
        _selectedStack._item = slot.stack._item;
        _selectedStack._nbItem = nbItem;
        slot.stack._nbItem -= nbItem;
    }

    private void SwapSelectedStack(InventorySlot slot)
    {
        Stack tmp = slot.stack;
        slot.stack = _selectedStack;
        _selectedStack = tmp;
    }

    private void SetItemFromSelectedStack(InventorySlot slot, int nbItem)
    {
        slot.stack._item = _selectedStack._item;
        slot.stack._nbItem = nbItem;
        _selectedStack._nbItem -= nbItem;
    }

    private void SlotClicked(InventorySlot slot, UnityEngine.EventSystems.PointerEventData clickEvent)
    {
        if(_selectedStack == null ||_selectedStack._nbItem==0)
        {
            if (slot.stack._nbItem == 0) return;
            TransferItemToSelectedStack(slot, (clickEvent.button == UnityEngine.EventSystems.PointerEventData.InputButton.Left?slot.stack._nbItem:slot.stack._nbItem/2));
        }
        else
        {
            if (slot.stack._nbItem == 0)
                SetItemFromSelectedStack(slot, (clickEvent.button == UnityEngine.EventSystems.PointerEventData.InputButton.Left ? _selectedStack._nbItem : 1));
            else
            {
                if (slot.stack._item != _selectedStack._item)
                {
                    if (clickEvent.button == UnityEngine.EventSystems.PointerEventData.InputButton.Right) return;
                    SwapSelectedStack(slot);
                }
                else
                {
                    TransferItemFromSelectedStack(slot, (clickEvent.button == UnityEngine.EventSystems.PointerEventData.InputButton.Left ? _selectedStack._nbItem : 1));
                }
            }
        }

        slot.UpdateSlot();
        UpdateMouseCursor();

    }

    /*** end inventory transfer methods ***/

    void OnEnable()
    {
        _playerActions.Enable();
    }

    void OnDisable()
    {
        _playerActions.Disable();
    }
}
