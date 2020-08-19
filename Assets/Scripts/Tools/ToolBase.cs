﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class ToolBase: ScriptableObject
{
    [SerializeField]
    protected int id;
    [SerializeField]
    protected Sprite icon;
    [SerializeField]
    protected long durability;
    [SerializeField]
    protected TextLanguageOwn myName;
    [SerializeField]
    protected TextLanguageOwn description;
    [SerializeField]
    protected List<SkillBonus> skillBonuses;
    [SerializeField]
    protected Tag[] tags;

    public int GetId() { return id; }
    public double GetDurability(int tier=0) { return durability * (1 + tier); }
    public Tag[] GetTags() { return tags; }
    public List<SkillBonus> GetSkillBonuses(int tier = 0) {
        if (tier==0)
            return skillBonuses;
        List<SkillBonus> bonusesTier = new List<SkillBonus>();
        foreach(SkillBonus sb in skillBonuses)
        {
            int newMod = (int)(sb.GetLevelModifier() * (1 + 0.25f * tier));
            bonusesTier.Add(new SkillBonus(sb.GetSkillId(), newMod));
        }
        return bonusesTier;
    }
    public Sprite GetIcon() { return icon; }
    public void Use()
    {
        
    }

    public string GetName() { return myName.GetText(); }
    public string GetDescription() { return description.GetText(); }

    public virtual List<Item> GetTaskItemCostForThisMonster(Task task, Monster monster, List<Item> currentCosts)
    {
        // Change for Tools with specific abilities
        return currentCosts;
    }
    public virtual List<ItemReward> GetTaskItemRewards(Task task, Monster monster, List<ItemReward> currentRewards)
    {
        // Change for Tools with specific abilities
        return currentRewards;
    }
    public virtual List<Tool> GetTaskToolRewards(Task task, Monster monster, List<Tool> currentRewards)
    {
        // Change for Tools with specific abilities
        return currentRewards;
    }
    public virtual List<Clothes> GetTaskClothesRewards(Task task, Monster monster, List<Clothes> currentRewards)
    {
        // Change for Tools with specific abilities
        return currentRewards;
    }
}
