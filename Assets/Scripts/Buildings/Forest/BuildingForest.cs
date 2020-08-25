using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingForest : Building
{
    protected override void Start()
    {
        base.Start();
        Building b = BuildingMaster.GetInstance().GetBuilding(BuildingIds.Campfire);
        b.onLevelUp += CheckForUnlock;
    }

    public void CheckForUnlock(Building b)
    {
        /* Change when message system is compleated to notify */
        if (b.GetLevel() == 2)
        {
            UIBuildingMaster.GetInstance().UpdateBuildingList();
        }
    }

    public override bool CanUnlock()
    {
        Building b = BuildingMaster.GetInstance().GetBuilding(BuildingIds.Campfire);
        return b.GetLevel() > 1;
    }
}
