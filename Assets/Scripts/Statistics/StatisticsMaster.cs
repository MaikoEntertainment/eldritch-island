using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatisticsMaster : MonoBehaviour
{
    private static StatisticsMaster _instance;
    [SerializeField]
    private StatisticsDatabase database = new StatisticsDatabase();
    private Dictionary<StatisticIds, StatisticValue> statistics;

    // Events
    public delegate void StatisticUpdate(StatisticValue statistic);
    public static event StatisticUpdate onStatisticUpdate;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            InitializaDictionary();
        }
        else
        {
            Destroy(this);
        }
    }

    public void InitializaDictionary()
    {
        database.InitializeDictionary();
        statistics = new Dictionary<StatisticIds, StatisticValue>();
        foreach (Statistic s in database.GetStatistics().Values.ToList())
        {
            statistics.Add(s.GetId(), new StatisticValue(s));
        }
    }
    public static StatisticsMaster GetInstance() { return _instance; }

    public Dictionary<StatisticIds, StatisticValue> GetStatistics()
    {
        return statistics;
    }

    public StatisticValue GetStatistic(StatisticIds id)
    {
        return statistics[id];
    }

    public void UpdateStatistic(StatisticIds id, object value)
    {
        StatisticValue s = statistics[id].UpdateValue(value);
        onStatisticUpdate?.Invoke(s);
    }
}
