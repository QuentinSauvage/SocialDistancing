using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(TimeManager), typeof(GardenManager), typeof(ShopManager))]
public class GameController : MonoBehaviour
{
	PlayerController _playerController;
	TimeManager _timeManager;
	InventoryController _inventoryController;
	TileManager _tileManager;
    //GardenManager _gardenManager;
    //ShopManager _shopManager;

    [SerializeField] FishingController _fishingController;


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
	public void CheckAction(Vector3Int target, RaycastHit2D hit)
	{
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
}
