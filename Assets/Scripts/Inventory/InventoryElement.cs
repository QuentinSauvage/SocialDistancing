using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventoryElement
{
    public UnityEngine.Events.UnityAction OnAddingItem;
    public UnityEngine.Events.UnityAction OnRemovingItem;

    [Header("UI")]
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private LayoutGroup _layout;

    [Header("Logic")]
    [SerializeField] private bool _isGrid;
    [SerializeField] private int _size;
    public int Size { get { return _size; } }

    private List<InventorySlot> _slots;

    public bool IsVisible { get { return _rectTransform.gameObject.activeInHierarchy; } set { _rectTransform.gameObject.SetActive(value); } }

    public void Start(GameObject pslot, InventorySlot.Clicked clicked)
    {
        OnAddingItem += () => {  };
        OnRemovingItem += () => {  };

        _slots = new List<InventorySlot>(_size);
        for (int i = 0; i < _size; ++i)
        {
            GameObject slot = GameObject.Instantiate(pslot, _rectTransform);

            _slots.Add(slot.GetComponent<InventorySlot>());
            _slots[i]._clicked = clicked;
            _slots[i].AddingItemEvent.AddListener(OnAddingItem);
            _slots[i].RemovingItemEvent.AddListener(OnRemovingItem);
            _slots[i]._stack = new InventoryController.Stack();
        }
    }

    public RectTransform Position(int i) { return _slots[i].RectTransform; }

    public InventorySlot Slot(int i) { return _slots[i]; }

    /// <summary>
    /// Add item to the inventoryElement
    /// </summary>
    /// <param name="item">Item to add</param>
    /// <param name="nbItem">nb item to add</param>
    /// <returns>nb item that couldn't be added can be 0</returns>
    public int AddItem(Item item, int nbItem)
    {
        if (nbItem == 0) return 0;
        foreach (InventorySlot slot in _slots)
        {
            if (slot._stack._nbItem == 0)
            {
                slot._stack._item = item;
                if ((nbItem = slot.AddItemToSlot(nbItem)) == 0) return 0;
                else continue;
            }
            else
            {
                if (slot._stack._item.ID == item.ID)
                {
                    if ((nbItem = slot.AddItemToSlot(nbItem)) == 0) return 0;
                    else continue;
                }
            }
        }
        return nbItem;
    }

    /// <summary>
    /// Remove item from the inventoryElement
    /// </summary>
    /// <param name="item">Item to remove</param>
    /// <param name="nbItem">nb item to remove</param>
    /// <returns>nb item that couldn't be removed, can be 0</returns>
    public int RemoveItem(Item item, int nbItem)
    {
        if (nbItem == 0) return 0;
        foreach (InventorySlot slot in _slots)
        {
            if (slot._stack._nbItem != 0)
            {
                if (slot._stack._item.ID == item.ID)
                {
                    if ((nbItem = slot.RemoveItemFromSlot(nbItem)) == 0) return 0;
                    else continue;
                }
            }
        }
        return nbItem;
    }

    /// <summary>
    /// Check if the element contains the item
    /// </summary>
    /// <param name="item">item to be tested</param>
    /// <returns>true if the element contains the item, false else</returns>
    public bool Contains(Item item)
    {
        foreach(InventorySlot slot in _slots)
        {
            if (slot._stack._item.ID == item.ID) return true;
        }
        return false;
    }

    /// <summary>
    /// Count how many items the element countains
    /// </summary>
    /// <param name="item">item to be tested</param>
    /// <returns>how many items the element countains</returns>
    public int Count(Item item)
    {
        int nb = 0;
        foreach(InventorySlot slot in _slots)
        {
            if (slot._stack._item.ID == item.ID) nb += slot._stack._nbItem;
        }
        return nb;
    }
}
