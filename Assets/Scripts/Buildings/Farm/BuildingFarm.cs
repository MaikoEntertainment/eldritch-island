using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFarm : Building
{
    int forestLevelReq = 7;

    protected override void Start()
    {
        base.Start();
        Building b = BuildingMaster.GetInstance().GetBuilding(BuildingIds.Forest);
        b.onLevelUp += CheckForUnlock;
    }
    public void CheckForUnlock(Building b)
    {
        /* Change when message system is compleated to notify */
        if (b.GetLevel() == forestLevelReq)
        {
            UIBuildingMaster.GetInstance().UpdateBuildingList();
        }
    }
    public override bool CanUnlock()
    {
        Building b = BuildingMaster.GetInstance().GetBuilding(BuildingIds.Forest);
        return b.GetLevel() >= forestLevelReq;
    }
    public override int GetTaskSlots()
    {
        return 1 + GetLevel() / 10;
    }
}
