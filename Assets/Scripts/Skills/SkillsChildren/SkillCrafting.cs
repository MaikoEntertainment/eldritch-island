using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCrafting : Skill
{
    public SkillCrafting(int initialLevel = 0, double exp = 0) : base(initialLevel, exp)
    {
        id = SkillIds.Crafting;
    }

    public override Skill Copy()
    {
        return new SkillCrafting(level, exp);
    }
}
