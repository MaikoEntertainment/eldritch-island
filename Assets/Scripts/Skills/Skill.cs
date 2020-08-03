using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    protected int level = 0;
    protected double exp = 0;

    public Skill(int initialLevel=0)
    {
        level = initialLevel;
    }

    public virtual SkillIds GetId()
    {
        return SkillIds.Nature;
    }
    public int GetLevel()
    {
        return level;
    }

   public double GetExp()
    {
        return exp;
    }

    public virtual double GetExpToLevelUp()
    {
        double baseValue = 5;
        return Math.Pow(baseValue, 2 * level);
    }

    public bool AddExp(double expGained)
    {
        exp += expGained;
        if (exp > GetExpToLevelUp())
        {
            LevelUp();
            return true;
        }
        return false;
    }

    public int LevelUp()
    {
        level += 1;
        return level;
    }
}
