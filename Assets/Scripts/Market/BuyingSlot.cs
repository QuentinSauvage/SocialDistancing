using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BuyingEntry
{
    [SerializeField] private int _price;
    [SerializeField] private Item _item;

    public int Price { get { return _price; } }
    public Item Item { get { return _item; } }
}

[System.Serializable]
public class BuyEvent : UnityEngine.Events.UnityEvent<BuyingEntry> { }

public class BuyingSlot : MonoBehaviour
{
    private BuyingEntry _entry;
    public BuyingEntry Entry {set{ _itemSprite.sprite = value.Item.Icon; _itemPrice.text = ""+value.Price; _entry = value; } get { return _entry; } }

    [SerializeField] private Image _itemSprite;
    [SerializeField] private TMPro.TextMeshProUGUI _itemPrice;
    [SerializeField] private Button _button;

    public BuyEvent _buyEvent;

    public void Buy() { _buyEvent.Invoke(_entry); }

    // Update is called once per frame
    void Update()
    {
        
    }
}
