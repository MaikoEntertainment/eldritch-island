using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildingBase : MonoBehaviour
{
    public TextMeshProUGUI buildingName;
    public TextMeshProUGUI level;
    public Transform taskList;

    public UITaskBase taskPrefab;

    [SerializeField]
    protected BuildingIds id;

    public BuildingIds getId() { return id; }

    public void Initializae()
    {
        Building b = BuildingMaster.GetInstance().GetBuilding(id);
        UpdateTasks();
        UpdateLevel(b);
        b.onTasksUpdated += UpdateTasks;
        b.onLevelUp += UpdateLevel;
        buildingName.text = b.GetName();
    }

    public void OnDisable()
    {
        Building b = BuildingMaster.GetInstance().GetBuilding(id);
        b.onTasksUpdated -= UpdateTasks;
        b.onLevelUp -= UpdateLevel;
    }

    public void UpdateLevel(Building b)
    {
        level.text = b.GetLevel().ToString();
    }

    public void UpdateTasks()
    {
        ClearTasks();
        List<Task> tasks = BuildingMaster.GetInstance().GetBuilding(id).GetActiveTasks();
        foreach (Task t in tasks)
        {
            Instantiate(taskPrefab.gameObject, taskList).GetComponent<UITaskBase>().Load(t);
        }
    }

    public void ClearTasks()
    {
        foreach (Transform tasks in taskList)
        {
            Destroy(tasks.gameObject);
        }
    }

    public void OpenTaskManager()
    {
        Building building = BuildingMaster.GetInstance().GetBuilding(id);
        UIBuildingMaster.GetInstance().OpenTaskCreator(building);
    }

    public void CloseTaskManager()
    {
        UIBuildingMaster.GetInstance().CloseTaskCreator();
    }

}
