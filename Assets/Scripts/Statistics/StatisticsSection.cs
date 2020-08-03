using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class StatisticsSection
{
    [SerializeField]
    protected string category = "";
    [SerializeField]
    protected List<Statistic> statistics;

    public List<Statistic> GetStatistics() { return statistics; }
}
