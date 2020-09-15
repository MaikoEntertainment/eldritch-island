using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterWendigo : Monster
{
    public override Dictionary<SkillIds, Skill> GetFinalSkills(Task task)
    {
        Dictionary<SkillIds, Skill> finalSkills = base.GetFinalSkills(task);
        foreach(Skill skill in finalSkills.Values.ToList())
        {
            int lvl = skill.GetLevelWithBonuses();
            skill.AddBonusLevel(Mathf.CeilToInt(lvl * 0.5f));
        }
        return finalSkills;
    }

    public override List<Item> GetTaskItemCostForThisMonster(Task task, List<Item> currentCosts)
    {
        for (int i = 0; i < currentCosts.Count; i++)
        {
            Item itemCost = currentCosts[i];
            if (itemCost.GetId() == 0)
            {
                currentCosts[i] = new Item(itemCost.GetItemBase(), (int)(itemCost.GetAmount() * 1.5f));
                break;
            }
        }
        return base.GetTaskItemCostForThisMonster(task, currentCosts);
    }
}
