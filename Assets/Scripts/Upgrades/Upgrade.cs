using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    protected UpgradeBase upgradeBase;
    protected int level;

    public Upgrade(UpgradeBase upgrade, int level)
    {
        upgradeBase = upgrade;
        this.level = level;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public UpgradeBase GetUpgradeBase()
    {
        return upgradeBase;
    }

    public int GetLevel() { return level; }

    public int LevelUp(int levels = 1)
    {
        if (CanPay(levels))
        {
            List<Item> itemCost = upgradeBase.GetLevelUpCost(GetLevel(), levels);
            foreach (Item ic in itemCost)
            {
                InventoryMaster.GetInstance().ChangeItemAmount(ic.GetId(), -1 * ic.GetAmount());
            }
            level += levels;
        }
        return level;
    }

    public List<Item> GetLevelUpCost()
    {
        return upgradeBase.GetLevelUpCost(GetLevel(), 1);
    }

    public bool CanPay(int levels = 1)
    {
        List<Item> itemCost = upgradeBase.GetLevelUpCost(GetLevel(), levels);
        foreach(Item ic in itemCost)
        {
            Item itemInventory = InventoryMaster.GetInstance().GetItem(ic.GetId());
            if (itemInventory == null || itemInventory.GetAmount() < ic.GetAmount())
                return false;
        }
        return true;
    }

    public float GetBonus()
    {
        return upgradeBase.GetBonus(GetLevel());
    }
}
