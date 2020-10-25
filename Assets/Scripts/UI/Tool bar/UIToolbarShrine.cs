using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIToolbarShrine : MonoBehaviour
{
    public TextMeshProUGUI shrineLevel;

    private void Start()
    {
        StatisticValue sv = StatisticsMaster.GetInstance().GetStatistic(StatisticIds.ShrineBuilding);
        sv.OnValueUpdate += UpdateLevel;
        UpdateLevel(sv.GetValue());
    }

    public void UpdateLevel(object newLevel)
    {
        shrineLevel.text = newLevel.ToString();
    }

    public void OpenUpgrades()
    {
        UIUpgradeMaster.GetInstance().OpenUpgradesShrine();
    }
}
