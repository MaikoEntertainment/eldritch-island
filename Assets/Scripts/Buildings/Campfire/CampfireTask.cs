using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "Tasks/Campfire/Campfire Ritual")]
public class CampfireTask : TaskBaseBuildingLevelRequirement
{
    public override void OnComplete()
    {
        foreach(Monster m in MonsterMaster.GetInstance().GetTasklessMonsters().Values.ToList())
        {
            m.AddStress(-20);
        }
        UITasklessMonsterMaster.GetInstance().UpdateTasklessMonsters();
    }
}
