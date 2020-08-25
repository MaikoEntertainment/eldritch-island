using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TaskBuildingRequirement
{
    [SerializeField]
    protected BuildingIds buildingId;
    [SerializeField]
    protected int minLevelRequired;

    public BuildingIds GetBuildingId()
    {
        return buildingId;
    }
    public int GetMinLevelRequired() { return minLevelRequired; }
}
