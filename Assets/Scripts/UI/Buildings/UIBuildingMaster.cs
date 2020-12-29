using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildingMaster : MonoBehaviour
{
    private static UIBuildingMaster _instance;
    public Transform buildingList;
    public Transform taskCreator;

    public List<UIBuildingBase> UIBuildingsPrefabs;
    public UITaskCreatorHandler taskCreatorPrefab;

    private UITaskCreatorHandler creatorInstance;

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
    public static UIBuildingMaster GetInstance() { return _instance; }

    public void UpdateBuildingList()
    {
        ClearBuildings();
        List<Building> buildings = BuildingMaster.GetInstance().GetUnlockedBuildings();
        foreach (Building b in buildings)
        {
            foreach (UIBuildingBase ui in UIBuildingsPrefabs)
            {
                if (b.GetId() == ui.getId())
                {
                    Instantiate(ui.gameObject, buildingList)
                        .GetComponent<UIBuildingBase>().Initializae();
                    break;
                }
            }
        }
    }

    public void ClearBuildings()
    {
        foreach (Transform building in buildingList)
        {
            Destroy(building.gameObject);
        }
    }

    public void OpenTaskCreator(Building building)
    {
        CloseTaskCreator();
        creatorInstance = Instantiate(taskCreatorPrefab, taskCreator).GetComponent<UITaskCreatorHandler>();
        creatorInstance.Load(building);
    }

    public UITaskCreatorHandler GetCurrentTaskCreator()
    {
        return creatorInstance;
    }

    public void CloseTaskCreator()
    {
        foreach (Transform child in taskCreator)
        {
            Destroy(child.gameObject);
        }
        creatorInstance = null;
    }

    public void ViewTaskDetails(TaskBase task)
    {
        if (creatorInstance)
        {
            creatorInstance.LoadTaskDraft(task);
        }
    }

    public void CloseMonsterPicker()
    {
        if (creatorInstance)
        {
            creatorInstance.CloseMonsterPicker();
        }
    }
}
