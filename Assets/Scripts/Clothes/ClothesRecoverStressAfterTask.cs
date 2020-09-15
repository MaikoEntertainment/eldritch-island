using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Clothes/Change Stress")]
public class ClothesRecoverStressAfterTask : ClothesBase
{
    public float stressChange = -1f;
    public float changePerTier = -0.2f;
    public override void Use(Monster m, Task task, int tier=0)
    {
        base.Use(m, task);
        float finalGain = GetFinalChange(tier);
        m.AddStress(finalGain);
    }

    protected float GetFinalChange(int tier = 0) { return stressChange + tier * changePerTier; }

    public override string GetDescription(int tier=0)
    {
        return base.GetDescription() + " " + GetFinalChange(tier);
    }
}
