using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingMaster : MonoBehaviour
{
    public static BuildingMaster _instance;
    public List<Building> buildingsPrefabs;
    protected Dictionary<BuildingIds,Building> buildingDictionary = new Dictionary<BuildingIds, Building>();

    void Awake()
    {
        if (_instance)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
            InitializeDictionary();
        }
    }

    private void Start()
    {
        UIBuildingMaster.GetInstance().UpdateBuildingList(buildingDictionary.Values.ToList());
    }

    public static BuildingMaster GetInstance() { return _instance; }

    public void InitializeDictionary()
    {
        foreach (Building b in buildingsPrefabs)
            buildingDictionary.Add(b.GetId(), Instantiate(b.gameObject).GetComponent<Building>());
    }

    public Building GetBuilding(BuildingIds id)
    {
        if (buildingDictionary.ContainsKey(id))
            return buildingDictionary[id];
        return null;
    }
    public List<Building> GetBuildings()
    {
        return buildingDictionary.Values.ToList();
    }

    public List<Building> GetUnlockedBuildings()
    {
        List<Building> unlocked = new List<Building>();
        foreach(Building b in buildingsPrefabs)
        {
            if (b.CanUnlock()) unlocked.Add(b);
        }
        return unlocked;
    }
}
