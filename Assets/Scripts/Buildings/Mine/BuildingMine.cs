using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMine : Building
{
    protected override void Start()
    {
        base.Start();
        Building b = BuildingMaster.GetInstance().GetBuilding(BuildingIds.CraftHouse);
        b.onLevelUp += CheckForUnlock;
    }

    public void CheckForUnlock(Building b)
    {
        /* Change when message system is compleated to notify */
        if (b.GetLevel() == 4)
        {
            UIBuildingMaster.GetInstance().UpdateBuildingList();
        }
    }

    public override bool CanUnlock()
    {
        Building b = BuildingMaster.GetInstance().GetBuilding(BuildingIds.CraftHouse);
        return b.GetLevel() >= 4;
    }

    public override int GetTaskSlots()
    {
        return 1 + GetLevel() / 5;
    }
}
