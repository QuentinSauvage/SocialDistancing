using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(TimeManager), typeof(FishManager), typeof(GardenManager), typeof(ShopManager))]
public class GameController : MonoBehaviour
{
	PlayerController _playerController;
	TimeManager _timeManager;
	InventoryController _inventoryController;
	TileManager _tileManager;
	//FishManager _fishManager;
	//GardenManager _gardenManager;
	//ShopManager _shopManager;

    // Start is called before the first frame update
    void Start()
    {
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

		// Retrieves the time manager
		_timeManager = GetComponent<TimeManager>();

		// Retrieves the tile manager
		_tileManager = GetComponent<TileManager>();
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
		_tileManager.CheckAction(target, hit, null);
		//_tileManager.CheckAction(target, hit, _inventoryController.StackSelected._item) ;
	}
}
