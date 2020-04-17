using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/BonusBait")]
public class BonusBait : Item
{
    [SerializeField]
    private float _bonusPercentWaitTime;
    public float BonusPercentWaitTime { get { return _bonusPercentWaitTime; } }

    [SerializeField]
    private bool _bonusIgnoreHour;
    public bool BonusIgnoreHour { get { return _bonusIgnoreHour; } }

    [SerializeField]
    private bool _bonusIgnoreWeather;
    public bool BonusIgnoreWeaher { get { return _bonusIgnoreWeather; } }

    [SerializeField]
    private bool _bonusForceRarity;
    public bool BonusForceRarity { get { return _bonusForceRarity; } }
    [SerializeField]
    private Fish.FishRarity _bonusRarity;
    public Fish.FishRarity BonusRarity { get { return  _bonusRarity; } }

    public override void DoAction()
    {
        // do nothing, it's a bait
    }
}
