using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMonsterDragger : MonoBehaviour
{
    public Image icon;

    protected Monster monster;
    protected Task task = null;
    protected Task lastTaskColission = null;

    private void Start()
    {
        FollowMouse();
    }

    public void Load(Monster m, Task currentTask = null)
    {
        monster = m;
        task = currentTask;
        lastTaskColission = task;
        icon.sprite = m.GetIcon();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        UITaskBase colliderTask = collision.gameObject.GetComponent<UITaskBase>();
        if (colliderTask)
        {
            lastTaskColission = colliderTask.GetTask();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        UITaskBase colliderTask = collision.gameObject.GetComponent<UITaskBase>();
        if (colliderTask)
        {
            lastTaskColission = null;
        }
    }

    public void FollowMouse()
    {
        transform.position = Input.mousePosition;
    }

    public void ReasignMonster()
    {
        if (lastTaskColission == null)
        {
            if (task != null)
            {
                float leaveTaskPenalty = task.GetLeaveTaskPenalty();
                monster.AddStress(leaveTaskPenalty);
                task.RemoveMonster(monster);
            }
            UITasklessMonsterMaster.GetInstance().UpdateTasklessMonsters();
        }
        // If not on same task as before
        else if (task != lastTaskColission)
        {
            if (lastTaskColission.CanAddMonsters())
            {
                List<Item> costs = monster
                        .GetTaskItemCostForThisMonster(
                            lastTaskColission,
                            lastTaskColission.GetTask().GetCostPerMonster()
                        );
                bool canPay = InventoryMaster.GetInstance().CanPayItems(costs);
                float leaveTaskPenalty = task != null ? task.GetLeaveTaskPenalty() : 0;
                if (canPay && monster.CanWork(lastTaskColission, leaveTaskPenalty))
                {
                    InventoryMaster.GetInstance().PayItems(costs);
                    lastTaskColission.AddMonsters(monster);
                    // If change went well then remove monster from other task
                    if (task != null)
                    {
                        monster.AddStress(leaveTaskPenalty);
                        task.RemoveMonster(monster);
                    }
                }
            }
            
            UITasklessMonsterMaster.GetInstance().UpdateTasklessMonsters();
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        FollowMouse();
        if (Input.GetMouseButtonUp(0))
        {
            ReasignMonster();
        }
    }
}
