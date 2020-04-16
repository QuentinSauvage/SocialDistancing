using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;

public class TimeManager : MonoBehaviour
{
	//************** START AMBIENT MUSIC **************\\
	private AudioSource _audioSource;
	[SerializeField] private AudioClip[] _ambientMusics = new AudioClip[8];

	//************** END AMBIENT MUSIC **************\\

	//************** START TIME HANDLING **************\\

	// Current hour in the game
	private float _hour;
	// Current minute in the game
	private float _minute;
	// Time since last in-game minute
	private float _elapsedTime;
	[SerializeField] private float _secondDuration = 3;

	private static float MIDNIGHT = 0;
	private static float DAWN = 6;
	private static float MIDDAY = 14;
	private static float DUSK = 20;

	[Header("Time Handling")]
	// Idle UI
	[SerializeField] private GameObject _idleUI;
	// Hour displayed on the screen
	[SerializeField] private TextMeshProUGUI _timerText;

	//************** END TIME HANDLING **************\\


	//************** START LIGHT HANDLING **************\\
	[Header("Light Handling")]
	// Global light of the game
	[SerializeField] private Light2D _light;

	// Day/Night colors
	[SerializeField] DaySegment[] _daySegments = new DaySegment[4];

	//************** END LIGHT HANDLING **************\\


	//************** START WEATHER HANDLING **************\\

	[Header("Weather Handling")]
	private ParticleSystem _weatherSystem;
	private int _currentWeather;
	[SerializeField] private List<Weather> _weathers;
	[SerializeField] private float _weatherIntensity;
	private float _weatherTimer;

	//************** END WEATHER HANDLING **************\\

    //********** ACCESSOR ************\\

    public float Hour { get { return _hour; } }
    public bool IsRaining { get { return _weathers[_currentWeather].Name == "RAIN"; } }

    //******** END ACCESSOR **********\\

	private void Awake()
	{
		_audioSource = gameObject.AddComponent<AudioSource>();
	}

	// Start is called before the first frame update
	void Start()
    {
		_hour = 6;
		_minute = 0;
		_elapsedTime = 0;
		_weatherIntensity = 0;
		_idleUI.SetActive(false);

		_weatherSystem = GetComponent<ParticleSystem>();
		_weatherSystem.Stop();
		_currentWeather = 0;
		_weatherTimer = 0;

		UpdateCurrentMusic();
		UpdateText();
		UpdateSunLight();
	}

	// Update is called once per frame
	void Update()
    {
		_elapsedTime += Time.deltaTime;

		if(_currentWeather != 0)
		{
			_weatherTimer += Time.deltaTime;
			if(_weatherTimer > _weatherSystem.main.duration)
			{
				_currentWeather = 0;
				_weatherSystem.Stop();
				_weatherIntensity = 0;
				_weatherTimer = 0;
				CheckWeatherChanges();
			}
		}

		// 3 real seconds = 1 minute
		if(_elapsedTime >= _secondDuration)
		{
			if(_minute < 60)
			{
				++_minute;
			}
			else
			{
				CheckWeatherChanges();
				_minute = 0;
				if(_hour < 23)
				{
					++_hour;
				}
				else
				{
					_hour = 0;
				}
				UpdateCurrentMusic();
			}
			_elapsedTime = 0;

			UpdateText();
			UpdateSunLight();
		}
		
	}

	//Update the hours and minutes displayed on the idle UI
	private void UpdateText()
	{
		_timerText.text = (_hour >= 10) ? _hour.ToString("F0") : '0' + _hour.ToString("F0");
		_timerText.text += ":";
		_timerText.text += (_minute >= 10) ? _minute.ToString("F0") : '0' + _minute.ToString("F0");
	}

	//Update the color and the intensity of the global light, according to the time of the day
	private void UpdateSunLight()
	{
		// dusk - midnight
		if (_hour >= DUSK)
		{
			float tLerp = ((_hour - _daySegments[2].Hour) * 60 + _minute) / (4 * 60);
			_light.intensity = Mathf.Lerp(_daySegments[2].Intensity - _weatherIntensity, _daySegments[3].Intensity - _weatherIntensity / 2, tLerp);
			_light.color = Color.Lerp(_daySegments[2].Color, _daySegments[3].Color, tLerp);
		}
		// midday - dusk
		else if (_hour >= MIDDAY)
		{
			float tLerp = ((_hour - _daySegments[1].Hour) * 60 + _minute) / (6 * 60);
			_light.intensity = Mathf.Lerp(_daySegments[1].Intensity - _weatherIntensity, _daySegments[2].Intensity - _weatherIntensity, tLerp);
			_light.color = Color.Lerp(_daySegments[1].Color, _daySegments[2].Color, tLerp);
		}
		// dawn - midday
		else if (_hour >= DAWN)
		{
			float tLerp = ((_hour - _daySegments[0].Hour) * 60 + _minute) / (8 * 60);
			_light.intensity = Mathf.Lerp(_daySegments[0].Intensity - _weatherIntensity / 2, _daySegments[1].Intensity - _weatherIntensity, tLerp);
			_light.color = Color.Lerp(_daySegments[0].Color, _daySegments[1].Color, tLerp);
		}
		// midnight - dawn
		else
		{
			float tLerp = ((_hour - _daySegments[3].Hour) * 60 + _minute) / (6 * 60);
			_light.intensity = Mathf.Lerp(_daySegments[3].Intensity - _weatherIntensity / 2, _daySegments[0].Intensity - _weatherIntensity, tLerp);
			_light.color = Color.Lerp(_daySegments[3].Color, _daySegments[0].Color, tLerp);
		}
	}

	// Displays the idle UI
	public void DisplayTime()
	{
		_idleUI.SetActive(true);
	}

	// Hides the idle UI
	public void HideTime()
	{
		_idleUI.SetActive(false);
	}

	// Checks if the weather should change, and if so, modify the weather system
	// Return true if the weather changes
	private bool CheckWeatherChanges()
	{
		float currentProba = 0;
		for(int i = 1; i < _weathers.Count; ++i)
		{
			int weatherCheck = Random.Range(0, 100);
			if (weatherCheck - currentProba < _weathers[i].Probability)
			{
				_currentWeather = i;
				_weatherIntensity = _weathers[i].Intensity;

				//the new weather should last from 2 hours to 10 hours
				float weatherDuration = Random.Range(_secondDuration * 120, _secondDuration * 600);
				ParticleSystem.MainModule main = _weatherSystem.main;
				main.duration = weatherDuration;
				_weatherSystem.Play();
				return true;
			}
			currentProba += _weathers[i].Probability;
		}
		return false;
	}

	// Checks if the day section has changed, and if so, plays the next music
	private void UpdateCurrentMusic()
	{
		// The music changes every 3 hours
		if(Mathf.Approximately(_hour % 3, 0))
		{
			int daySection = (int)(_hour / 3.0f);
			_audioSource.Stop();
			_audioSource.clip = _ambientMusics[daySection];
			_audioSource.Play();
		}
	}
}
