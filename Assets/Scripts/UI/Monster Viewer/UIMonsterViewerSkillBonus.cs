using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMonsterViewerSkillBonus : MonoBehaviour
{
    public TextMeshProUGUI skillText;

    public void Load(SkillBonus s)
    {
        string skillName = LanguageMaster.GetInstance().GetSkillName(s.GetSkillId());
        int mod = s.GetLevelModifier();
        skillText.text = skillName + (mod > 0 ? " +" : " " ) + mod;
        skillText.color = mod > 0 ? Utils.GetSuccessColor() : Utils.GetWrongColor();
    }
}
