using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public static float maxStress = 100;
    public static float stressTreshold = 100;

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

    public delegate void StressChange(Monster m);
    public event StressChange onStressChange;

    private void Awake()
    {
        skills.InitializeSkills();
    }

    public void Load(SaveMonster sm)
    {
        stress = sm.GetStress();
        skills.Load(sm.GetSaveSkills());
        tools.Load(sm.GetSaveTools());
        clothes.Load(sm.GetSaveClothes());
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
    public Dictionary<SkillIds, Skill> GetFinalSkills()
    {
        Dictionary<SkillIds, Skill> skills = GetSkills();
        Dictionary<SkillIds, Skill> finalSkills = new Dictionary<SkillIds, Skill>();
        foreach (Skill sk in skills.Values.ToList())
        {
            finalSkills.Add(sk.GetId(), sk.Copy());
        }
        foreach (Tool tl in tools.GetEquippedTools())
        {
            foreach(SkillBonus sb in tl.GetSkillBonuses())
            {
                finalSkills[sb.GetSkillId()].AddBonusLevel(sb.GetLevelModifier());
            }
        }
        foreach (Clothes cl in clothes.GetEquippedClothes())
        {
            foreach (SkillBonus sb in cl.GetSkillBonuses())
            {
                finalSkills[sb.GetSkillId()].AddBonusLevel(sb.GetLevelModifier());
            }
        }
        if (isOverStressed())
        {
            foreach (Skill fs in finalSkills.Values.ToList())
            {
                finalSkills[fs.GetId()].AddBonusLevel((int)(-0.5f * fs.GetLevelWithBonuses()));
            }
        }
        return finalSkills;
    }
    public virtual Dictionary<SkillIds, Skill> GetFinalSkills(Task task)
    {
        return GetFinalSkills();
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
        bool leveledUp = skills.AddExp(id, exp);
        if (leveledUp)
            NotificationMaster.GetInstance().SendLevelUpNotification(GetIcon(), LanguageMaster.GetInstance().GetSkillName(id), GetSkillLevel(id));
        return leveledUp;
    }
    public virtual int GetSkillLevel(SkillIds id)
    {
        return skills.GetSkill(id).GetLevel();
    }
    public virtual float GetStress() { return stress; }
    public virtual float GetSaneStressMax() { return maxStress; }
    public virtual float GetStressTreshold() { return stressTreshold; }
    public virtual float GetStressMax() { return maxStress + stressTreshold; }
    public virtual bool isOverStressed() 
    { 
        return GetStress() > GetStressMax() - GetStressTreshold(); 
    }

    public virtual float GetStressAfterTask(Task t)
    {
        return Mathf.Max(stress + t.GetStressChange(),0);
    }
    public virtual void AddTaskStress(Task t)
    {
        stress = GetStressAfterTask(t);
        onStressChange?.Invoke(this);
    }
    public virtual bool AddStress(float stressChange)
    {
        stress =  Math.Max(stress + stressChange,0);
        onStressChange?.Invoke(this);
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
    public bool EquipTool(Tool t)
    {
        return tools.EquipTool(t);
    }
    public bool UnEquipTool(int index)
    {
        return tools.UnEquipTool(index);
    }
    public int GetToolSlots()
    {
        return tools.GetToolSlots();
    }
    public List<Clothes> GetClothes()
    {
        return clothes.GetEquippedClothes();
    }
    public bool UnEquipClothes(int index)
    {
        return clothes.UnEquipTool(index);
    }
    public bool EquipClothes(Clothes c)
    {
        return clothes.EquipClothes(c);
    }
    public int GetClothesSlots()
    {
        return clothes.GetClothesSlots();
    }
    public string GetSpecies() { return species.GetText(); }
    public string GetDescription() { return description.GetText(); }
    public string GetAbility() { return ability.GetText(); }

    // Taks specific
    public virtual void FinishWork(Task t)
    {
        tools.UseTools(this, t);
        clothes.UseClothes(this, t);
        AddTaskStress(t);
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
            finalCost = t.GetTaskItemRewards(task, this, finalCost);
        }
        foreach (Tool t in GetTools())
        {
            finalCost = t.GetTaskItemRewards(task, this, finalCost);
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
