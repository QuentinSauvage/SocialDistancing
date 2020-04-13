using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName ="Items/Database",order =-1)]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private Dictionary<string, Item> _database;

    /*
     * Get IItem by its id, returns null if the id is not used
     */
    public Item GetItemByID(string id) 
    {
        try
        {
            return _database[id];
        }catch(KeyNotFoundException)
        {
            return null;
        }
    }

#if UNITY_EDITOR

    [ContextMenu("Fill")]
    public void Fill()
    {
        _database = new Dictionary<string, Item>();

        Debug.Log(typeof(Item).Name);

        foreach (string guid in AssetDatabase.FindAssets("t:Item", new string[]{"Assets/Items"}))
        {
            Debug.Log(guid);
            Item item = AssetDatabase.LoadAssetAtPath<Item>(AssetDatabase.GUIDToAssetPath(guid));
            try
            {           
                _database.Add(item.ID, item);
            }catch(System.ArgumentException)
            {
                Debug.LogError($"{item.Name}'s id({item.ID} is already used");
            }
        }
    }

    [CustomEditor(typeof(ItemDatabase))]
    public class ItemDatabaseEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Debug.Log("non");

            Dictionary<string, Item> dictionary = ((ItemDatabase)target)._database;
            if (dictionary == null) return;
            Debug.Log("gre");

            foreach (Item item in dictionary.Values)
            {
                Debug.Log("oui");
                EditorGUILayout.LabelField(item.Name);
            }
        }
    }


#endif
}


