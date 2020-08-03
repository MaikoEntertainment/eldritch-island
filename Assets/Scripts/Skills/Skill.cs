using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Skill
{
    [SerializeField]
    protected SkillIds id = SkillIds.Nature;
    [SerializeField]
    protected int level = 0;
    [SerializeField]
    protected double exp = 0;

    public Skill(int initialLevel = 0, double exp=0)
    {
        level = initialLevel;
        this.exp = exp;

    }

    public virtual SkillIds GetId()
    {
        return id;
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
