using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DaySegment
{
	[SerializeField] private int _hour;
	[SerializeField] private Color _color;
	[SerializeField] private float _intensity;

	public int Hour
	{
		get { return _hour; }
	}

	public Color Color
	{
		get { return _color; }
	} 

	public float Intensity
	{
		get { return _intensity; }
	}
}
