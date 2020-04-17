using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class BuyingController : MonoBehaviour
{
    [SerializeField] private InventoryController _inventoryController;

    [SerializeField] private GameObject _pSlot;
    [SerializeField] private RectTransform _uiContainer;
    [SerializeField] private BuyingEntry[] _catalog;

    public bool IsVisible { get { return gameObject.activeInHierarchy; } }

    private void Awake()
    {
        if (_uiContainer != null && _pSlot != null)
        {
            foreach (BuyingEntry entry in _catalog)
            {
                BuyingSlot slot = Instantiate(_pSlot, _uiContainer).GetComponent<BuyingSlot>();
                slot.Entry = entry; slot._buyEvent.AddListener(Buy);
            }
        }
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void Buy(BuyingEntry entry)
    {
        Debug.Log($"Buying {entry.Item.Name} for {entry.Price}");
        _inventoryController.AddItem(Instantiate(entry.Item), 1);
        _inventoryController.Money = _inventoryController.Money - entry.Price;
    }
}
