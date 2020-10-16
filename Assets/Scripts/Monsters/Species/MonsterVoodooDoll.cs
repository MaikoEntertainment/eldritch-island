using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterVoodooDoll : Monster
{
    public override void FinishWork(Task t)
    {
        base.FinishWork(t);
        foreach(Monster m in t.GetMonsters()){
            if (m.GetId() != GetId())
            {
                float stressDiff = Mathf.Max(0, m.GetStressAfterTask(t) - m.GetStress());
                m.AddStress(-1 * stressDiff);
            }
        }
    }
}
