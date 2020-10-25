using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Clothes/Tool Repair")]
public class ClothesRepearTools : ClothesBase
{
    public double repairValue = 0.75;
    public double increasePerLevel = 0.05d;

    public override void Use(Monster m, Task task, int tier = 0)
    {
        base.Use(m, task, tier);
        foreach(Tool t in m.GetTools())
        {
            t.ChangeDurability(GetBonus());
        }
    }

    public double GetBonus(int tier = 0)
    {
        return repairValue + increasePerLevel * tier;
    }

    public override string GetDescription(int tier = 0)
    {
        return base.GetDescription(tier) + GetBonus(tier).ToString("F2");
    }
}
