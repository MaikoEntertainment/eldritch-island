using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UITaskCreatorHandler : MonoBehaviour
{
    public Transform taskList;
    public Transform monsterPicker;

    public Transform itemCostList;
    public Transform itemCostPerMonsterList;
    public Transform itemTotalCostList;
    public Transform resultList;
    public Transform monsterList;
    public TextMeshProUGUI progressNeeded;
    public TextMeshProUGUI progressPerSec;
    public TextMeshProUGUI stress;
    public TextMeshProUGUI description;

    public UITaskPickTask taskPrefab;
    public UIItem itemPrefab;
    public UIItemReward itemRewardPrefab;
    public UIMonsterTaskPickerHandler monsterPickerPrefab;
    public UITasklessMonster monsterPickedPrefab;

    private Building currentBuilding;
    private Task currentTask;

    public void Load(Building building)
    {
        currentBuilding = building;
        foreach (TaskBase tb in building.GetTasks())
        {
            Instantiate(taskPrefab.gameObject, taskList).GetComponent<UITaskPickTask>().Load(tb);
        }
        if (building.GetDraftTask()!=null)
        {
            LoadTask(building.GetDraftTask());
        }
        else
        {
            TaskBase first = building.GetUnlockedTasks()[0];
            LoadTask(building.CreateTask(first.GetId()));
        }
    }

    public void LoadTaskDraft(TaskBase task)
    {
        Task taskNew = currentBuilding.CreateTask(task.GetId());
        LoadTask(taskNew);
    }
    public void LoadTask(Task task)
    {
        currentTask = task;
        UpdateCurrentTask();
    }

    public void UpdateCurrentTask()
    {
        progressNeeded.text = "" + currentTask.GetTask().GetProgressNeeded();
        progressPerSec.text = currentTask.CalculateProgressPerSecond().ToString("F1") + "/s";
        stress.text = (currentTask.GetTask().GetStressChange() >= 0 ? "+" : "") + currentTask.GetTask().GetStressChange();
        description.text = currentTask.GetTask().GetDescription();
        ClearTask();
        foreach (Item i in currentTask.GetTask().GetItemCost())
        {
            Instantiate(itemPrefab.gameObject, itemCostList).GetComponent<UIItem>().Load(i);
        }
        foreach (Item i in currentTask.GetTask().GetCostPerMonster())
        {
            Instantiate(itemPrefab.gameObject, itemCostPerMonsterList).GetComponent<UIItem>().Load(i);
        }
        foreach (ItemReward i in currentTask.GetTask().GetItemRewards())
        {
            Instantiate(itemRewardPrefab.gameObject, resultList).GetComponent<UIItemReward>().Load(i);
        }
        foreach (Item i in currentTask.GetItemFinalCost())
        {
            Instantiate(itemPrefab.gameObject, itemTotalCostList).GetComponent<UIItem>().Load(i);
        }
        foreach (Monster m in currentTask.GetMonsters())
        {
            Instantiate(monsterPickedPrefab.gameObject, monsterList).GetComponent<UITasklessMonster>().Load(m);
        }
    }

    public void ClearTask()
    {
        foreach (Transform i in itemCostList)
        {
            Destroy(i.gameObject);
        }
        foreach (Transform i in itemCostPerMonsterList)
        {
            Destroy(i.gameObject);
        }
        foreach (Transform i in resultList)
        {
            Destroy(i.gameObject);
        }
        foreach (Transform i in itemTotalCostList)
        {
            Destroy(i.gameObject);
        }
        foreach (Transform m in monsterList)
        {
            Destroy(m.gameObject);
        }
    }

    public void Close()
    {
        UIBuildingMaster.GetInstance().CloseTaskCreator();
    }

    public void OpenMonsterPicker()
    {
        CloseMonsterPicker();
        Instantiate(monsterPickerPrefab.gameObject, monsterPicker).GetComponent<UIMonsterTaskPickerHandler>().Load(currentTask);
    }

    public void CloseMonsterPicker()
    {
        foreach(Transform t in monsterPicker)
        {
            Destroy(t.gameObject);
        }
        UpdateCurrentTask();
    }

    public void BeginTaskDraft()
    {
        bool result = currentBuilding.BeginDraftTask();
        if (result)
        {
            Close();
        }
            
    }
}
