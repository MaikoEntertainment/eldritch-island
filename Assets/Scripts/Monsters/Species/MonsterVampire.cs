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

    public override float GetStressAfterTask(Task t, float additionalStress=0)
    {
        float stressChange = t.GetTask().GetStressChange() + additionalStress;
        if (stressChange < 0)
            stressChange *= 1.5f;
        return Mathf.Max(stress + stressChange, 0);
    }
}
