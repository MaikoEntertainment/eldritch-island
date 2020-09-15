using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIItemToolBarChangeSecond : MonoBehaviour
{
    public TextMeshProUGUI text;

    protected Item item;

    public void Load(Item i)
    {
        item = i;
        CalculateChangePerSecond();
    }

    public void CalculateChangePerSecond()
    {
        double itemAmountPerSecond = 0;
        foreach(Building b in BuildingMaster.GetInstance().GetUnlockedBuildings())
        {
            foreach(Task task in b.GetActiveTasks())
            {
                double progressPerSecond = task.GetProgressPerSecond();
                double progressNeeded = task.GetProgressGoal();
                if (progressPerSecond > 0)
                {
                    double amountPerSecond = 0;
                    foreach (ItemReward ir in task.GetTask().GetItemRewards())
                    {
                        if (ir.GetItem().GetId() == item.GetId())
                        {
                            amountPerSecond += (ir.GetMinAmount() + ir.GetExtraRange() / 2f) * ir.GetRewardChance() * (progressPerSecond / progressNeeded);
                            break;
                        }
                    }
                    foreach (Item ic in task.GetItemFinalCost())
                    {
                        if (ic.GetId() == item.GetId())
                        {
                            amountPerSecond -= ic.GetAmount() * (progressPerSecond / progressNeeded);
                            break;
                        }
                    }
                    itemAmountPerSecond += amountPerSecond;
                }
            }
        }
        text.text = (itemAmountPerSecond < 0 ? "-" : "+") + Math.Abs(itemAmountPerSecond).ToString("F2")+"/s";
        text.color = itemAmountPerSecond < 0 ? Utils.GetWrongColor() : Utils.GetSuccessColor();
    }
}
