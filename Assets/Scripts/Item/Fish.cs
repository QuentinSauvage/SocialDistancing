using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Fish")]
public class Fish : Salable
{
    [SerializeField] private bool _canBeFishedRaining;
    [SerializeField] private bool _canBeFishedNotRaining;

    [SerializeField] private bool _needBait;
    [Header("if working bait is empty, every baits are working")]
    [SerializeField] private List<Bait> _workingBait;

    [Header("Hours when the fish is gettable")]
    [SerializeField] private int _beginHour;
    [SerializeField] private int _endHour;

    [Header("Time to wait before hooking")]
    [SerializeField] private int _minWaitSecond;
    [SerializeField] private int _maxWaitSecond;

    [Header("Time to hook before the fish run away")]
    [SerializeField] private int _hookSecond;
    public int HookSecond { get { return _hookSecond; } }

    public enum FishRarity { COMMUN,RARE,EPIC,LEGEND};
    [SerializeField] FishRarity _rarity;
    public FishRarity Rarity { get { return _rarity; } }
    
    private bool IsGoodHour(int hour) { return hour >= _beginHour && hour <= _endHour; }
    private bool IsGoodWeather(bool isRaining) { return (isRaining && _canBeFishedRaining) || (!isRaining && _canBeFishedNotRaining); }
    private bool IsGoodBait(Bait bait)
    {
        if(_needBait)
        {
            return bait != null && (_workingBait.Count == 0 || _workingBait.Contains(bait));
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Test if a fish can be fished depending of the conditions
    /// </summary>
    /// <param name="isRaining">true if it's raining</param>
    /// <param name="bait">bait used, can be null</param>
    /// <param name="hour">hour of the game</param>
    /// <returns>true if the fish can be fished</returns>
    public bool CanBeFished(bool isRaining, Bait bait, BonusBait bonusBait, int hour)
    {
        return (bonusBait.BonusIgnoreHour||IsGoodHour(hour)) && (bonusBait.BonusIgnoreWeaher||IsGoodWeather(isRaining)) && IsGoodBait(bait);
    }

    public int GetWaitSecond()
    {
        return Random.Range(_minWaitSecond, _maxWaitSecond);
    }

    public override void DoAction()
    {
        //do nothing, it's a fish
    }
}
