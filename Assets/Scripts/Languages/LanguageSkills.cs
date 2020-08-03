using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LanguageSkills
{
    [SerializeField]
    protected List<LanguageSkill> skillsText;

    public string GetSkillName(SkillIds id)
    {
        foreach(LanguageSkill ls in skillsText)
        {
            if (ls.GetSkillId() == id)
                return ls.GetText();
        }
        return "???";
    }
}
