using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToolBarMaster : MonoBehaviour
{
    public static UIToolBarMaster _instance;

    public UIToolBarMonsterSummoner summoner;
    public UIToolbarUpgrade upgrades;
    public UIToolbarShrine shrine;

    void Awake()
    {
        if (_instance)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    public void Start()
    {
        Building b = BuildingMaster.GetInstance().GetBuilding(BuildingIds.Campfire);
        UpdateSummoner();
        b.onLevelUp += (Building bu) => { UpdateSummoner(); };
        MonsterMaster.GetInstance().onMonsterActivated += (Monster mon) => UpdateSummoner();

        StatisticValue sv = StatisticsMaster.GetInstance().GetStatistic(StatisticIds.Dungeon1Clears);
        sv.OnValueUpdate += UpdateUpgrade;
        UpdateUpgrade(sv.GetValue());

        StatisticValue svs = StatisticsMaster.GetInstance().GetStatistic(StatisticIds.ShrineBuilding);
        svs.OnValueUpdate += UpdateShrine;
        UpdateShrine(svs.GetValue());
    }

    public void UpdateSummoner()
    {
        int campfireLevel = BuildingMaster.GetInstance().GetBuilding(BuildingIds.Campfire).GetLevel();
        if (campfireLevel == 0) return;
        summoner.gameObject.SetActive(true);
        int availableSummons = MonsterMaster.GetInstance().GetAvailableSummons();
        summoner.UpdateAvailableSummons(availableSummons);
    }
    public void UpdateUpgrade(object value)
    {
        double clears = (double)value;
        if (clears < 10) return;
        upgrades.gameObject.SetActive(true);
    }

    public void UpdateShrine(object value)
    {
        double clears = (double)value;
        if (clears < 10) return;
        shrine.gameObject.SetActive(true);
    }
}
