using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Vegetable")]
public class Vegetable : Salable
{
	[Header("In-game minutes before being fully grown")]
	[SerializeField] float _timeToGrow;
	public float TimeToGrow { get { return _timeToGrow; } }

	[Header("In-game minutes before wilting")]
	[SerializeField] int _timeBeforeWilting;
	[Header("In-game minutes before wilting, if not watered, 0 means it doesn't need water")]
	[SerializeField] float _waterTimer;
	public float WaterTimer {
		get { return _waterTimer; }
		set { _waterTimer = value; }
	}

	[SerializeField] int _quantity;
	[SerializeField] List<Sprite> _growthSprites;
	public List<Sprite> Sprites { get { return _growthSprites; } }

	[SerializeField] Sprite _wiltedSprite;
	[SerializeField] Sprite _wateredSprite;

	int _currentState = 0;
	public int State { get { return _currentState; } }
	bool _needsWater = false;

	int _lastMilestone;

	public int Quantity { get { return _quantity; } }

    public override void DoAction()
    {
        // do nothing, it's a vegetable
    }

	// Decreases all timers and returns a sprite corresponding to the state of the object if it changed, null otherwise
	public Sprite UpdateGrowth(bool raining, float baseGrowthTimer, float baseWateredTimer)
	{
		if (_timeToGrow > 1)
		{
			--_timeToGrow;
			if(_timeToGrow == 0)
			{
				_currentState = _growthSprites.Count - 1;
				return _growthSprites[_currentState];
			}
			if(!raining)
			{
				--_waterTimer;
				if(_waterTimer == 0)
				{
					_timeBeforeWilting = 0;
					return _wiltedSprite;
				}
				else if(_waterTimer < baseWateredTimer / 2 && !_needsWater)
				{
					_needsWater = true;
					return _wateredSprite;
				}
			}
			else
			{
				_waterTimer = baseWateredTimer;
				if (_needsWater)
				{
					_needsWater = false;
					return _growthSprites[_currentState];
				}
			}

			float growthPercentage = ((baseGrowthTimer - _timeToGrow) / baseGrowthTimer) * 100;
			float milestone = ((_currentState + 1) * (100 / _growthSprites.Count));
			if (growthPercentage > milestone && _lastMilestone < milestone)
			{
				milestone = _lastMilestone;
				++_currentState;
				if (!_needsWater)
				{
					return _growthSprites[_currentState];
				}	
			}
		}
		else if(_timeBeforeWilting > 0)
		{
			--_timeBeforeWilting;
			if (_timeBeforeWilting == 0)
			{
				_currentState = -1;
				return _wiltedSprite;
			}
		}
		return null;
	}

	public Sprite CheckHarvest(float waterTimer)
	{
		_waterTimer = waterTimer;
		if (_needsWater)
		{
			_needsWater = false;
			Debug.Log(_currentState);
			return _growthSprites[_currentState];
		}
		Debug.Log(_waterTimer);
		return null;
	}

}
