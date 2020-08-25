using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Tasks/Task Buildings Level Req")]
public class TaskBaseBuildingLevelRequirement : TaskBase
{
    public List<TaskBuildingRequirement> requirements = new List<TaskBuildingRequirement>();
    public override bool IsAvailable()
    {
        foreach (TaskBuildingRequirement r in requirements)
        {
            Building b = BuildingMaster.GetInstance().GetBuilding(r.GetBuildingId());
            if (!b) return false;
            if (b.GetLevel() < r.GetMinLevelRequired()) return false;
        }
        return true;
    }
}
