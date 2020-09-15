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

    public Tool (ToolBase toolBase, double durabilityUsed, int tier)
    {
        this.toolBase = toolBase;
        this.durabilityUsed = durabilityUsed;
        this.tier = tier;
    }

    public int GetId() { return toolBase.GetId(); }
    public double GetDurability() { return toolBase.GetDurability(GetTier()); }
    public string GetDescription() { return toolBase.GetDescription(GetTier()); }
    public Tag[] GetTags() { return toolBase.GetTags(); }
    public List<SkillBonus> GetSkillBonuses() { return toolBase.GetSkillBonuses(GetTier()); }
    public Sprite GetIcon() { return toolBase.GetIcon(); }
    public bool Use(Monster m, Task t)
    {
        toolBase.Use(m,t, GetTier());
        durabilityUsed += 1;
        double durability = GetDurability();
        return durabilityUsed >= durability;
    }
    public int GetTier() { return tier; }
    public double GetDurabilityLeft()
    {
        double durability = GetDurability();
        return Math.Max(durability - durabilityUsed, 0);
    }

    public ToolBase GetToolBase() { return toolBase; }
}
