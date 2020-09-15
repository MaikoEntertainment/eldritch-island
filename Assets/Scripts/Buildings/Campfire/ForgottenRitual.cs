using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "Tasks/Campfire/ForgottenRitual")]
public class ForgottenRitual : TaskBaseBuildingLevelRequirement
{
    public override void OnComplete()
    {
        foreach (Monster m in MonsterMaster.GetInstance().GetActiveMonsters().Values.ToList())
        {
            m.AddStress(-5);
        }
        UITasklessMonsterMaster.GetInstance().UpdateTasklessMonsters();
    }
}
