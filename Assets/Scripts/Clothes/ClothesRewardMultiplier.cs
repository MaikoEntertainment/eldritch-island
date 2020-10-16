using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Clothes/Reward Multiplier Chance")]
public class ClothesRewardMultiplier : ClothesBase
{
    public float multiplier = 1.25f;
    public float multiplierChangePerLevel = 0.05f;

    public override List<ItemReward> GetTaskItemRewards(Task task, Monster monster, List<ItemReward> currentRewards, int tier)
    {
        List<ItemReward> rewards = new List<ItemReward>();
        foreach(ItemReward ir in currentRewards)
        {
            int mult = (int)Math.Round((ir.GetMinAmount() + ir.GetExtraRange()/2f) * (GetMultiplier(tier) - 1));
            rewards.Add(
                new ItemReward(
                    ir.GetItem(),
                    ir.GetMinAmount() + mult,
                    ir.GetExtraRange(),
                    ir.GetRewardChance()
                    )
                );
        }
        return rewards;
    }

    protected float GetMultiplier(int tier)
    {
        return multiplier + multiplierChangePerLevel * tier;
    }

    public override string GetDescription(int tier = 0)
    {
        return base.GetDescription(tier) + (100 * (GetMultiplier(tier) - 1)).ToString("F1")+"%";
    }
}
