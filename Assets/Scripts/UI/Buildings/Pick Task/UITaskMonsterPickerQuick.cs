using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UITaskMonsterPickerQuick : MonoBehaviour
{
    public TextMeshProUGUI progressPerSecond;
    public TextMeshProUGUI leavePenalty;

    public Transform activeList;
    public Transform inactiveList;
    public Transform costList;

    public UITaskMonsterPickQuick monsterPick;
    public UIItem costPrefab;

    private Task task;
    public void Load(Task t)
    {
        ClearLists();
        t.CalculateProgressPerSecond();
        t.PauseActiveTask();
        task = t;
        progressPerSecond.text = t.GetProgressPerSecond().ToString("F2") + "/s";
        float leaveTaskPenalty = task.GetLeaveTaskPenalty();
        if (leaveTaskPenalty > 0)
        {
            leavePenalty.text = "+" + leaveTaskPenalty.ToString("F2");
            leavePenalty.color = Utils.GetWrongColor();
        } else if (leaveTaskPenalty < 0)
        {
            leavePenalty.text = leaveTaskPenalty.ToString("F2");
            leavePenalty.color = Utils.GetSuccessColor();
        }
        else
        {
            leavePenalty.text = leaveTaskPenalty.ToString("F2");
        }
        foreach (Monster m in t.GetMonsters())
        {
            UITaskMonsterPickQuick mp = Instantiate(monsterPick.gameObject, activeList).GetComponent<UITaskMonsterPickQuick>().Load(m, t);
            mp.onToogle += (Monster mo, Task ta) => Load(t);
        }
        foreach (Monster m in MonsterMaster.GetInstance().GetTasklessMonsters().Values.ToList())
        {
            List<Item> costs = m.GetTaskItemCostForThisMonster(t, t.GetTask().GetCostPerMonster());
            bool canPay = InventoryMaster.GetInstance().CanPayItems(costs);
            if (canPay)
            {
                UITaskMonsterPickQuick mp = Instantiate(monsterPick.gameObject, inactiveList).GetComponent<UITaskMonsterPickQuick>().Load(m, t);
                mp.onToogle += (Monster mo, Task ta) => Load(t);
            }
        }
        foreach(Item i in task.GetTask().GetCostPerMonster())
        {
            bool canPay = InventoryMaster.GetInstance().CanPayItem(i.GetId(), i.GetAmount());
            Instantiate(costPrefab.gameObject, costList).GetComponent<UIItem>().Load(i, !canPay);
        }
    }

    public void ClearLists()
    {
        foreach (Transform t in activeList)
            Destroy(t.gameObject);
        foreach (Transform t in inactiveList)
            Destroy(t.gameObject);
        foreach (Transform t in costList)
            Destroy(t.gameObject);
    }

    public void Close()
    {
        task.ResumeActiveTask();
        Destroy(gameObject);
    }
}
