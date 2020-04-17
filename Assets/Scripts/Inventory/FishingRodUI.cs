using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingRodUI : MonoBehaviour
{
    [SerializeField] private InventorySlot _bait;
    [SerializeField] private InventorySlot _bonusBait;

    [SerializeField] private Canvas _canvas;

    private InventorySlot.Clicked _clicked;

    private FishingRod _rod;

    public bool IsVisible { get { return _canvas.gameObject.activeInHierarchy; } }

    private void Awake()
    {
        _bait._clicked = OnClickBait;
        _bonusBait._clicked = OnClickBonusBait;
    }

    public void Toggle(FishingRod rod)
    {
        _rod = rod;
        _canvas.gameObject.SetActive(!_canvas.gameObject.activeInHierarchy);
    }

    private void OnClickBait(InventorySlot slot, UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (eventData.button == UnityEngine.EventSystems.PointerEventData.InputButton.Right) return;
        _clicked(slot, eventData);
        if (slot._stack._nbItem == 0) return;
        if(slot._stack._item.GetType() !=  typeof(Bait))
        {
            _bait._clicked.Invoke(slot,eventData);
        }
        else
        {
            _rod.Bait = (Bait)slot._stack._item;
        }
    }

    private void OnClickBonusBait(InventorySlot slot, UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (eventData.button == UnityEngine.EventSystems.PointerEventData.InputButton.Right) return;
        _clicked(slot, eventData);
        if (slot._stack._nbItem == 0) return;
        if (slot._stack._item.GetType() != typeof(BonusBait))
        {
            _bait._clicked.Invoke(slot, eventData);
        }
        else
        {
            _rod.BonusBait = (BonusBait)slot._stack._item;
        }
    }

    public void SetOnclick(InventorySlot.Clicked clicked)
    {
        _clicked = clicked;
        _bait._stack = new InventoryController.Stack(); _bonusBait._stack = new InventoryController.Stack();
    }
}
