using System;
using UnityEngine;

public class Statistic: ScriptableObject
{
    [SerializeField]
    protected StatisticIds id;
    [SerializeField]
    protected object value;

    // Events
    public delegate void UpdateValue(object value);
    public static event UpdateValue OnValueUpdate;

    public void SetValue(object value)
    {
        this.value = value;
        OnValueUpdate?.Invoke(value);
    }
}
