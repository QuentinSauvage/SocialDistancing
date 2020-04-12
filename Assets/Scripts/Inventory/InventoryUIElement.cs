using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class SelectStackEvent : UnityEngine.Events.UnityEvent<InventoryController.Stack> { }
//USELESS to delete
public class InventoryUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool _pointerIsIn;

    public void Awake()
    {
        _pointerIsIn = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _pointerIsIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _pointerIsIn = false;
    }
}
