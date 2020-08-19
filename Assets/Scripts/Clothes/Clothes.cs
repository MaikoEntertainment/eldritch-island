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
    public double GetDurability() { return clothesBase.GetDurability(GetTier()); }
    public List<SkillBonus> GetSkillBonuses() { return clothesBase.GetSkillBonuses(GetTier()); }
    public Tag[] GetTags() { return clothesBase.GetTags(); }
    public Sprite GetIcon() { return clothesBase.GetIcon(); }

    public Clothes(ClothesBase clothesBase, int tier = 0)
    {
        this.clothesBase = clothesBase;
        this.tier = tier;
    }
    public bool Use(double durabilityUsed = 1)
    {
        clothesBase.Use();
        this.durabilityUsed += durabilityUsed;
        double durability = GetDurability();
        return this.durabilityUsed >= durability;
    }
    public int GetTier() { return tier; }
    public double GetDurabilityLeft()
    {
        double durability = GetDurability();
        return Math.Max(durability - durabilityUsed, 0);
    }
    public ClothesBase GetClothes() { return clothesBase; }
}
