using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    public InventoryController.Stack stack;

    public delegate void Clicked(InventorySlot slot);
    public Clicked clicked;

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
        clicked(this);
    }

    public void UpdateSlot()
    {
        if (stack._nbItem != 0)
        {
            _image.sprite = stack._item.Icon;
            _nb.text = stack._nbItem.ToString();
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
