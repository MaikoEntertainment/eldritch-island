using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager
{
    protected Dictionary<SkillIds, Skill> skills;

    public SkillsManager(Dictionary<SkillIds, Skill> initialSkills)
    {
        InitializeSkills(initialSkills);
    }

    protected void InitializeSkills(Dictionary<SkillIds, Skill> initialSkills)
    {
        skills = initialSkills;
        if (!initialSkills.ContainsKey(SkillIds.Nature))
            skills.Add(SkillIds.Nature, new Skill());
        if (!initialSkills.ContainsKey(SkillIds.Woodcutting))
            skills.Add(SkillIds.Woodcutting, new SkillWoodcutting());
        if (!initialSkills.ContainsKey(SkillIds.Mining))
            skills.Add(SkillIds.Mining, new SkillMining());
        if (!initialSkills.ContainsKey(SkillIds.Crafting))
            skills.Add(SkillIds.Crafting, new SkillCrafting());
        if (!initialSkills.ContainsKey(SkillIds.War))
            skills.Add(SkillIds.War, new SkillWar());
        if (!initialSkills.ContainsKey(SkillIds.Witchcraft))
            skills.Add(SkillIds.Witchcraft, new SkillWitchcraft());
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
