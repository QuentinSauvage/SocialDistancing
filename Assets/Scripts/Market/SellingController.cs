using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingController : MonoBehaviour
{
    // prefab of slot
    [SerializeField] private GameObject _pslot;

    [SerializeField] private InventoryElement _inventoryElement;

    [SerializeField] private InventoryController _inventoryController;

    [SerializeField] private TMPro.TextMeshProUGUI _sellButtonText;

    private int CurrentPrice;

    public void Awake()
    {
        Debug.Assert(_inventoryElement != null, "need _inventoryElement");


        _inventoryElement.OnAddingItem += UpdatePrice;
        _inventoryElement.OnRemovingItem += UpdatePrice;

        _inventoryElement.Start(_pslot,_inventoryController.SlotClicked);


    }

    private void UpdatePrice()
    {
        CurrentPrice = 0;
        for(int i=0;i<_inventoryElement.Size;++i)
        {
            InventorySlot slot = _inventoryElement.Slot(i);
            if(slot._stack._nbItem!=0 && slot._stack._item.IsSalable)
            {
                CurrentPrice += ((Salable)slot._stack._item).Price * slot._stack._nbItem;
            }
        }

        Debug.Log($"Sell for {CurrentPrice}");
        _sellButtonText.text = $"Sell for {CurrentPrice}";
    }
}
