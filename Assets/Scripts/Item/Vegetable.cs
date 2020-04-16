using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Vegetable")]
public class Vegetable : Salable
{
	[Header("In-game minutes before being fully grown")]
	[SerializeField] int _timeToGrow;
	[Header("In-game minutes before wilting")]
	[SerializeField] int _timeBeforeWilting;
	[Header("In-game minutes before wilting, if not watered, 0 means it doesn't need water")]
	[SerializeField] int _waterTimer;
	[SerializeField] int _quantity;
	[SerializeField] List<Sprite> _growthSprites;
	[SerializeField] Sprite _wiltedSprite;

	public int Quantity { get { return _quantity; } }

    public override void DoAction()
    {
        // do nothing, it's a vegetable
    }

	public void UpdateGrowth(bool raining)
	{
		if(_timeToGrow > 0)
		{
			--_timeToGrow;
			if(!raining)
			{
				--_waterTimer;
			}
		}
		else
		{
			--_timeBeforeWilting;
		}
	}

}
