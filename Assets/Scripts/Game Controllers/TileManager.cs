using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class TileManager : MonoBehaviour
{
	private List<Tilemap> _tilemaps = new List<Tilemap>();
	private Dictionary<string, System.Action> _actions = new Dictionary<string, System.Action>();

    public UnityEvent _fishingEvent, _gardentEvent, _openDoorEvent, _readBoardEvent, _chopTreeEvent;

	// Start is called before the first frame update
	void Awake()
	{
		_actions["Water"] = DoFishing;
		_actions["Garden"] = DoGardening;
		_actions["Door"] = OpenDoor;
		_actions["Board"] = ReadBoard;
		_actions["Tree"] = ChopTree;

		foreach (Tilemap t in GameObject.FindObjectsOfType<Tilemap>())
		{
			_tilemaps.Add(t);
		}
	}

	public void CheckAction(Vector3Int target, RaycastHit2D hit, Item selectedItem)
	{
		if(hit.collider != null)
		{
			if (_actions.ContainsKey(hit.transform.gameObject.tag))
			{
				_actions[hit.transform.gameObject.tag]();
			}
		}
		foreach(Tilemap t in _tilemaps)
		{
			TileBase tb = t.GetTile(target);
			if(tb != null)
			{
				if (_actions.ContainsKey(t.name))
				{
					_actions[t.name]();
				}
				else if(_actions.ContainsKey(tb.name))
				{
					_actions[tb.name]();
				}
			}
		}
	}

	void DoFishing()
	{
		Debug.Log("I love fishing");
        _fishingEvent.Invoke();
	}

	void DoGardening()
	{
		Debug.Log("I love vegetables");
        _gardentEvent.Invoke();
	}

	void OpenDoor()
	{
		Debug.Log("I have no keys");
        _openDoorEvent.Invoke();
	}

	void ReadBoard()
	{
		Debug.Log("I can't read");
        _readBoardEvent.Invoke();
	}

	void ChopTree()
	{
		Debug.Log("This tree is very specular");
        _chopTreeEvent.Invoke();
	}
}
