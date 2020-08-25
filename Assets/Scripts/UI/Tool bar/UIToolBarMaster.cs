using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToolBarMaster : MonoBehaviour
{
    public static UIToolBarMaster _instance;

    public UIToolBarMonsterSummoner summoner;

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
    }

    public void UpdateSummoner()
    {
        int campfireLevel = BuildingMaster.GetInstance().GetBuilding(BuildingIds.Campfire).GetLevel();
        if (campfireLevel == 0) return;
        summoner.gameObject.SetActive(true);
        int availableSummons = MonsterMaster.GetInstance().GetAvailableSummons();
        summoner.UpdateAvailableSummons(availableSummons);
    }
}
