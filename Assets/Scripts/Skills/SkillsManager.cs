using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SkillsManager
{
    [SerializeField]
    protected List<Skill> initialSkills = new List<Skill>();
    protected Dictionary<SkillIds, Skill> skills = new Dictionary<SkillIds, Skill>();

    public void InitializeSkills()
    {
        // Adds missing skills
        if (!skills.ContainsKey(SkillIds.Nature))
            AddMissingSkill(SkillIds.Nature);
        if (!skills.ContainsKey(SkillIds.Woodcutting))
            AddMissingSkill(SkillIds.Woodcutting);
        if (!skills.ContainsKey(SkillIds.Mining))
            AddMissingSkill(SkillIds.Mining);
        if (!skills.ContainsKey(SkillIds.Crafting))
            AddMissingSkill(SkillIds.Crafting);
        if (!skills.ContainsKey(SkillIds.War))
            AddMissingSkill(SkillIds.War);
        if (!skills.ContainsKey(SkillIds.Witchcraft))
            AddMissingSkill(SkillIds.Witchcraft);
    }

    public void Load(List<SaveSkill> savedSkills)
    {
        foreach (SaveSkill ss in savedSkills)
        {
            if (skills.ContainsKey(ss.GetId()))
            {
                skills[ss.GetId()].Load(ss);
            }
            else
            {
                AddSkill(ss.GetId(), ss.GetLevel(), ss.GetExp());
            }
        }
    }

    protected void AddMissingSkill(SkillIds id)
    {
        Skill initial = GetIntialSkill(id);
        int level = initial!=null ? initial.GetLevel() : 0;
        double exp = initial != null ? initial.GetExp() : 0;
        AddSkill(id, level, exp);
    }

    public void AddSkill(SkillIds id, int level, double exp)
    {
        switch (id)
        {
            case SkillIds.Woodcutting:
                skills.Add(id, new SkillWoodcutting(level, exp));
                break;
            case SkillIds.Mining:
                skills.Add(id, new SkillMining(level, exp));
                break;
            case SkillIds.Crafting:
                skills.Add(id, new SkillCrafting(level, exp));
                break;
            case SkillIds.War:
                skills.Add(id, new SkillWar(level, exp));
                break;
            case SkillIds.Witchcraft:
                skills.Add(id, new SkillWitchcraft(level, exp));
                break;
            default:
                skills.Add(id, new Skill(level, exp));
                break;
        }
    }

    public Skill GetIntialSkill(SkillIds id)
    {
        foreach (Skill initial in initialSkills)
        {
            if (initial.GetId() == id)
                return initial;
        }
        return null;
    }
    public List<Skill> GetIntialSkills()
    {
        return initialSkills;
    }

    public Skill GetSkill(SkillIds id)
    {
        if (skills.ContainsKey(id))
            return skills[id];
        else
            return null;
    }

    public bool AddExp(SkillIds id, double exp)
    {
        Skill skill = GetSkill(id);
        if (skill != null)
            return skill.AddExp(exp);
        return false;
    }

    public Dictionary<SkillIds, Skill> GetSkills()
    {
        return skills;
    }
}
