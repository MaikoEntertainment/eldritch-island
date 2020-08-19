using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITaskMonsterSkill : MonoBehaviour
{
    public TextMeshProUGUI skillText;

    public void Load(Skill s)
    {
        string skillName = LanguageMaster.GetInstance().GetSkillName(s.GetId());
        double exp = s.GetExp();
        double toLevel = s.GetExpToLevelUp();
        int originalLevel = s.GetLevel();
        int bonus = s.GetLevelWithBonuses();
        skillText.text = skillName + " " + bonus + " ("+(exp/toLevel*100).ToString("F1")+"%)";
        if (originalLevel < bonus)
            skillText.color = Utils.GetSuccessColor();
    }
}
