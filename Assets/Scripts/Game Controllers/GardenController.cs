using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenController : MonoBehaviour
{
	[SerializeField] List<VegetableObject> _vegetables = new List<VegetableObject>();

	public void UpdatePlantation(bool raining)
	{
		foreach(VegetableObject v in _vegetables)
		{
			v.UpdateGrowth(raining);
		}
	}

	public void AddVegetable(VegetableObject v)
	{
		_vegetables.Add(v);
	}

	public void RemoveVegetable(VegetableObject v)
	{
		_vegetables.Remove(v);
	}
}
