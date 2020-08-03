using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    protected ToolBase toolBase;
    protected double durabilityUsed;
    protected int tier;

    public int GetId() { return toolBase.GetId(); }
    public double GetDurability() { return toolBase.GetDurability(); }
    public Tag[] GetTags() { return toolBase.GetTags(); }
    public List<SkillBonus> GetSkillBonuses() { return toolBase.GetSkillBonuses(); }
    public Sprite GetIcon() { return toolBase.GetIcon(); }
    public bool Use(double durabilityUsed=1)
    {
        toolBase.Use();
        durabilityUsed += durabilityUsed;
        double durability = toolBase.GetDurability();
        return durabilityUsed >= durability;
    }
    public int GetTier() { return tier; }
    public double GetDurabilityLeft()
    {
        double durability = toolBase.GetDurability();
        return Math.Max(durability - durabilityUsed, 0);
    }
}
