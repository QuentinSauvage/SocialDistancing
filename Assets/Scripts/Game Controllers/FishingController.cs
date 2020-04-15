using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HookEvent : UnityEvent<Fish.FishRarity> { }

[System.Serializable]
public class SuccessEvent : UnityEvent<Fish> { }

[System.Serializable]
public class FishingController
{
    [SerializeField] private FishDatabase _fishDatabase;

    public UnityEvent _beginEvent;
    public HookEvent _hookEvent;
    public SuccessEvent _succesEvent;
    public UnityEvent _failEvent;

    private bool _fishing;
    private bool _hooking;
    private Fish _fish;

    private List<Fish> _fishList;

    public FishingController() { _fishList = new List<Fish>(); }

    private Coroutine _coroutine;

    private Fish RandomFish()
    {
        float rand = Random.Range(0, 1f);
        Fish.FishRarity rarity = (rand < 0.1f ? Fish.FishRarity.LEGEND : rand < 0.25f ? Fish.FishRarity.EPIC : rand < 0.5f ? Fish.FishRarity.RARE : Fish.FishRarity.COMMUN);
        List<Fish> possibleFish = _fishList.FindAll((Fish f) => { return f.Rarity == rarity; });
        if (possibleFish.Count != 0)
        {
            return possibleFish[Random.Range(0, possibleFish.Count)];
        }
        else
            return _fishList[Random.Range(0, possibleFish.Count)];
    }

    private IEnumerator Fishing(bool isRainig, int hour, Bait bait)
    {
        _beginEvent.Invoke();
        _fishing = true;
        _fishDatabase.GetPossibleFish(isRainig, hour, bait, _fishList);
        yield return 0; //wait next frame
        _fish = RandomFish();
        Debug.Assert(_fish != null);
        yield return new WaitForSecondsRealtime(_fish.GetWaitSecond());
        _hooking = true;
        _hookEvent.Invoke(_fish.Rarity);
        yield return new WaitForSecondsRealtime(_fish.HookSecond);
        _failEvent.Invoke();
        _fish = null; _hooking = false; _fishing = false;
    }

    public void ActionPressed(GameController gController)
    {
        if(!_fishing)
        {
            _coroutine = gController.StartCoroutine(Fishing(false,0,null));
        }
        else
        {
            gController.StopCoroutine(_coroutine);
            if (_hooking)
            {
                _succesEvent.Invoke(_fish);
            }
            else
            {
                _failEvent.Invoke();
            }
            _fish = null; _hooking = false; _fishing = false;
        }
    }

}
