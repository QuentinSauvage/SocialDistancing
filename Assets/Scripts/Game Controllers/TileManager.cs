using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TileManager : MonoBehaviour
{
	private List<Tilemap> _tilemaps = new List<Tilemap>();
	private Dictionary<string, System.Action> _actions = new Dictionary<string, System.Action>();

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
	}

	void DoGardening()
	{
		Debug.Log("I love vegetables");
	}

	void OpenDoor()
	{
		Debug.Log("I have no keys");
	}

	void ReadBoard()
	{
		Debug.Log("I can't read");
	}

	void ChopTree()
	{
		Debug.Log("This tree is very specular");
	}
}
