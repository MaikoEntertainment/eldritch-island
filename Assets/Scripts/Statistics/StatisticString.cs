using UnityEngine;
[CreateAssetMenu(menuName = "Statistics/String")]
public class StatisticString : Statistic
{
    [SerializeField]
    protected string initialValue = "";
    public StatisticString()
    {
        value = initialValue;
    }
}

