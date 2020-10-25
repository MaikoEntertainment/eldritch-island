using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationTaskHope : NotificationTaskFinish
{
    public NotificationTaskHope(Notification not, Task task, List<Item> rewards, List<Tool> tools, List<Clothes> clothes) : base(not,task,rewards,tools,clothes)
    {}
    public override string GetTitle() { return notificationBase.GetTitle(); }
}
