using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFungus : Monster
{
    public override bool AddStress(float stressChange)
    {
        if (stressChange > 0)
            stressChange *= 0.9f;
        return base.AddStress(stressChange);
    }

    public override float GetStressAfterTask(Task t)
    {
        float stressChange = t.GetTask().GetStressChange();
        if (stressChange > 0)
            stressChange *= 0.9f;
        return Mathf.Max(stress + stressChange, 0);
    }
}
