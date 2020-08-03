using System;
using UnityEngine;

public class Statistic: ScriptableObject
{
    [SerializeField]
    protected StatisticIds id;
    [SerializeField]
    protected object value;

    // Events
    public delegate void UpdateValueDelegate(object value);
    public event UpdateValueDelegate OnValueUpdate;

    public StatisticIds GetId() { return id; }
    public void UpdateValue(object value)
    {
        this.value = value;
        OnValueUpdate?.Invoke(value);
    }
}
