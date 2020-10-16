using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Tasks/Task Level Up")]
public class TaskBaseLevelUp : TaskBase
{
    [SerializeField]
    protected BuildingIds buildingId;
    public override void OnComplete()
    {
        BuildingMaster.GetInstance().GetBuilding(buildingId).LevelUp();
    }

    public override NotificationTaskFinish GetNotificationOnEnd(Task task, List<Item> rewards, List<Tool> tools, List<Clothes> clothes)
    {
        return new NotificationBuildingLevelUp(onEndNotification, task, rewards, tools, clothes);
    }

    // Base method for getting increasing costs
    public override List<Item> GetItemCost()
    {
        int level = BuildingMaster.GetInstance().GetBuilding(buildingId).GetLevel();
        if (level == 0) return base.GetItemCost();
        List<Item> trueCosts = new List<Item>();
        foreach (Item cost in base.GetItemCost())
        {
            Item newCost = cost.Clone();
            newCost.ChangeAmount((int)(newCost.GetAmount() * level * 0.5f));
            trueCosts.Add(newCost);
        }
        return trueCosts;
    }
    // Base Method for getting increasing Progress Needed
    public override double GetProgressNeeded()
    {
        int level = BuildingMaster.GetInstance().GetBuilding(buildingId).GetLevel();
        return base.GetProgressNeeded() * (1 + level);
    }
    public override bool IsAvailable()
    {
        bool isActive = false;
        // Checks if the tasks is already being developed
        List<Task> tasks = BuildingMaster.GetInstance().GetBuilding(buildingId).GetActiveTasks();
        foreach(Task t in tasks)
            if (t.GetTask().GetId() == GetId())
            {
                isActive = true;
                break;
            }
        return base.IsAvailable() && !isActive;
    }

    public BuildingIds GetBuildingIds() { return buildingId; }
}
