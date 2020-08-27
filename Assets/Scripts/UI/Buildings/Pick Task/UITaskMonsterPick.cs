﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITaskMonsterPick : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI monsterName;
    public Transform skillList;
    public Transform equipmentList;
    public TextMeshProUGUI stressBefore;
    public TextMeshProUGUI stressAfter;

    public UITaskMonsterSkill monsterPickSkillPrefab;
    public UITaskMonsterTool monsterPickToolPrefab;

    public delegate void OnClick(Monster m);
    public event OnClick onClick;

    private Monster monster;

    public void Load(Monster m, Task t)
    {
        monster = m;
        float stress = m.GetStress();
        float stressAfter = m.GetStressAfterTask(t);
        float stressMax = m.GetStressMax();
        icon.sprite = m.GetIcon();
        stressBefore.text = stress.ToString("F1");
        this.stressAfter.text = stressAfter.ToString("F1");
        if (stressMax < stressAfter)
            this.stressAfter.color = Utils.GetWrongColor();
        monsterName.text = m.GetSpecies();

        foreach (SkillBonus ts in t.GetTask().GetSkillsRequired())
        {
            Dictionary<SkillIds, Skill> monsterSkills = m.GetFinalSkills();
            Instantiate(monsterPickSkillPrefab.gameObject, skillList).GetComponent<UITaskMonsterSkill>().Load(monsterSkills[ts.GetSkillId()]);
        }
        foreach (Tool tool in m.GetTools())
        {
            Instantiate(monsterPickToolPrefab.gameObject, equipmentList).GetComponent<UITaskMonsterTool>().Load(tool);
        }
    }

    public void HandleOnClick()
    {
        onClick?.Invoke(monster);
    }

}
