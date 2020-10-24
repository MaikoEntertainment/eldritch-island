using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShadow : Monster
{
    public override List<Item> GetTaskItemCostForThisMonster(Task task, List<Item> currentCosts)
    {
        List<Item> costs = new List<Item>();
        for (int i=0; i < currentCosts.Count; i++)
        {
            Item itemCost = currentCosts[i];
            if (itemCost.GetId() == 0)
            {
                costs.Add(new Item(itemCost.GetItemBase(), itemCost.GetAmount() / 2));
            }
            else
            {
                costs.Add(new Item(itemCost.GetItemBase(), itemCost.GetAmount()));
            }
        }
        return base.GetTaskItemCostForThisMonster(task, costs);
    }
}
