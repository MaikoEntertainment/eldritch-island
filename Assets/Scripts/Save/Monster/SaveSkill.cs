using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SaveSkill
{
    public SkillIds id;
    public int level = 0;
    public double exp = 0;

    public SaveSkill(Skill s)
    {
        id = s.GetId();
        level = s.GetLevel();
        exp = s.GetExp();
    }

    public SkillIds GetId() { return id; }
    public int GetLevel() { return level; }
    public double GetExp() { return exp; }
}
