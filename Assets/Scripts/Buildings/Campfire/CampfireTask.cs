using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "Tasks/Campfire/Campfire")]
public class CampfireTask : TaskBase
{
    public override void OnComplete()
    {
        foreach(Monster m in MonsterMaster.GetInstance().GetTasklessMonsters().Values.ToList())
        {
            m.AddStress(-10);
        }
    }
}
