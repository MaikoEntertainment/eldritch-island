using System;
[Serializable]
public class SaveStatistic
{
    public StatisticIds id;

    public SaveStatistic(StatisticValue statistic)
    {
        id = statistic.GetId();
    }

    public virtual object GetValue() { return null; }
}
