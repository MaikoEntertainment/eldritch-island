using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SkillsManager
{
    [SerializeField]
    protected List<Skill> initialSkills;
    protected Dictionary<SkillIds, Skill> skills;

    public void InitializeSkills()
    {
        Dictionary<SkillIds, Skill> savedSkills = new Dictionary<SkillIds, Skill>();
        skills = savedSkills;
        // Adds missing skills
        if (!savedSkills.ContainsKey(SkillIds.Nature))
            AddMissingSkill(SkillIds.Nature);
        if (!savedSkills.ContainsKey(SkillIds.Woodcutting))
            AddMissingSkill(SkillIds.Woodcutting);
        if (!savedSkills.ContainsKey(SkillIds.Mining))
            AddMissingSkill(SkillIds.Mining);
        if (!savedSkills.ContainsKey(SkillIds.Crafting))
            AddMissingSkill(SkillIds.Crafting);
        if (!savedSkills.ContainsKey(SkillIds.War))
            AddMissingSkill(SkillIds.War);
        if (!savedSkills.ContainsKey(SkillIds.Witchcraft))
            AddMissingSkill(SkillIds.Witchcraft);
    }

    protected void AddMissingSkill(SkillIds id)
    {
        Skill initial = GetIntialSkill(id);
        int level = initial!=null ? initial.GetLevel() : 0;
        double exp = initial != null ? initial.GetExp() : 0;
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
