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
    // Use only when getting the Skill with bonus and never for the actual skill
    protected int bonusLevel = 0;
    public Skill(int initialLevel = 0, double exp=0)
    {
        level = initialLevel;
        this.exp = exp;

    }

    public virtual Skill Copy()
    {
        return new Skill(level, exp);
    }
    public void Load(SaveSkill ss)
    {
        level = ss.GetLevel();
        exp = ss.GetExp();
    }

    public virtual SkillIds GetId()
    {
        return id;
    }
    public int GetLevel()
    {
        return level;
    }
    public int GetBonusLevel()
    {
        return bonusLevel;
    }
    public int GetLevelWithBonuses()
    {
        return level + bonusLevel;
    }
    public int AddBonusLevel(int bonus)
    {
        bonusLevel += bonus;
        return GetLevelWithBonuses();
    }

   public double GetExp()
    {
        return exp;
    }

    public virtual double GetExpToLevelUp()
    {
        double baseValue = 5;
        return Math.Pow(baseValue, 1 + level * 0.5f);
    }

    public bool AddExp(double expGained)
    {
        exp += expGained;
        if (exp >= GetExpToLevelUp())
        {
            double extra = exp - GetExpToLevelUp();
            LevelUp();
            if (extra > 0)
                AddExp(extra);
            return true;
        }
        return false;
    }

    public void AddLevel(int add)
    {
        level += add;
    }

    public int LevelUp()
    {
        level += 1;
        exp = 0;
        return level;
    }
}
