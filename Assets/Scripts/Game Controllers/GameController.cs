using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(TimeManager), typeof(FishManager), typeof(GardenManager), typeof(ShopManager))]
public class GameController : MonoBehaviour
{
	PlayerController _playerController;
	TimeManager _timeManager;
	InventoryController _inventoryController;
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
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
