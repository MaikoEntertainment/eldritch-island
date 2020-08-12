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

    public void Load(Task task)
    {
        foreach(Monster m in task.GetMonsters())
        {
            Instantiate(monsterPrefab.gameObject, monsterList).GetComponent<UITaskMonster>().Load(m);
        }
        progress.text = task.GetProgressMade() + "/" + task.GetProgressGoal();
        iterations.text = task.GetIterationsLeft().ToString();
    }



}
