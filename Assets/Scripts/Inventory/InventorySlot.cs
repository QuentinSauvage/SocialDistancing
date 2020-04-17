using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    public UnityEngine.Events.UnityEvent AddingItemEvent;
    public UnityEngine.Events.UnityEvent RemovingItemEvent;

    public InventoryController.Stack _stack;

    public delegate void Clicked(InventorySlot slot, PointerEventData clickEvent);
    public Clicked _clicked;

    [SerializeField]
    private RectTransform _transform;
    public RectTransform RectTransform { get { return _transform; } }

    [SerializeField]
    private UnityEngine.UI.Image _background;

    [SerializeField]
    private UnityEngine.UI.Image _image;

    [SerializeField]
    private UnityEngine.UI.Text _nb;

    [SerializeField]
    private Color _selectedColor;

    [SerializeField]
    private Color _color;

    private void Awake()
    {
        _background.color = _color;
    }

    /// <summary>
    /// Transfer item from stack to this slot. Update the slot in ui.
    /// </summary>
    /// <param name="stack">stack from where the item are taken</param>
    /// <param name="nbItem">nb item to transfer</param>
    public void TransferItemFromStack(InventoryController.Stack stack, int nbItem)
    {
        if (_stack._nbItem + nbItem > InventoryController.MAX_ITEM_PER_STACK)
        {
            int rest = _stack._nbItem + nbItem - InventoryController.MAX_ITEM_PER_STACK;
            _stack._nbItem = InventoryController.MAX_ITEM_PER_STACK;
            stack._nbItem = rest;
        }
        else
        {
            _stack._nbItem += nbItem;
            stack._nbItem -= nbItem;
        }
        try { AddingItemEvent.Invoke(); } catch (System.NullReferenceException) { };
        UpdateSlot();
    }

    /// <summary>
    /// Transfer item from slot to stack. Update the slot in ui.
    /// </summary>
    /// <param name="stack">stack where the item are transfered</param>
    /// <param name="nbItem">nb item to transfer</param>
    public void TransferItemToStack(InventoryController.Stack stack, int nbItem)
    {
        stack._item = _stack._item;
        stack._nbItem = nbItem;
        _stack._nbItem -= nbItem;
        try{RemovingItemEvent.Invoke(); } catch (System.NullReferenceException) { };
        UpdateSlot();
    }

    /// <summary>
    /// Swap stack content with the stack from this slot. Update the ui.
    /// </summary>
    /// <param name="stack">stack to swap with</param>
    public void SwapStack(InventoryController.Stack stack)
    {
        Item item = _stack._item; int nbItem = _stack._nbItem; 
        _stack._item = stack._item; _stack._nbItem = stack._nbItem;
        stack._item = item; stack._nbItem = nbItem;
        try{AddingItemEvent.Invoke(); } catch (System.NullReferenceException) { };
        UpdateSlot();
    }

    /// <summary>
    /// Transfer item from stack to this slot and set the item in this slot. Update the ui.
    /// </summary>
    public void SetItemFromStack(InventoryController.Stack stack, int nbItem)
    {
        _stack._item = stack._item;
        _stack._nbItem = nbItem;
        stack._nbItem -= nbItem;
        AddingItemEvent.Invoke();
        UpdateSlot();
    }

    /// <summary>
    /// Add item to slot with item already setted. Update the ui.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="nbItem"></param>
    /// <returns>nb item taht couldn't be added. Can be 0.</returns>
    public int AddItemToSlot(int nbItem)
    {
        if (_stack._nbItem + nbItem > InventoryController.MAX_ITEM_PER_STACK)
        {
            int rest = _stack._nbItem + nbItem - InventoryController.MAX_ITEM_PER_STACK;
            _stack._nbItem = InventoryController.MAX_ITEM_PER_STACK;
            UpdateSlot();
            AddingItemEvent.Invoke(); 
            return rest;
        }
        else
        {
            _stack._nbItem += nbItem;
            UpdateSlot();
            AddingItemEvent.Invoke();
            return 0;
        }
    }

    /// <summary>
    /// remove item from the Update the ui.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="nbItem"></param>
    /// <returns>nb item taht couldn't be removed. Can be 0.</returns>
    public int RemoveItemFromSlot(int nbItem)
    {
        int rest = _stack._nbItem - nbItem;
        _stack._nbItem = Mathf.Clamp(rest, 0, InventoryController.MAX_ITEM_PER_STACK);
        UpdateSlot();
        RemovingItemEvent.Invoke();
        return Mathf.Clamp(rest, -InventoryController.MAX_ITEM_PER_STACK, 0) * -1;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _background.color = _selectedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _background.color = _color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _clicked(this,eventData);
    }

    private void OnDisable()
    {
        _background.color = _color;
    }

    public void UpdateSlot()
    {
        if (_stack._nbItem != 0)
        {
            _image.sprite = _stack._item.Icon;
            _nb.text = _stack._nbItem.ToString();
            _image.gameObject.SetActive(true);
            _nb.gameObject.SetActive(true);
        }
        else
        {
            _image.gameObject.SetActive(false);
            _nb.gameObject.SetActive(false);
        }


    }
}
