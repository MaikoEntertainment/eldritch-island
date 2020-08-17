using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticValue 
{
    protected Statistic statistic;

    protected object value;

    // Events
    public delegate void UpdateValueDelegate(object value);
    public event UpdateValueDelegate OnValueUpdate;

    public StatisticValue(Statistic s)
    {
        statistic = s;
        // Sets initial Value
        value = s.GetValue();
    }
    public StatisticValue UpdateValue(object value)
    {
        this.value = value;
        OnValueUpdate?.Invoke(value);
        return this;
    }

    public object GetValue()
    {
        return value;
    }
}
