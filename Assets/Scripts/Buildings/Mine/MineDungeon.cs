using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Tasks/Mine/Mine Dungeon")]
public class MineDungeon : TaskStatisticsRequirement
{
    public StatisticIds statisticId;
    public double increase = 1;

    public override void OnComplete()
    {
        StatisticValue sv = StatisticsMaster.GetInstance().GetStatistic(statisticId);
        sv.UpdateValue((double)sv.GetValue() + increase);
    }
}
