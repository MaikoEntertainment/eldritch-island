using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[Serializable]
public class ItemReward
{
    [SerializeField]
    protected ItemBase item;
    [SerializeField]
    protected int minAmount = 0;
    [SerializeField]
    protected int extraRange = 0;
    [SerializeField]
    protected float rewardChance = 1;

    public ItemReward(ItemBase item, int minAmount, int extraRange, float rewardChance)
    {
        this.item = item;
        this.minAmount = minAmount;
        this.extraRange = extraRange;
        this.rewardChance = rewardChance;
    }

    public ItemReward Copy()
    {
        return new ItemReward(item, minAmount, extraRange, rewardChance);
    }

    public ItemBase GetItem() { return item; }
    public int GetMinAmount() { return minAmount; }
    public int GetExtraRange() { return extraRange; }
    public float GetRewardChance() { return rewardChance; }

    public Item ObtainReward()
    {
        float chance = UnityEngine.Random.value;
        if (chance > rewardChance)
            return new Item(item, 0);
        int amount = UnityEngine.Random.Range(minAmount, minAmount + extraRange + 1);
        Item i = new Item(item, amount);
        return i;
    }
}
