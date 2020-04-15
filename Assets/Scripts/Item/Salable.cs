using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Salable : Item
{
    [SerializeField] protected int price;
    public int Price { get { return price; } }
    
    public override bool IsSalable { get { return true; } }
}
