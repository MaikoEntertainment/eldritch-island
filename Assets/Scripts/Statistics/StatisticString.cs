using UnityEngine;
[CreateAssetMenu]
public class StatisticString : Statistic
{
    [SerializeField]
    protected string initialValue = "";
    public StatisticString()
    {
        value = initialValue;
    }
}

