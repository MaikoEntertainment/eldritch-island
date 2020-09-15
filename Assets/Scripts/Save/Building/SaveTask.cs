using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SaveTask
{
    public int taskId = 0;
    public double progressMade = 0;
    public List<MonsterIds> monsters = new List<MonsterIds>();
    public int iterationsLeft = 0;
    public bool isInfinite = false;

    public SaveTask(Task t)
    {
        taskId = t.GetTask().GetId();
        progressMade = t.GetProgressMade();
        foreach(Monster m in t.GetMonsters())
        {
            monsters.Add(m.GetId());
        }
        iterationsLeft = t.GetIterationsLeft();
        isInfinite = t.GetIsInfinite();
        // Note: All saved tasks are considered to have the property hasBegun = true
    }

    public int GetId() { return taskId; }
    public double GetProgressMade() { return progressMade; }
    public List<MonsterIds> GetMonsterIds() { return monsters; }
    public int GetIterationsLeft() { return iterationsLeft; }
    public bool GetIsInfinite() { return isInfinite; }
}
