using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMaster : MonoBehaviour
{
    private static ItemMaster _instance;

    [SerializeField]
    protected ItemDatabase database;
    
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            database.InitializeDictionary();
        }
        else
        {
            Destroy(this);
        }
    }
    public static ItemMaster GetInstance() { return _instance; }

    public Dictionary<int, ItemBase> GetItems()
    {
        return database.GetItems();
    }

    public ItemBase GetItem(int id)
    {
        return database.GetItem(id);
    }

    public Tool CreateTool(ToolBase toolBase, int craftingPower)
    {
        
        double upgradeChance = Math.Pow(0.1f, 1 / (craftingPower * 0.25f + GetWorkShopLevelCraftMod()));
        int tier = 0;
        double random = UnityEngine.Random.value;
        while (random < upgradeChance && tier < 99)
        {
            tier++;
            random = UnityEngine.Random.value;
        }
        Tool tool = new Tool(toolBase, tier);
        return tool;
    }

    public Clothes CreateClothes(ClothesBase clothesBase, int craftingPower)
    {
        double upgradeChance = Math.Pow(0.1f, 1 / (craftingPower * 0.75f + GetWorkShopLevelCraftMod()));
        int tier = 0;
        double random = UnityEngine.Random.value;
        while (random < upgradeChance && tier < 99)
        {
            tier++;
            random = UnityEngine.Random.value;
        }
        Clothes tool = new Clothes(clothesBase, tier);
        return tool;
    }

    private float GetWorkShopLevelCraftMod()
    {
        int workshopLevel = BuildingMaster.GetInstance().GetBuilding(BuildingIds.CraftHouse).GetLevel();
        return 1 + 0.2f * workshopLevel;
    }
}
