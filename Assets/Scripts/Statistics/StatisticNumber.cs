using UnityEngine;
[CreateAssetMenu(menuName = "Statistics/Number")]
public class StatisticNumber : Statistic
{
    [SerializeField]
    protected double initialValue = 0;
    public StatisticNumber()
    {
        value = initialValue;
    }
}
