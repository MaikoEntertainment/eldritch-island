using System;
[Serializable]
public class SkillBonus
{
    public SkillIds skillId;
    public int levelModifier = 0;

    public int GetLevelModifier() { return levelModifier; }

    public SkillIds GetSkillId() { return skillId; }
}
