using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Tasks/Shrine")]
public class TaskShrine : TaskIncreaseStatisticOnClear
{
    public Notification notificationHope;

    public override void OnComplete()
    {
        base.OnComplete();
        double amount = (double)StatisticsMaster.GetInstance().GetStatistic(StatisticIds.ShrineBuilding).GetValue();
        if (amount >= 10)
            LetterMaster.GetInstance().UnlockLetter(LetterId.shrine1);
    }

    public override NotificationTaskFinish GetNotificationOnEnd(Task task, List<Item> rewards, List<Tool> tools, List<Clothes> clothes)
    {
        double amount = (double)StatisticsMaster.GetInstance().GetStatistic(StatisticIds.ShrineBuilding).GetValue();
        if (amount > 7)
            return new NotificationTaskHope(notificationHope, task, rewards, tools, clothes);
        return base.GetNotificationOnEnd(task, rewards, tools, clothes);
    }
}
