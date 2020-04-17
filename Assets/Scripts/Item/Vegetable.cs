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
	[SerializeField] int _waterTimer;
	[SerializeField] int _quantity;
	[SerializeField] List<Sprite> _growthSprites;
	public List<Sprite> Sprites { get { return _growthSprites; } }

	[SerializeField] Sprite _wiltedSprite;

	int _currentState = 0;
	public int State { get { return _currentState; } }

	int _lastMilestone;

	public int Quantity { get { return _quantity; } }

    public override void DoAction()
    {
        // do nothing, it's a vegetable
    }

	// Decreases all timers and returns a sprite corresponding to the state of the object if it changed, null otherwise
	public Sprite UpdateGrowth(bool raining, float baseTime)
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
			}

			float growthPercentage = ((baseTime - _timeToGrow) / baseTime) * 100;
			float milestone = ((_currentState + 1) * (100 / _growthSprites.Count));
			if (growthPercentage > milestone && _lastMilestone < milestone)
			{
				milestone = _lastMilestone;
				return _growthSprites[++_currentState];
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

}
