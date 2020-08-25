using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationBuildingLevelUp : NotificationTaskFinish
{
    public NotificationBuildingLevelUp(Notification not, Task task, List<Item> rewards, List<Tool> tools, List<Clothes> clothes) : base(not,task,rewards,tools,clothes)
    {

    }

    public override string GetTitle() {
        Building b = BuildingMaster.GetInstance().GetBuilding(((TaskBaseLevelUp)task.GetTask()).GetBuildingIds());
        return b.GetName() + " " + notificationBase.GetTitle() +" " + b.GetLevel()+"!"; 
    }
}
