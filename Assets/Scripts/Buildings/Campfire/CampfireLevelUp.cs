using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Tasks/Campfire/Task Level Up")]
public class CampfireLevelUp : TaskBaseLevelUp
{
    public override List<Item> GetItemCost()
    {
        int level = BuildingMaster.GetInstance().GetBuilding(buildingId).GetLevel();
        if (level == 0) return base.GetItemCost();
        List<Item> trueCosts = new List<Item>();
        foreach (Item cost in base.GetItemCost())
        {
            Item newCost = cost.Clone();
            newCost.ChangeAmount((int)Mathf.Pow((newCost.GetAmount() * level * 0.5f), 1.1f));
            trueCosts.Add(newCost);
        }
        return trueCosts;
    }

}
