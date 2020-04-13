using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
	// Current hour in the game
	private float _hour;
	// Current minute in the game
	private float _minute;
	private float _elapsedTime;

	// Idle UI
	[SerializeField] GameObject _idleUI;
	[SerializeField] TextMeshProUGUI _timerText;

	// Start is called before the first frame update
	void Start()
    {
		_hour = 0;
		_minute = 0;
		_elapsedTime = 0;
		_idleUI.SetActive(false);

		UpdateText();
	}

	// Update is called once per frame
	void Update()
    {
		_elapsedTime += Time.deltaTime;

		// 3 real seconds = 1 minute
		if(_elapsedTime >= 3)
		{
			if(_minute < 60)
			{
				++_minute;
			}
			else
			{
				_minute = 0;
				if(_hour < 24)
				{
					++_hour;
				}
				else
				{
					_hour = 0;
				}
			}
			_elapsedTime = 0;

			UpdateText();
		}
		
	}

	void UpdateText()
	{
		_timerText.text = (_hour >= 10) ? _hour.ToString("F0") : '0' + _hour.ToString("F0");
		_timerText.text += ":";
		_timerText.text += (_minute >= 10) ? _minute.ToString("F0") : '0' + _minute.ToString("F0");
	}

	public void DisplayTime()
	{
		_idleUI.SetActive(true);
	}

	public void HideTime()
	{
		_idleUI.SetActive(false);
	}
}
