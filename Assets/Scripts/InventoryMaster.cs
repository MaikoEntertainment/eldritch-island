﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryMaster : MonoBehaviour
{
    private static InventoryMaster _instance;

    [SerializeField]
    protected List<Tool> tools;
    [SerializeField]
    protected List<Clothes> clothes;
    [SerializeField]
    protected Dictionary<int, Item> items;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static InventoryMaster GetInstance() { return _instance; }

    public Dictionary<int, Item> GetItems() { return items; }
    public Item GetItem(int id) 
    {
        if (items.ContainsKey(id))
            return items[id];
        return null;
    }
    public void ChangeItemAmount(int id, long amount)
    {
        if (!items.ContainsKey(id))
        {
            ItemBase item = ItemMaster.GetInstance().GetItem(id);
            items.Add(item.GetId(), new Item(item, Math.Max(amount,0)));
        }
        items[id].ChangeAmount(amount);
    }

    public List<Tool> GetTools()
    {
        return tools;
    }
    public Tool GetTool(int index)
    {
        return tools[index];
    }

    public void AddTool(Tool tool)
    {
        tools.Add(tool);
    }

   
    public List<Clothes> GetClothes()
    {
        return clothes;
    }
    public Clothes GetClothes(int index)
    {
        return clothes[index];
    }
    public void AddClothes(Clothes clothes)
    {
        this.clothes.Add(clothes);
    }


}
