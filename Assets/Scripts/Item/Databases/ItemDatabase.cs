using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName ="Items/Database",order =-1)]
public class ItemDatabase : ScriptableObject
{
    [System.Serializable]
    internal class ItemDictionnary : SerializableDictionary<string, Item> { }

    [SerializeField] private ItemDictionnary _database = ItemDictionnary.New<ItemDictionnary>();

    /*
     * Get IItem by its id, returns null if the id is not used
     */
    public Item GetItemByID(string id) 
    {
        try
        {
            return Instantiate(_database.dictionary[id]);
        }catch(KeyNotFoundException)
        {
            return null;
        }
    }

#if UNITY_EDITOR

    [ContextMenu("Fill")]
    public void Fill()
    {
        _database.dictionary.Clear();

        foreach (string guid in AssetDatabase.FindAssets("t:Item", new string[]{"Assets/Items"}))
        {
            Item item = AssetDatabase.LoadAssetAtPath<Item>(AssetDatabase.GUIDToAssetPath(guid));
            try
            {           
                _database.dictionary.Add(item.ID, item);
            }catch(System.ArgumentException)
            {
                Debug.LogError($"{item.Name}'s id({item.ID} is already used");
            }
        }
    }
#endif
}


