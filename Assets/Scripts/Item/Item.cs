using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item :  ScriptableObject
{
    [SerializeField] protected string _name;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected string _id;
    [SerializeField] protected string _desc;

    public string Name { get { return _name; } }

    public Sprite Icon { get { return _icon; } }

    public string ID { get { return _id; } }

    public string Desc { get { return _desc; } }

    public virtual bool IsSalable { get { return false; } }

#if UNITY_EDITOR
    protected void OnValidate()
    {
        _id = (GetType().Name + "_" + _name.Trim()).ToLower();
        _id = System.Text.RegularExpressions.Regex.Replace(_id, @"\s+", "");
    }
#endif

    public abstract void DoAction();
}
