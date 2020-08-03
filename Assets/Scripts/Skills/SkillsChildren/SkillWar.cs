using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWar : Skill
{
    public SkillWar(int initialLevel = 0, double exp = 0) : base(initialLevel, exp)
    {
        id = SkillIds.War;
    }
}
