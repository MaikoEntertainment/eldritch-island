using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Upgrades/Upgrade Increase")]
public class UpgradeBaseIncrease : UpgradeBase
{
    public float power = 0.5f;

    public override float GetBonus(int level = 0)
    {
        return Mathf.Pow(baseValue * level, power);
    }
}
