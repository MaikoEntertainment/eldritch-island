using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWitchcraft : Skill
{
    public SkillWitchcraft(int initialLevel = 0, double exp = 0) : base(initialLevel, exp)
    {
        id = SkillIds.Witchcraft;
    }

    public override Skill Copy()
    {
        return new SkillWitchcraft(level, exp);
    }
}
