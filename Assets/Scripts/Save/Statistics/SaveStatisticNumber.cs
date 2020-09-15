using System;
[Serializable]
public class SaveStatisticNumber : SaveStatistic
{
    public double value;

    public SaveStatisticNumber(StatisticValue s) : base(s) 
    {
        value = (double)s.GetValue();
    }

    public override object GetValue()
    {
        return value;
    }
}
