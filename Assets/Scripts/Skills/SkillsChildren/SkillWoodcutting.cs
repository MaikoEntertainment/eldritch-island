using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWoodcutting : Skill
{
    public SkillWoodcutting(int initialLevel = 0, double exp=0) : base(initialLevel, exp) 
    {
        id = SkillIds.Woodcutting;
    }
}
