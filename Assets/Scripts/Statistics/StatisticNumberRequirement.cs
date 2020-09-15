using System;
[Serializable]
public class StatisticNumberRequirement
{
    public StatisticIds id;
    public double amountRequirement;

    public bool MeetsCondition()
    {
        double value = (double)StatisticsMaster.GetInstance().GetStatistic(id).GetValue();
        return amountRequirement <= value;
    }
}
