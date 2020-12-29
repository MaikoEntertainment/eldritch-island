using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITaskMonsterPickQuick : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI progressPerSecond;
    public Transform equipmentList;
    public TextMeshProUGUI stress;
    public Button addButton;
    public Button removeButton;

    public UITaskMonsterTool monsterPickToolPrefab;
    public UITaskMonsterClothes monsterPickClothesPrefab;

    public delegate void OnToogle(Monster m, Task t);
    public event OnToogle onToogle;

    private Monster monster;
    private Task task;

    public UITaskMonsterPickQuick Load(Monster m, Task t)
    {
        monster = m;
        task = t;
        float stress = m.GetStress();
        icon.sprite = m.GetIcon();
        this.stress.text = stress.ToString("F1");
        if (m.isOverStressed())
            this.stress.color = Utils.GetWrongColor();
        double powerSign = 0;
        foreach (SkillBonus ts in t.GetTask().GetSkillsRequired())
        {
            Dictionary<SkillIds, Skill> monsterSkills = m.GetFinalSkills(t);
            foreach(Skill ms in monsterSkills.Values.ToList())
            {
                if (ts.GetSkillId() == ms.GetId())
                {
                    powerSign += ms.GetBonusLevel();
                    break;
                }
            }
        }
        progressPerSecond.text = t.CalculateMonsterProgressPerSecond(m).ToString("F2") + "/s";
        if (powerSign > 0)
            progressPerSecond.color = Utils.GetSuccessColor();
        else if(powerSign < 0)
            progressPerSecond.color = Utils.GetWrongColor();

        foreach (Tool tool in m.GetTools())
        {
            Instantiate(monsterPickToolPrefab.gameObject, equipmentList).GetComponent<UITaskMonsterTool>().Load(tool);
        }
        foreach (Clothes clothes in m.GetClothes())
        {
            Instantiate(monsterPickClothesPrefab.gameObject, equipmentList).GetComponent<UITaskMonsterClothes>().Load(clothes);
        }
        addButton.gameObject.SetActive(!task.GetMonsters().Contains(monster));
        removeButton.gameObject.SetActive(task.GetMonsters().Contains(monster));
        return this;
    }

    public void Toogle()
    {
        bool inTask = task.GetMonsters().Contains(monster);
        if (inTask)
        {
            monster.AddStress(task.GetLeaveTaskPenalty());
            task.RemoveMonster(monster);
        }
        else
        {
            if (task.CanAddMonsters())
            {
                List<Item> costs = monster.GetTaskItemCostForThisMonster(task, task.GetTask().GetCostPerMonster());
                bool canPay = InventoryMaster.GetInstance().CanPayItems(costs);
                if (canPay)
                {
                    InventoryMaster.GetInstance().PayItems(costs);
                    task.AddMonsters(monster);
                }
            }
        }
        UITasklessMonsterMaster.GetInstance().UpdateTasklessMonsters();
        onToogle?.Invoke(monster, task);
    }
}
