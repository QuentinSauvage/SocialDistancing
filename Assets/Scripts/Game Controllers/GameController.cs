﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TimeManager), typeof(GardenController), typeof(TileManager))]
public class GameController : MonoBehaviour
{
	PlayerController _playerController;
	public PlayerController Player { get { return _playerController; } }

	TimeManager _timeManager;
	InventoryController _inventoryController;
	TileManager _tileManager;
    [SerializeField] SellingController _sellingController;
    [SerializeField] BuyingController _buyingController;

    [SerializeField] FishingController _fishingController;
	GardenController _gardenController;

	public static bool _gamePaused;
	public static bool _inventoryOpened;
	[SerializeField] GameObject _pauseMenu;

	// Used to know what has been hit by the ray
	private GameObject _hitObject;
	[SerializeField] GameObject _vegetablePrefab;

    // Start is called before the first frame update
    void Start()
    {
		_gamePaused = false;
		_pauseMenu.SetActive(false);
		_inventoryOpened = false;

		// Retrieves the player controller
		GameObject player = GameObject.Find("Player");
		if(player != null)
		{
			_playerController = player.GetComponent<PlayerController>();
		}


		// Retrieves the inventory controller
		GameObject inventory = GameObject.Find("Inventory");
		if (inventory != null)
		{
			_inventoryController = inventory.GetComponent<InventoryController>();
		}

		// Retrieves the garden controller
		_gardenController = GetComponent<GardenController>();

		// Retrieves the time manager
		_timeManager = GetComponent<TimeManager>();

		// Retrieves the tile manager
		_tileManager = GetComponent<TileManager>();

        //listen to the events
        _fishingController._hookEvent.AddListener((Fish.FishRarity rarity) => { _playerController.OnStartHooking((int) rarity); });
        _fishingController._succesEvent.AddListener((Fish fish) => { Debug.Log($"Success fishing the {fish.Name}"); _inventoryController.AddItem(fish, 1); });
        _fishingController._failEvent.AddListener(() => { Debug.Log($"Didn't hook at time or hook to soon"); /**need sad animation and sound**/ });

        //set fishing rod ui event
        _fishingController.UI.SetOnclick(_inventoryController.SlotClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

	// Asks the time manager to display the idle UI
	public void DisplayTime()
	{
		_timeManager.DisplayTime();
	}

	// Asks the time manager to hide the display UI
	public void HideTime()
	{
		_timeManager.HideTime();
	}

    public float GetHour()
    {
        return _timeManager.Hour;
    }

    public bool IsRaining()
    {
        return _timeManager.IsRaining;
    }

    public InventoryController.Stack GetBarSelectedStack()
    {
        return _inventoryController.StackSelected;
    }

	// Check if the player can interact with the tiles it is facing.
	// This function will firstly check if the actions that don't require any tool,
	// then it will check if an action can be done with the item selected in the inventory
	public void CheckAction(Vector3Int target, RaycastHit2D hit, bool secondAction=false)
	{
		if(hit.collider != null)
		{
			_hitObject = hit.transform.gameObject;
		}
		_tileManager.CheckAction(target, hit, null,secondAction);
		//_tileManager.CheckAction(target, hit, _inventoryController.StackSelected._item) ;
	}

    /// <summary>
    /// Called by TileManagerEvent, manage the fishing actions
    /// </summary>
    public void FishAction(bool secondary)
    {
        if (!secondary)
            _fishingController.ActionPressed(this);
        else
            _fishingController.SecondPressed(this);
    }

	/// <summary>
	/// Called by TileManagerEvent, manage the garden actions
	/// </summary>
	public void GardenAction()
	{
		Vegetable v = (Vegetable) _inventoryController.StackSelected._item;
		if(v != null && _inventoryController.StackSelected._nbItem > 0)
		{
			Vector3 initPos = _playerController.TileLookedPosition();
			initPos.x += 0.5f;
			GameObject g = Instantiate(_vegetablePrefab, initPos, Quaternion.identity);
			VegetableObject vo = g.AddComponent<VegetableObject>();
			Vegetable v2 = (Vegetable) _inventoryController.RemoveItemFromBarSelected(1);
			vo.Init(v2);
			_gardenController.AddVegetable(vo);
		}
	}

	/// <summary>
	/// Called by TileManagerEvent, manage the harvest actions
	/// </summary>
	public void HarvestAction()
	{
		VegetableObject vo = _hitObject.GetComponent<VegetableObject>();
		if (vo != null)
		{
			int state = vo.CheckHarvest();
			if (state == 1 || state == -1)
			{
				Destroy(_hitObject);
				_gardenController.RemoveVegetable(vo);
				Vegetable v = vo.Vegetable;

				if (state == 1)
				{
					_inventoryController.AddItem(v, v.Quantity);
				}
			}
		}
	}

	public void OnCloseMenu(InputAction.CallbackContext context)
	{
        bool closedSomething = false;
		int nbOpen = 0;
		if(_inventoryOpened)
		{
			++nbOpen;
            closedSomething = true;
			_inventoryOpened = false;
			_inventoryController.ToggleInventory2();
		}
        if(_fishingController.UI.IsVisible)
        {
			++nbOpen;
			closedSomething = true;
            _fishingController.UI.Toggle(null);
        }
        if(_sellingController.IsVisible)
        {
			++nbOpen;
			closedSomething = true;
            _sellingController.Toggle();
        }
        if(_buyingController.IsVisible)
        {
			++nbOpen;
			closedSomething = true;
            _buyingController.Toggle();
        }
		if (!closedSomething)
		{
			OnPause2();
		}
		if (nbOpen > 0 && closedSomething)
		{
			Player.UnFreeze();
		}
	}

	// Function called when the game is paused using a key
	public void OnPause(InputAction.CallbackContext context)
	{
		OnPause2();
	}

	// Function called by using a key or a button
	public void OnPause2()
	{
		if(!_inventoryOpened)
		{
			Time.timeScale = (_gamePaused) ? 1 : 0;
			_gamePaused = !_gamePaused;
			_timeManager.PauseMusic(_gamePaused);
			_pauseMenu.SetActive(_gamePaused);
		}
	}

	public void OnMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void OnQuitGame()
	{
		Application.Quit();
	}

	// Calls the GardenController to update every planted vegetables
	public void UpdatePlantation(bool raining)
	{
		_gardenController.UpdatePlantation(raining);
	}

	public void OnStartSkippingTime(InputAction.CallbackContext context)
	{
		_timeManager.StartSkippingTime();
	}

	public void OnStopSkippingTime(InputAction.CallbackContext context)
	{
		_timeManager.StopSkippingTime();
	}
}
