using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Statistics/Date")]
public class StatisticDate : Statistic
{
    public StatisticDate()
    {
        value = new DateTime();
    }
}
