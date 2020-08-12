using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsMaster : MonoBehaviour
{
    private static StatisticsMaster _instance;
    [SerializeField]
    private StatisticsDatabase database = new StatisticsDatabase();

    // Events
    public delegate void StatisticUpdate(Statistic statistic);
    public static event StatisticUpdate onStatisticUpdate;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    public static StatisticsMaster GetInstance() { return _instance; }

    public Dictionary<StatisticIds, Statistic> GetStatistics()
    {
        return database.GetStatistics();
    }

    public Statistic GetStatistic(StatisticIds id)
    {
        return database.GetStatistic(id);
    }

    public void UpdateStatistic(StatisticIds id, object value)
    {
        Statistic s = database.UpdateStatistic(id, value);
        onStatisticUpdate?.Invoke(s);
    }
}
