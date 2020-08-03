using System;
using UnityEngine;
[CreateAssetMenu]
public class StatisticDate : Statistic
{
    public StatisticDate()
    {
        value = new DateTime();
    }
}
