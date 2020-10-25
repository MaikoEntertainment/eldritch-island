using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIClearShrinePower : MonoBehaviour
{
    public Color startColor = Color.black;
    public Color endColor = Color.white;
    public float increasePerValue = 0.02f;

    public StatisticIds statisticId = StatisticIds.ShrineBuilding;

    private void OnEnable()
    {
        StatisticValue sv = StatisticsMaster.GetInstance().GetStatistic(statisticId);
        sv.OnValueUpdate += UpdateColor;
        UpdateColor(sv.GetValue());
    }

    private void OnDisable()
    {
        StatisticValue sv = StatisticsMaster.GetInstance().GetStatistic(statisticId);
        sv.OnValueUpdate -= UpdateColor;
    }

    public void UpdateColor(object value)
    {
        float percentageChanged = (float)((double)value * increasePerValue);
        GetComponent<Image>().color = Color.Lerp(startColor, endColor, percentageChanged);
    }
}
