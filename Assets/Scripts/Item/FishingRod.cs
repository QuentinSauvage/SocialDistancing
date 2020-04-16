using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/FishingRod")]
public class FishingRod : Item
{
    //fishing rod bonus ?

    private Bait _bait;
    private BonusBait _bonusBait;

    public Bait Bait { get { return _bait; } set { _bait = value; } }
    public BonusBait BonusBait { get { return _bonusBait; } set { _bonusBait = value; } }

    public override void DoAction()
    {
        //todo: display menu
    }
}
