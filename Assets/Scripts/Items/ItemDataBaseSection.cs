using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ItemDataBaseSection
{
    [SerializeField]
    protected string category = "";
    [SerializeField]
    protected List<ItemBase> items;

    public ItemBase GetItem(int id)
    {
        foreach(ItemBase ib in items)
        {
            if (ib.GetId() == id)
                return ib;
        }
        return null;
    }

    public List<ItemBase> GetItems()
    {
        return items;
    }
}
