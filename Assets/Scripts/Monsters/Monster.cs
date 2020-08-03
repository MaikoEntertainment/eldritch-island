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
    protected SkillsManager skills;
    [SerializeField]
    protected ToolsManager tools;
    [SerializeField]
    protected ClothesManager clothes;

    public MonsterIds GetId() { return id; }

    public Sprite GetIcon()
    {
        return icon;
    }

    public Dictionary<SkillIds, Skill> GetSkills()
    {
        return skills.GetSkills();
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
    public virtual bool AddStress(float stressChange)
    {
        stress += stressChange;
        return (stress > GetStressMax());
    }
    public virtual bool CanWork(float stressGain) 
    {
        Console.WriteLine("Add Task to parameters");
        float resultingStress = stressGain + stress;
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
    public bool Work(float stress, double exp)
    {
        Console.WriteLine("Add Task to parameters and make them add exp to required skills");
        tools.UseTools();
        clothes.UseClothes();
        return AddStress(stress);
    }
    public string GetSpecies() { return species.GetText(); }
    public string GetDescription() { return description.GetText(); }
    public string GetAbility() { return ability.GetText(); }
}
