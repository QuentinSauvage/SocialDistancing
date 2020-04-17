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

	private GameController _gameController;

    // prefab of slot
    [SerializeField] private GameObject _pslot;
    private PlayerActions _playerActions;
    [SerializeField] public static int MAX_ITEM_PER_STACK=99;

    private int _money;
    public int Money { get { return _money;} set { _money = value; _uiMoney.text = "" + _money; } }

    private Stack _mouseStack;

    //*************** INVENTORY *****************\\

    //Logic
    /*[SerializeField] private Vector2 _inventorySize;


    //UI
    [SerializeField] private RectTransform _uiInventory;
    [SerializeField] private GridLayoutGroup _inventoryLayout;

    private List<InventorySlot> _inventorySlots;

    //private float _inventorySlotSize;*/
    [SerializeField] InventoryElement _inventory; //12*5
	public InventoryElement Inventory { get { return _inventory; } }

    //*************** BAR ***************\\

    //Logic
    /*[SerializeField] private int _barLenght;*/

    private int _idSelected;
    public Stack StackSelected { get { return _bar.Slot(_idSelected)._stack; } }

	private double _lastSelectorUpdate = 0;

    /*private List<Stack> _bar;

    //UI
    [SerializeField] private RectTransform _uiBar;*/
    [SerializeField] private RectTransform _uiSelector;

    //private List<InventorySlot> _barSlots;

    //private float _barSlotSize;
    [SerializeField] InventoryElement _bar;

    [SerializeField] TMPro.TextMeshProUGUI _uiMoney;

    [SerializeField] ToolTipUI _tooltip;

#if UNITY_EDITOR
    [Header("DEBUG")]
    public ItemDatabase itemDatabase;

    [ContextMenu("Add 50 pumpkins")]
    void Add50Pumpkins()
    {
        AddItem(itemDatabase.GetItemByID("vegetable_pumpkin"),50);
    }
    [ContextMenu("Add 99 pumpkins")]
    void Add99Pumpkins()
    {
        AddItem(itemDatabase.GetItemByID("vegetable_pumpkin"), 99);
    }
    [ContextMenu("Add 10 pumpkins")]
    void Add10Pumpkins()
    {
        AddItem(itemDatabase.GetItemByID("vegetable_pumpkin"), 10);
    }
    [ContextMenu("Add 50 salads")]
    void Add50Salads()
    {
        AddItem(itemDatabase.GetItemByID("vegetable_salad"), 50);
    }
    [ContextMenu("Add 99 salads")]
    void Add99Salads()
    {
        AddItem(itemDatabase.GetItemByID("vegetable_salad"), 99);
    }
    [ContextMenu("Add 10 salads")]
    void Add10Salads()
    {
        AddItem(itemDatabase.GetItemByID("vegetable_salad"), 10);
    }
    [ContextMenu("Add baits")]
    void AddBaits()
    {
        AddItem(itemDatabase.GetItemByID("bait_worm"), 55);
        AddItem(itemDatabase.GetItemByID("bonusbait_shrimp"), 41);
        AddItem(itemDatabase.GetItemByID("bonusbait_debug_legend"), 10);
    }
    [ContextMenu("Add fishing rod")]
    void AddRod()
    {
        AddItem(itemDatabase.GetItemByID("fishingrod_rod"),1);
    }

    [ContextMenu("Count Salads")]
    void CountSalads()
    {
        Debug.Log($"there is {Count(itemDatabase.GetItemByID("vegetable_salad"))} salad in the inventory");
    }
    [ContextMenu("Count pumpkins")]
    void CountPumpkins()
    {
        Debug.Log($"there is {Count(itemDatabase.GetItemByID("vegetable_pumpkin"))} pumpkin in the inventory");
    }

    [ContextMenu("Check Salad")]
    void CheckSalad()
    {
        if (Contains(itemDatabase.GetItemByID("vegetable_salad")))
        {
            Debug.Log("theres is salad in the inventory");
        }
        else
            Debug.Log("there is no salad in the inventory");
    }

    [ContextMenu("Remove 25 salads")]
    void Remove25salads()
    {
        Debug.Log("remove return: "+RemoveItem(itemDatabase.GetItemByID("vegetable_salad"), 25));
    }
    [ContextMenu("Remove 20 pumpkins")]
    void Remove20pumpkins()
    {
        Debug.Log("remove return: " + RemoveItem(itemDatabase.GetItemByID("vegetable_pumpkin"), 20));
    }
#endif

    void Awake()
    {
        Debug.Assert(_pslot != null, "Inventory need a slot prefab");
        Debug.Assert(_uiSelector != null, "Inventory Bar need a selector");
        Debug.Assert(MAX_ITEM_PER_STACK != 0, "max item per stack not set");

        _playerActions = new PlayerActions();
        _playerActions.Inventory.MoveBarSelector.performed += UpdateSelectorPosition;
        _playerActions.Inventory.ToggleInventory.performed += ToggleInventory;
        _playerActions.Inventory.CursorPosition.performed += (InputAction.CallbackContext ctx) => { _tooltip.Postion = ctx.ReadValue<Vector2>() + new Vector2(3,-3); };
        _playerActions.Inventory.ShowDescritpion.started += (InputAction.CallbackContext ctx) => { _tooltip.ShowDescription(); };
        _playerActions.Inventory.ShowDescritpion.canceled += (InputAction.CallbackContext ctx) => { _tooltip.HideDescription(); };

        _bar.Start(_pslot, SlotClicked,SlotHover,SlotStopHover);
        _inventory.Start(_pslot, SlotClicked,SlotHover, SlotStopHover);

        _mouseStack = new Stack();

        _idSelected = 0;
        _money = 0;
    }

    void Start()
    {
        Invoke("UpdateSelectorPosition", Time.fixedDeltaTime);
		_gameController = GameObject.Find("GameController").GetComponent<GameController>();
		_inventory.IsVisible = false;

        Money = 250;
	}

    /*** callabable method to add or remove item ***/

    /// <summary>
    /// Add item to the inventory. Return nb of item that couldn't be added. Update the UI.
    /// </summary>
    /// <param name="item">Item to be added</param>
    /// <param name="nbItem">nbItem to be added</param>
    public int AddItem(Item item, int nbItem)
    {
        nbItem = _inventory.AddItem(item, nbItem);

        nbItem = _bar.AddItem(item, nbItem);

        return nbItem;
    }

    /// <summary>
    /// Remove item from the inventory. Return nb of item that couldn't be removed. Update the UI.
    /// </summary>
    /// <param name="item">Item to be removed</param>
    /// <param name="nbItem">nbItem to be removed</param>
    public int RemoveItem(Item item, int nbItem)
    {
        nbItem = _inventory.RemoveItem(item, nbItem);

        nbItem = _bar.RemoveItem(item, nbItem);

        return nbItem;
    }

    /// <summary>
    /// Remove item from the slot selectionned in the bar. Update the ui. /!\ doesn't test if there is an item in the slot selected.
    /// </summary>
    /// <param name="nbItem">nbItem to be removed</param>
    public Item RemoveItemFromBarSelected(int nbItem)
    {
        _bar.Slot(_idSelected).RemoveItemFromSlot(nbItem);
		return itemDatabase.GetItemByID(_bar.Slot(_idSelected)._stack._item.ID);
	}

    /// <summary>
    /// Check if the inventory or the bar contains an item
    /// </summary>
    /// <param name="item">item to be tested</param>
    /// <returns>true if it contains the item, false else</returns>
    public bool Contains(Item item)
    {
        return _inventory.Contains(item) || _bar.Contains(item);
    }

    /// <summary>
    /// Count how many items are countained by the inventory
    /// </summary>
    /// <param name="item">Item to be counted</param>
    /// <returns>how many item there is in the inventory</returns>
    public int Count(Item item)
    {
        return _bar.Count(item) + _inventory.Count(item);
    }

	// Handles the player input corresponding to the inventory
    private void ToggleInventory(InputAction.CallbackContext context)
    {
		if(!GameController._gamePaused)
		{
			ToggleInventory2();
		}
    }

	// Handles the inventory visibility without needing a CallbackContext
	public void ToggleInventory2()
	{
		if(_gameController.Player.FrozenState)
		{
			_gameController.Player.UnFreeze();
		}
		else
		{
			_gameController.Player.Freeze();
		}
		_inventory.IsVisible = !_inventory.IsVisible;

        if (!_inventory.IsVisible) _tooltip.gameObject.SetActive(false);
	}

    private void UpdateSelectorPosition(InputAction.CallbackContext context)
    {
        if (context.time - _lastSelectorUpdate < Time.fixedDeltaTime) return;
        _lastSelectorUpdate = context.time;
        if (context.ReadValue<Vector2>().y < 0)
            _idSelected++;
        else
            _idSelected--;

        _idSelected = Mathf.Clamp(_idSelected, 0, _bar.Size - 1);
        UpdateSelectorPosition();
    }

    private void UpdateSelectorPosition()
    {
        _uiSelector.position = _bar.Position(_idSelected).position;
    }

    private void UpdateMouseCursor()
    {
        if(_mouseStack != null &&_mouseStack._nbItem!=0)
        {
            Sprite icon = _mouseStack._item.Icon;
            Texture2D texture = new Texture2D((int)icon.textureRect.width, (int)icon.textureRect.height,TextureFormat.RGBA32,false);
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

    public void SlotClicked(InventorySlot slot, UnityEngine.EventSystems.PointerEventData clickEvent)
    {
        if(_mouseStack == null ||_mouseStack._nbItem==0)
        {
            if (slot._stack._nbItem == 0) return;
            slot.TransferItemToStack(_mouseStack, (clickEvent.button == UnityEngine.EventSystems.PointerEventData.InputButton.Left?slot._stack._nbItem:slot._stack._nbItem/2));
        }
        else
        {
            if (slot._stack._nbItem == 0)
                slot.SetItemFromStack(_mouseStack, (clickEvent.button == UnityEngine.EventSystems.PointerEventData.InputButton.Left ? _mouseStack._nbItem : 1));
            else
            {
                if (slot._stack._item != _mouseStack._item)
                {
                    if (clickEvent.button == UnityEngine.EventSystems.PointerEventData.InputButton.Right) return;
                    slot.SwapStack(_mouseStack);
                }
                else
                {
                    slot.TransferItemFromStack(_mouseStack, clickEvent.button == UnityEngine.EventSystems.PointerEventData.InputButton.Left ? _mouseStack._nbItem : 1);
                }
            }
        }

        UpdateMouseCursor();

    }

    public void SlotHover(InventorySlot slot, UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (slot._stack._nbItem != 0 && slot._stack._item != null)
        {
            _tooltip.Item = slot._stack._item;
            _tooltip.gameObject.SetActive(true);

            //_tooltip.Postion = eventData.position;
        }
    }

    public void SlotStopHover(InventorySlot slot, UnityEngine.EventSystems.PointerEventData eventData)
    {
        _tooltip.gameObject.SetActive(false);
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
