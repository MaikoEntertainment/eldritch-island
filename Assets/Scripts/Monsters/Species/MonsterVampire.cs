using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterVampire : Monster
{
    public override bool AddStress(float stressChange)
    {
        if (stressChange < 0)
            stressChange *= 1.5f;
        return base.AddStress(stressChange);
    }

    public override float GetStressAfterTask(Task t)
    {
        float stressChange = t.GetTask().GetStressChange();
        if (stressChange < 0)
            stressChange *= 1.5f;
        return Mathf.Max(stress + stressChange, 0);
    }
}
