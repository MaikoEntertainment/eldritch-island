using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Tool
{
    [SerializeField]
    protected ToolBase toolBase;
    [SerializeField]
    protected double durabilityUsed;
    protected int tier;

    public Tool(ToolBase toolBase, int tier = 0)
    {
        this.toolBase = toolBase;
        this.tier = tier;
    }

    public int GetId() { return toolBase.GetId(); }
    public double GetDurability() { return toolBase.GetDurability(GetTier()); }
    public Tag[] GetTags() { return toolBase.GetTags(); }
    public List<SkillBonus> GetSkillBonuses() { return toolBase.GetSkillBonuses(GetTier()); }
    public Sprite GetIcon() { return toolBase.GetIcon(); }
    public bool Use(double durabilityUsed=1)
    {
        toolBase.Use();
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

    public ToolBase GetToolBase() { return toolBase; }
}
