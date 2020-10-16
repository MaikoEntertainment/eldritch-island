using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "Tasks/Campfire/Spores Ritual")]
public class CampfireSporesRitual : TaskBaseBuildingLevelRequirement
{
    public float specialStressChange = -5;
    public override void OnComplete() {
        foreach (Monster m in MonsterMaster.GetInstance().GetActiveMonsters().Values.ToList())
        {
            m.AddStress(m.isOverStressed() ? 2 * specialStressChange : specialStressChange);
        }
        UITasklessMonsterMaster.GetInstance().UpdateTasklessMonsters();
    }
}
