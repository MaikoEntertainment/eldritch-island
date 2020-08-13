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
        skillText.text = skillName + " " + s.GetLevel();
    }
}
