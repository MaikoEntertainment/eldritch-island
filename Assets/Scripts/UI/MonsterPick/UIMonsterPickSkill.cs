using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMonsterPickSkill : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    public void Load(Skill skill)
    {
        string skillName = LanguageMaster.GetInstance().GetSkillName(skill.GetId());
        string skillValue = skill.GetLevel() > 0 ? "+" : "-" + skill.GetLevel();
        text.text = skillName + skillValue;
    }
}
