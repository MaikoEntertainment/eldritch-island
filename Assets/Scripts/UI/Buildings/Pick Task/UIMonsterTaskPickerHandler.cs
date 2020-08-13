using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIMonsterTaskPickerHandler : MonoBehaviour
{
    public Transform availableMonsterList;
    public Transform chosenMonsterList;

    public TextMeshProUGUI spaceAvailable;

    public UITaskMonsterPick taskMonsterPrefab;

    private Task task;

    public void Load(Task task)
    {
        this.task = task;
        UpdateLists();
    }

    public void UpdateLists()
    {
        ClearLists();
        foreach (Monster m in task.GetMonsters())
        {
            UITaskMonsterPick uiMonster = Instantiate(taskMonsterPrefab.gameObject, chosenMonsterList).GetComponent<UITaskMonsterPick>();
            uiMonster.Load(m, task);
            uiMonster.onClick += OnMonsterPress;
        }
        foreach (Monster m in MonsterMaster.GetInstance().GetTasklessMonsters().Values.ToList())
        {
            if (!task.GetMonsters().Contains(m))
            {
                UITaskMonsterPick uiMonster = Instantiate(taskMonsterPrefab.gameObject, availableMonsterList).GetComponent<UITaskMonsterPick>();
                uiMonster.Load(m, task);
                uiMonster.onClick += OnMonsterPress;
            }
        }
    }

    public void ClearLists()
    {
        foreach(Transform m in availableMonsterList)
        {
            Destroy(m.gameObject);
        }
        foreach (Transform m in chosenMonsterList)
        {
            Destroy(m.gameObject);
        }
    }

    public void Close()
    {
        UIBuildingMaster.GetInstance().CloseMonsterPicker();
    }

    public void OnMonsterPress(Monster monster)
    {
        if (!monster.CanWork(task)) return;
        if (task.GetMonsters().Contains(monster))
            task.RemoveMonster(monster);
        else
            task.AddMonsters(monster);
        UpdateLists();
    }
}
