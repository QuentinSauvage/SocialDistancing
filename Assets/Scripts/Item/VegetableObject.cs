using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class VegetableObject : MonoBehaviour
{
	[SerializeField] Vegetable _vegetable;
	Vegetable _instance;
	public Vegetable Vegetable { get { return _instance; } }

	SpriteRenderer _spriteRenderer;

	public void Init(Vegetable v)
	{
		_vegetable = v;
		_instance = Instantiate(v);
	}

	private void Start()
	{
		Init(_vegetable);
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_spriteRenderer.sprite = _instance.Sprites[0];
	}

	public void UpdateGrowth(bool raining)
	{
		Sprite newSprite = _instance.UpdateGrowth(raining, _vegetable.TimeToGrow);
		if(newSprite != null)
		{
			_spriteRenderer.sprite = newSprite;
		}
	}

	public int CheckHarvest()
	{
		if(_instance.State == _instance.Sprites.Count - 1)
		{
			return 1;
		}
		else if(_instance.State == -1)
		{
			return -1;
		}
		return 0;
	}
}
