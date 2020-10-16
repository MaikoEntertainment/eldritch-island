using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITaskCreatorHandler : MonoBehaviour
{
    public Transform taskList;
    public Transform monsterPicker;
    public Transform errorArea;
    public InputField iterations;
    public Toggle toggle;
    public Button done;

    public TextMeshProUGUI level;
    public TextMeshProUGUI usedSlots;
    public TextMeshProUGUI totalSlots;
    public Image selectedTaskIcon;
    public TextMeshProUGUI selectedTaskName;
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
    public UITaskLocked taskLockedPrefab;
    public UIItem itemPrefab;
    public UIItemReward itemRewardPrefab;
    public UIMonsterTaskPickerHandler monsterPickerPrefab;
    public UITasklessMonster monsterPickedPrefab;
    public UIToolReward toolRewardPrefab;
    public UIError errorPrefab;

    public TextLanguageOwn reasourcesError;
    public TextLanguageOwn slotsError;

    private Building currentBuilding;
    private Task currentTask;

    public void Load(Building building)
    {
        currentBuilding = building;
        if (building.GetDraftTask()!=null)
        {
            LoadTask(building.GetDraftTask());
        }
        else
        {
            TaskBase first = building.GetUnlockedTasks()[0];
            LoadTaskDraft(first);
        }
        UpdateTaskList();
        int used = building.GetActiveTasks().Count;
        usedSlots.text = used.ToString();
        usedSlots.color = (used < building.GetTaskSlots()) ? Utils.GetSuccessColor() : Utils.GetWrongColor();
        totalSlots.text = building.GetTaskSlots().ToString();
        level.text = building.GetLevel().ToString();
    }

    public void LoadTaskDraft(TaskBase task)
    {
        Task taskNew = currentBuilding.CreateTask(task.GetId());
        taskNew.SetInfinite(toggle.isOn);
        iterations.interactable = !toggle.isOn;
        LoadTask(taskNew);
    }
    public void LoadTask(Task task)
    {
        currentTask = task;
        UpdateCurrentTask();
        UpdateTaskList();
    }

    public void UpdateTaskList()
    {
        foreach (Transform task in taskList)
        {
            Destroy(task.gameObject);
        }
        foreach (TaskBase tb in currentBuilding.GetUnlockedTasks())
        {
            bool isSelected = (tb.GetId() == currentBuilding.GetDraftTask().GetTask().GetId());
            Instantiate(taskPrefab.gameObject, taskList).GetComponent<UITaskPickTask>().Load(tb, isSelected);
        }
        foreach (TaskBase tb in currentBuilding.GetTasks())
        {
            if (!tb.IsAvailable())
                Instantiate(taskLockedPrefab.gameObject, taskList).GetComponent<UITaskLocked>().Load(tb);
        }
    }

    public void UpdateCurrentTask()
    {
        selectedTaskIcon.sprite = currentTask.GetTask().GetIcon();
        selectedTaskName.text = currentTask.GetTask().GetName();
        progressNeeded.text = "" + (int)currentTask.GetTask().GetProgressNeeded();
        progressPerSec.text = currentTask.CalculateProgressPerSecond().ToString("F2") + "/s";
        stress.text = (currentTask.GetTask().GetStressChange() >= 0 ? "+" : "") + currentTask.GetTask().GetStressChange().ToString("F1");
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
        foreach (ItemReward i in currentTask.GetFinalItemRewardsPreview())
        {
            Instantiate(itemRewardPrefab.gameObject, resultList).GetComponent<UIItemReward>().Load(i);
        }
        foreach (ToolBase tr in currentTask.GetTask().GetToolRewards())
        {
            Instantiate(toolRewardPrefab.gameObject, resultList).GetComponent<UIToolReward>().Load(tr);
        }
        foreach (ClothesBase cr in currentTask.GetTask().GetClotheRewards())
        {
            Instantiate(toolRewardPrefab.gameObject, resultList).GetComponent<UIToolReward>().Load(cr);
        }
        foreach (Item i in currentTask.GetItemFinalCost())
        {
            UIItem uiCost = Instantiate(itemPrefab.gameObject, itemTotalCostList).GetComponent<UIItem>();
            Item iInventory = InventoryMaster.GetInstance().GetItem(i.GetId());
            long inventoryAmount = iInventory != null ? iInventory.GetAmount() : 0;
            uiCost.Load(i, i.GetAmount() > inventoryAmount);

        }
        foreach (Monster m in currentTask.GetMonsters())
        {
            Instantiate(monsterPickedPrefab.gameObject, monsterList).GetComponent<UITasklessMonster>().Load(m);
        }
        //done.interactable = currentBuilding.CanBeginDraft();
    }

    public void UpdateIterations(string newValue)
    {
        currentTask.SetIterationsLeft(int.Parse(newValue, CultureInfo.InvariantCulture.NumberFormat));
    }

    public void UpdateIsInfinite(bool isInfinite)
    {
        currentTask.SetInfinite(isInfinite);
        iterations.interactable = !isInfinite;
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
        currentBuilding.CancelDraftTask();
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
        if (!currentTask.CanPayTask())
        {
            Instantiate(errorPrefab.gameObject, errorArea).GetComponent<UIError>().Load(reasourcesError.GetText());
            return;
        }
        int used = currentBuilding.GetActiveTasks().Count;
        if (used >= currentBuilding.GetTaskSlots())
        {
            Instantiate(errorPrefab.gameObject, errorArea).GetComponent<UIError>().Load(slotsError.GetText());
            return;
        }
        bool result = currentBuilding.BeginDraftTask();
        if (result)
        {
            Close();
        }
            
    }
}
