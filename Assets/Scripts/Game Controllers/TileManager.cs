using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

[System.Serializable]
public class TileActionEvent : UnityEvent<bool> { }

public class TileManager : MonoBehaviour
{
	private List<Tilemap> _tilemaps = new List<Tilemap>();
	private Dictionary<string, System.Action<bool>> _actions = new Dictionary<string, System.Action<bool>>();

    public TileActionEvent _fishingEvent, _gardentEvent, _openDoorEvent, _readBoardEvent, _chopTreeEvent, _harvestEvent;

	// Start is called before the first frame update
	void Awake()
	{
		_actions["Water"] = DoFishing;
		_actions["Garden"] = PlantVegetable;
		_actions["Vegetable"] = DoHarvest;
		_actions["Door"] = OpenDoor;
		_actions["Board"] = ReadBoard;
		_actions["Tree"] = ChopTree;

		foreach (Tilemap t in GameObject.FindObjectsOfType<Tilemap>())
		{
			_tilemaps.Add(t);
		}
	}

	public void CheckAction(Vector3Int target, RaycastHit2D hit, Item selectedItem, bool secondary=false)
	{
		if(hit.collider != null)
		{
			if (_actions.ContainsKey(hit.transform.gameObject.tag))
			{
				_actions[hit.transform.gameObject.tag](secondary);
			}
			return;
		}
		foreach(Tilemap t in _tilemaps)
		{
			TileBase tb = t.GetTile(target);
			if(tb != null)
			{
				if (_actions.ContainsKey(t.name))
				{
					_actions[t.name](secondary);
				}
				else if(_actions.ContainsKey(tb.name))
				{
					_actions[tb.name](secondary);
				}
			}
		}
	}

	void DoFishing(bool secondary)
	{
		Debug.Log("I love fishing");
        _fishingEvent.Invoke(secondary);
	}

	void PlantVegetable(bool secondary)
	{
		Debug.Log("I love vegetables");
        _gardentEvent.Invoke(secondary);
	}

	void DoHarvest(bool secondary)
	{
		Debug.Log("Harvest");
		_harvestEvent.Invoke(secondary);
	}

	void OpenDoor(bool secondary)
	{
		Debug.Log("I have no keys");
        _openDoorEvent.Invoke(secondary);
	}

	void ReadBoard(bool secondary)
	{
		Debug.Log("I can't read");
        _readBoardEvent.Invoke(secondary);
	}

	void ChopTree(bool secondary)
	{
		Debug.Log("This tree is very specular");
        _chopTreeEvent.Invoke(secondary);
	}
}
