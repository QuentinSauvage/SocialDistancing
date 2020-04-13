using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;

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
	[SerializeField] Light2D _light;
	[SerializeField] Color _midnightColor;
	[SerializeField] Color _middayColor;
	[SerializeField] Color _dawnColor;
	[SerializeField] Color _duskColor;
	[SerializeField] float _middayIntensity;

	// Start is called before the first frame update
	void Start()
    {
		_hour = 0;
		_minute = 0;
		_elapsedTime = 0;
		_idleUI.SetActive(false);

		UpdateText();
		UpdateSunLight();
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
			UpdateSunLight();
		}
		
	}

	void UpdateText()
	{
		_timerText.text = (_hour >= 10) ? _hour.ToString("F0") : '0' + _hour.ToString("F0");
		_timerText.text += ":";
		_timerText.text += (_minute >= 10) ? _minute.ToString("F0") : '0' + _minute.ToString("F0");
	}

	void UpdateSunLight()
	{
		// dusk - midnight
		if (_hour >= 18)
		{
			float tLerp = ((_hour - 18) * 60 + _minute) / (6 * 60);
			_light.color = Color.Lerp(_duskColor, _midnightColor, tLerp);
		}
		// midday - dusk
		else if (_hour >= 12)
		{
			float tLerp = ((_hour - 12) * 60 + _minute) / (6 * 60);
			_light.intensity = Mathf.Lerp(_middayIntensity, 1, tLerp);
			_light.color = Color.Lerp(_middayColor, _duskColor, tLerp);
		}
		// dawn - midday
		else if (_hour >= 6)
		{
			float tLerp = ((_hour - 6) * 60 + _minute) / (6 * 60);
			_light.intensity = Mathf.Lerp(1, _middayIntensity, tLerp);
			_light.color = Color.Lerp(_dawnColor, _middayColor, tLerp);
		}
		// midnight - dawn
		else
		{
			float tLerp = (_hour * 60 + _minute) / (6 * 60);
			_light.color = Color.Lerp(_midnightColor, _dawnColor, tLerp);
		}
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
