using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenController : MonoBehaviour
{
	List<Vegetable> _vegetables = new List<Vegetable>();
	public void UpdatePlantation(bool raining)
	{
		foreach(Vegetable v in _vegetables)
		{
			v.UpdateGrowth(raining);
		}
	}
}
