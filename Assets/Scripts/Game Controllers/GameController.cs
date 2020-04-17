using System.Collections;
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
    //ShopManager _shopManager;

    [SerializeField] FishingController _fishingController;
	GardenController _gardenController;

	public static bool _gamePaused;
	[SerializeField] GameObject _pauseMenu;

	// Used to know what has been hit by the ray
	private GameObject _hitObject;
	[SerializeField] GameObject _vegetablePrefab;

    // Start is called before the first frame update
    void Start()
    {
		_gamePaused = false;
		_pauseMenu.SetActive(false);

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
        _fishingController._hookEvent.AddListener((Fish.FishRarity rarity) => { Debug.Log($"need hooking for {rarity.ToString()}");/**need player animation and sound*/ });
        _fishingController._succesEvent.AddListener((Fish fish) => { Debug.Log($"Success fishing the {fish.Name}"); _inventoryController.AddItem(fish, 1); });
        _fishingController._failEvent.AddListener(() => { Debug.Log($"Didn't hook at time or hook to soon"); /**need sad animation and sound**/ });

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

	// Check if the player can interact with the tiles it is facing.
	// This function will firstly check if the actions that don't require any tool,
	// then it will check if an action can be done with the item selected in the inventory
	public void CheckAction(Vector3Int target, RaycastHit2D hit)
	{
		if(hit.collider != null)
		{
			_hitObject = hit.transform.gameObject;
		}
		_tileManager.CheckAction(target, hit, null);
		//_tileManager.CheckAction(target, hit, _inventoryController.StackSelected._item) ;
	}

    /// <summary>
    /// Called by TileManagerEvent, manage the fishing actions
    /// </summary>
    public void FishAction()
    {
        _fishingController.ActionPressed(this);
    }

	/// <summary>
	/// Called by TileManagerEvent, manage the garden actions
	/// </summary>
	public void GardenAction()
	{
		Vegetable v = (Vegetable) _inventoryController.StackSelected._item;
		if(v != null && _inventoryController.StackSelected._nbItem > 0)
		{
			GameObject g = Instantiate(_vegetablePrefab, _playerController.transform.position, Quaternion.identity);
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
		int state = vo.CheckHarvest();
		if (vo != null && (state == 1 || state == -1))
		{
			Destroy(_hitObject);
			_gardenController.RemoveVegetable(vo);
			Vegetable v = vo.Vegetable;

			if(state == 1)
			{
				_inventoryController.AddItem(v, v.Quantity);
			}
		}
	}

	public void OnCloseMenu(InputAction.CallbackContext context)
	{
		if(_inventoryController.Inventory.IsVisible)
		{
			_inventoryController.ToggleInventory2();
		}
		else
		{
			OnPause2();
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
		if(!_inventoryController.Inventory.IsVisible)
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
