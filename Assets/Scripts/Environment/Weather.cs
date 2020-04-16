using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Weather
{
	[SerializeField] private string _name;
	[SerializeField] private int _probability;
	[SerializeField] private float _intensity;
	[SerializeField] private bool _watersVegetables;
	// ParticleSystem?

	public int Probability
	{
		get { return _probability; }
	}

	public float Intensity
	{
		get { return _intensity; }
	}

	public bool WatersVegetables
	{
		get { return _watersVegetables; }
	}

    public string Name
    {
        get { return _name; }
    }
}
