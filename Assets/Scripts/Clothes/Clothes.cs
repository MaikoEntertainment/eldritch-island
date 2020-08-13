using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Clothes
{
    protected ClothesBase clothesBase;
    protected double durabilityUsed;
    protected int tier;

    public int GetId() { return clothesBase.GetId(); }
    public double GetDurability() { return clothesBase.GetDurability(); }
    public List<SkillBonus> GetSkillBonuses() { return clothesBase.GetSkillBonuses(); }
    public Tag[] GetTags() { return clothesBase.GetTags(); }
    public Sprite GetIcon() { return clothesBase.GetIcon(); }

    public bool Use(double durabilityUsed = 1)
    {
        clothesBase.Use();
        durabilityUsed += durabilityUsed;
        double durability = clothesBase.GetDurability();
        return durabilityUsed >= durability;
    }
    public int GetTier() { return tier; }
    public double GetDurabilityLeft()
    {
        double durability = clothesBase.GetDurability();
        return Math.Max(durability - durabilityUsed, 0);
    }
    public ClothesBase GetClothes() { return clothesBase; }
}
