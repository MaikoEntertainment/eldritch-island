using System;
[Serializable]
public class SkillBonus
{
    public SkillIds skillId;
    public int levelModifier = 0;

    public SkillBonus(SkillIds id, int levelMod)
    {
        skillId = id;
        levelModifier = levelMod;
    }

    public SkillBonus Copy() { return new SkillBonus(skillId, levelModifier); }

    public int GetLevelModifier() { return levelModifier; }

    public SkillIds GetSkillId() { return skillId; }
}
