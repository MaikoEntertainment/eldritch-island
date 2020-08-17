using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class StatisticsDatabase : ScriptableObject
{
    [SerializeField]
    protected List<StatisticsSection> sections = new List<StatisticsSection>();
    protected Dictionary<StatisticIds, Statistic> dictionary = new Dictionary<StatisticIds, Statistic>();

    public void InitializeDictionary()
    {
        foreach (StatisticsSection section in sections)
        {
            foreach (Statistic st in section.GetStatistics())
            {
                dictionary.Add(st.GetId(), st);
            }
        }
    }

    public Dictionary<StatisticIds, Statistic> GetStatistics()
    {
        return dictionary;
    }
    public Statistic GetStatistic(StatisticIds id)
    {
        if (dictionary.ContainsKey(id))
        {
            return dictionary[id];
        }
        return null;
    }
    public Statistic UpdateStatistic(StatisticIds id, object value)
    {
        Statistic s = GetStatistic(id);
        s.UpdateValue(s);
        return s;
    }

}
