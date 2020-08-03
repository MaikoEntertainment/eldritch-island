using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LanguageSkill
{
    [SerializeField]
    protected SkillIds id;
    [SerializeField]
    protected TextLanguageOwn name;

    public SkillIds GetSkillId() { return id; }

    public string GetText() { return name.GetText(); }
}
