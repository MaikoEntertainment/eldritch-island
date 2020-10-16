using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSnake : Monster
{
    public override bool AddSkillExp(SkillIds id, double exp)
    {
        return base.AddSkillExp(id, exp * 1.5d);
    }
}
