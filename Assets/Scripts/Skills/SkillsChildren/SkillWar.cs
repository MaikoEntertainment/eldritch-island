using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWar : Skill
{
    public SkillWar(int initialLevel = 0, double exp = 0) : base(initialLevel, exp)
    {
        id = SkillIds.War;
    }

    public override Skill Copy()
    {
        return new SkillWar(level, exp);
    }
}
