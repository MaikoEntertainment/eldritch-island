using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITaskCreatorHandler : MonoBehaviour
{
    public Transform taskList;

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

    public void Load(Building building)
    {
        foreach(TaskBase tb in building.GetTasks())
        {
            Instantiate(taskPrefab.gameObject, taskList).GetComponent<UITaskPickTask>().Load(tb, building);
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
    public void LoadTask(Task task)
    {
        progressNeeded.text = "" + task.GetTask().GetProgressNeeded();
        stress.text = (task.GetTask().GetStressChange() >= 0 ? "+" : "") + task.GetTask().GetStressChange();
        description.text = task.GetTask().GetDescription();
        progressPerSec.text = task.CalculateProgressPerSecond()+"/s";
        ClearTask();
        foreach (Item i in task.GetTask().GetItemCost())
        {
            Instantiate(itemPrefab.gameObject, itemCostList).GetComponent<UIItem>().Load(i);
        }
        foreach (Item i in task.GetTask().GetCostPerMonster())
        {
            Instantiate(itemPrefab.gameObject, itemCostPerMonsterList).GetComponent<UIItem>().Load(i);
        }
        foreach (ItemReward i in task.GetTask().GetItemRewards())
        {
            Instantiate(itemRewardPrefab.gameObject, resultList).GetComponent<UIItemReward>().Load(i);
        }
        foreach (Item i in task.GetItemFinalCost())
        {
            Instantiate(itemPrefab.gameObject, itemTotalCostList).GetComponent<UIItem>().Load(i);
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
    }

    public void Close()
    {
        UIBuildingMaster.GetInstance().CloseTaskCreator();
    }
}
