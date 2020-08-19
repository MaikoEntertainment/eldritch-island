using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMining : Skill
{
    public SkillMining(int initialLevel = 0, double exp = 0) : base(initialLevel, exp) 
    {
        id = SkillIds.Mining;
    }

    public override Skill Copy()
    {
        return new SkillMining(level, exp);
    }
}
