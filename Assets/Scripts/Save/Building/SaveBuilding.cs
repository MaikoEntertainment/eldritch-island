using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SaveBuilding
{
    public BuildingIds id;
    public int level = 0;
    public List<SaveTask> tasks = new List<SaveTask>();

    public SaveBuilding(Building b)
    {
        id = b.GetId();
        level = b.GetLevel();
        tasks = new List<SaveTask>();
        foreach (Task t in b.GetActiveTasks())
            tasks.Add(new SaveTask(t));
    }

    public BuildingIds GetId() { return id; }
    public int GetLevel() { return level; }
    public List<SaveTask> GetTasks() { return tasks; }
}
