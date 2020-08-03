
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDatabase : ScriptableObject
{
    [SerializeField]
    protected List<ItemDataBaseSection> sections = new List<ItemDataBaseSection>();
    protected Dictionary<int, ItemBase> itemsDictionary = new Dictionary<int, ItemBase>();

    public ItemDatabase()
    {
        InitializeDictionary();
    }

    protected void InitializeDictionary()
    {
        foreach (ItemDataBaseSection section in sections)
        {
            foreach(ItemBase ib in section.GetItems())
            {
                itemsDictionary.Add(ib.GetId(), ib);
            }
        }
    }

    public Dictionary<int, ItemBase> GetItems()
    {
        return itemsDictionary;
    }

    public ItemBase GetItem(int id) {
        if (itemsDictionary.ContainsKey(id))
        {
            return itemsDictionary[id];
        }
        return null;
    }

}
