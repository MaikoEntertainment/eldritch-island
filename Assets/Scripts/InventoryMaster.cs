using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryMaster : MonoBehaviour
{
    private static InventoryMaster _instance;

    [SerializeField]
    protected List<Tool> tools = new List<Tool>();
    [SerializeField]
    protected List<Clothes> clothes = new List<Clothes>();
    [SerializeField]
    protected Dictionary<int, Item> items = new Dictionary<int, Item>();

    public delegate void NewItem(Item i);
    public NewItem OnNewItem;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    // Only load after Awake
    public void Load(SaveInventory save)
    {
        foreach(SaveItem si in save.savedItems)
        {
            ItemBase ib = ItemMaster.GetInstance().GetItem(si.GetId());
            if (ib)
                items.Add(si.GetId(), new Item(ib, si.GetAmount()));
        }
        foreach (SaveTool st in save.savedTools)
        {
            ToolBase tb = ToolsMaster.GetInstance().GetTool(st.GetId());
            tools.Add(new Tool(tb, st.GetDurabilityUsed(), st.GetTier()));
        }
        foreach (SaveClothes sc in save.savedClothes)
        {
            ClothesBase cb = ClothesMaster.GetInstance().GetClothes(sc.GetId());
            clothes.Add(new Clothes(cb, sc.GetDurabilityUsed(), sc.GetTier()));
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
            Item newItem = new Item(item, Math.Max(amount, 0));
            items.Add(item.GetId(), newItem);
            OnNewItem?.Invoke(newItem);
            newItem.onAmountChange?.Invoke(newItem, amount);
        }
        else
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
    public void RemoveTool(Tool tool)
    {
        tools.Remove(tool);
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
    public void RemoveClothes(Clothes clothes)
    {
        this.clothes.Remove(clothes);
    }

    public Tool CreateTool(ToolBase toolBase, int craftingPower)
    {
        float templeBonus = 1 + UpgradeMaster.GetInstance().GetUpgrade(UpgradeId.CraftingPower).GetBonus();
        double upgradeChance = Math.Pow(0.1f, 1 / (craftingPower * 0.25f * templeBonus + GetWorkShopLevelCraftMod()));
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
        float templeBonus = 1 + UpgradeMaster.GetInstance().GetUpgrade(UpgradeId.CraftingPower).GetBonus();
        double upgradeChance = Math.Pow(0.1f, 1 / (craftingPower * 0.25f * templeBonus + GetWorkShopLevelCraftMod()));
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
