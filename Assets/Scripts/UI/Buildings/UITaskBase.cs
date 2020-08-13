using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITaskBase : MonoBehaviour
{
    public TextMeshProUGUI progress;
    public TextMeshProUGUI iterations;
    public Transform monsterList;

    public UITaskMonster monsterPrefab;

    private Task task;
    public void Load(Task task)
    {
        this.task = task;
        task.onUpdate += UpdateValues;
    }

    public void UpdateValues(Task task)
    {
        ClearMonsters();
        foreach (Monster m in task.GetMonsters())
        {
            Instantiate(monsterPrefab.gameObject, monsterList).GetComponent<UITaskMonster>().Load(m);
        }
        progress.text = Utils.ToFormat((long)task.GetProgressMade()) + "/" + Utils.ToFormat((long)task.GetProgressGoal());
        iterations.text = (task.GetIterationsLeft()+1).ToString();
    }

    public void UpdateProgress()
    {
        
    }

    public void ClearMonsters()
    {
        foreach (Transform m in monsterList)
        {
            Destroy(m.gameObject);
        }
    }

    private void OnDisable()
    {
        task.onUpdate -= UpdateValues;
    }

}
