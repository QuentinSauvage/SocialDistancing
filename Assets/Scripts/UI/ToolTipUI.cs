using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipUI : MonoBehaviour
{
    [SerializeField] private RectTransform _transform;

    [SerializeField] private TMPro.TextMeshProUGUI _name;
    [SerializeField] private TMPro.TextMeshProUGUI _desc;
    public Item Item { set { _name.text = value.Name; _desc.text = value.Desc; } }

    public Vector2 Postion { set { _transform.position = value; } }

    public void ShowDescription()
    {
        _desc.gameObject.SetActive(true);
        _transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200);
    }

    public void HideDescription()
    {
        _desc.gameObject.SetActive(false);
        _transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
    }

    public void Update()
    {
    }
}
