using System;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public static float maxStress = 100;

    [SerializeField]
    protected MonsterIds id;
    [SerializeField]
    protected Sprite icon;
    [SerializeField]
    protected TextLanguageOwn species;
    [SerializeField]
    protected TextLanguageOwn description;
    [SerializeField]
    protected TextLanguageOwn ability;

    protected float stress;

    [SerializeField]
    protected SkillsManager skills = new SkillsManager();
    [SerializeField]
    protected ToolsManager tools = new ToolsManager();
    [SerializeField]
    protected ClothesManager clothes = new ClothesManager();

    private void Awake()
    {
        skills.InitializeSkills();
    }

    public MonsterIds GetId() { return id; }

    public Sprite GetIcon()
    {
        return icon;
    }

    public Dictionary<SkillIds, Skill> GetSkills()
    {
        return skills.GetSkills();
    }
    public List<Skill> GetInitialSkills()
    {
        return skills.GetIntialSkills();
    }
    public virtual Dictionary<SkillIds, Skill> GetUsableSkills()
    {
        return skills.GetSkills();
    }
    public virtual bool AddSkillExp(SkillIds id, double exp)
    {
        return skills.AddExp(id, exp);
    }
    public virtual int GetSkillLevel(SkillIds id)
    {
        return skills.GetSkill(id).GetLevel();
    }
    public virtual float GetStress() { return stress; }
    public virtual float GetStressMax() { return maxStress; }

    public virtual float GetStressAfterTask(Task t)
    {
        return Mathf.Max(stress + t.GetTask().GetStressChange(),0);
    }
    public virtual bool AddStress(float stressChange)
    {
        stress += stressChange;
        return (stress > GetStressMax());
    }
    public virtual bool CanWork(Task task) 
    {
        float resultingStress = GetStressAfterTask(task);
        return GetStressMax() > resultingStress;
    }
    public List<Tool> GetTools()
    {
        return tools.GetEquippedTools();
    }
    public int GetToolSlots()
    {
        return tools.GetToolSlots();
    }
    public List<Clothes> GetClothes()
    {
        return clothes.GetEquippedClothes();
    }
    public int GetClothesSlots()
    {
        return clothes.GetClothesSlots();
    }
    public string GetSpecies() { return species.GetText(); }
    public string GetDescription() { return description.GetText(); }
    public string GetAbility() { return ability.GetText(); }

    // Taks specific
    public bool FinishWork(Task t)
    {
        float stressChange = GetStressAfterTask(t);
        tools.UseTools();
        clothes.UseClothes();
        return AddStress(stressChange);
    }
    public virtual List<Item> GetTaskItemCostForThisMonster(Task task, List<Item> currentCosts)
    {
        // Change for Monster with specific abilities
        List<Item> finalCost = currentCosts;
        foreach (Clothes t in GetClothes())
        {
            finalCost = t.GetClothes().GetTaskItemCostForThisMonster(task, this, finalCost);
        }
        foreach (Tool t in GetTools())
        {
            finalCost = t.GetToolBase().GetTaskItemCostForThisMonster(task, this, finalCost);
        }
        return finalCost;
    }

    public virtual List<ItemReward> GetTaskItemRewards(Task task, List<ItemReward> currentRewards)
    {
        // Change for Monster with specific abilities
        List<ItemReward> finalCost = currentRewards;
        foreach (Clothes t in GetClothes())
        {
            finalCost = t.GetClothes().GetTaskItemRewards(task, this, finalCost);
        }
        foreach (Tool t in GetTools())
        {
            finalCost = t.GetToolBase().GetTaskItemRewards(task, this, finalCost);
        }
        return finalCost;
    }
    public virtual List<Tool> GetTaskToolRewards(Task task, List<Tool> currentRewards)
    {
        // Change for Monster with specific abilities
        List<Tool> finalCost = currentRewards;
        foreach (Clothes t in GetClothes())
        {
            finalCost = t.GetClothes().GetTaskToolRewards(task, this, finalCost);
        }
        foreach (Tool t in GetTools())
        {
            finalCost = t.GetToolBase().GetTaskToolRewards(task, this, finalCost);
        }
        return finalCost;
    }
    public virtual List<Clothes> GetTaskClothesRewards(Task task, List<Clothes> currentRewards)
    {
        // Change for Monster with specific abilities
        List<Clothes> finalCost = currentRewards;
        foreach (Clothes t in GetClothes())
        {
            finalCost = t.GetClothes().GetTaskClothesRewards(task, this, finalCost);
        }
        foreach (Tool t in GetTools())
        {
            finalCost = t.GetToolBase().GetTaskClothesRewards(task, this, finalCost);
        }
        return finalCost;
    }
}
