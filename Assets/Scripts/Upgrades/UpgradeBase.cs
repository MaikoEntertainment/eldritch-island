using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "Upgrades/Upgrade Base")]
public class UpgradeBase : ScriptableObject
{
    public UpgradeId id;
    public float baseValue = 0.95f;
    public Sprite icon;
    public new TextLanguageOwn name;
    public TextLanguageOwn description;
    public TextLanguageOwn unlockCondition;
    public List<Item> upgradeCost;
    public List<TaskBuildingRequirement> buildingRequirements;
    public List<StatisticNumberRequirement> statisticRequirements;

    public Sprite GetIcon() { return icon; }
    public string GetDescription() { return description.GetText(); }
    public string GetUnlockCondition() { return unlockCondition.GetText(); }
    public bool IsAvailable()
    {
        foreach (TaskBuildingRequirement br in buildingRequirements)
        {
            int level = BuildingMaster.GetInstance().GetBuilding(br.GetBuildingId()).GetLevel();
            if (level < br.GetMinLevelRequired())
                return false;
        }
        foreach (StatisticNumberRequirement sr in statisticRequirements)
        {
            if (!sr.MeetsCondition())
                return false;
        }
        return true;
    }

    public List<Item> GetLevelUpCost(int currentLevel, int levels = 1)
    {
        Dictionary<int,Item> finalUpgradeCost = new Dictionary<int, Item>();
        int levelGoal = currentLevel + levels;
        for(int l=currentLevel; l<levelGoal; l++)
        {
            foreach(Item ic in upgradeCost)
            {
                if (finalUpgradeCost.ContainsKey(ic.GetId()))
                    finalUpgradeCost[ic.GetId()].ChangeAmount(ic.GetAmount() * (l + 1));
                else
                    finalUpgradeCost.Add(ic.GetId(), new Item(ic.GetItemBase(), ic.GetAmount() * (l + 1)));
            }
        }
        return finalUpgradeCost.Values.ToList();
    }

    public virtual float GetBonus(int level=0)
    {
        return 1 - Mathf.Pow(baseValue, level);
    }
    public virtual string GetBonusUI(int level)
    {
        return (GetBonus(level) * 100).ToString("F0")+"%";
    }
}
