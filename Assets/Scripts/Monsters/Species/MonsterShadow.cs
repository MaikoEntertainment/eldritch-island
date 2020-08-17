using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShadow : Monster
{
    public override List<Item> GetTaskItemCostForThisMonster(Task task, List<Item> currentCosts)
    {
        for(int i=0; i < currentCosts.Count; i++)
        {
            Item itemCost = currentCosts[i];
            if (itemCost.GetId() == 0)
            {
                currentCosts[i] = new Item(itemCost.GetItemBase(), itemCost.GetAmount() / 2);
                break;
            }
        }
        return base.GetTaskItemCostForThisMonster(task, currentCosts);
    }
}
