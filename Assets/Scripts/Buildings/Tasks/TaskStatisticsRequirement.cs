using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Tasks/Statistic Level Req")]
public class TaskStatisticsRequirement : TaskBaseBuildingLevelRequirement
{
    public List<StatisticRequirement> statisticRequirements = new List<StatisticRequirement>();

    public override bool IsAvailable()
    {
        foreach (StatisticRequirement sr in statisticRequirements)
            if (sr.levelRequirement > (double)StatisticsMaster.GetInstance().GetStatistic(sr.id).GetValue())
                return false;
        return base.IsAvailable();
    }
}
