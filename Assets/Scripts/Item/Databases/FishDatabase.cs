using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Items/FishDatabase")]
public class FishDatabase : ScriptableObject
{
    [SerializeField] private List<Fish> _fish;

    /// <summary>
    /// Get a list of possible fish to get depending on the conditions
    /// </summary>
    /// <param name="isRaining">true if it's raining</param>
    /// <param name="hour">hour of the game</param>
    /// <param name="bait">bait used, can be null</param>
    /// <param name="list">lsit to be filled, need to be init but not cleared</param>
    public void GetPossibleFish(bool isRaining, int hour, Bait bait, List<Fish> list)
    {
        list.Clear();
        foreach(Fish fish in _fish)
        {
            if (fish.CanBeFished(isRaining, bait, hour))
                list.Add(fish);
        }
    }

#if UNITY_EDITOR

    [ContextMenu("Fill")]
    public void Fill()
    {
        _fish = new List<Fish>();

        foreach (string guid in AssetDatabase.FindAssets("t:Item", new string[] { "Assets/Items/Fish" }))
        {
            Fish item = (Fish)AssetDatabase.LoadAssetAtPath<Item>(AssetDatabase.GUIDToAssetPath(guid));
            _fish.Add(item);
        }
    }
#endif
}


