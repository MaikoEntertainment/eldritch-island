using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    protected BuildingIds id;
    protected int level = 0;

    [SerializeField]
    protected List<TaskBase> taskBases;
    [SerializeField]
    protected List<Task> tasksActive;

    protected Task draftTask;

    protected Dictionary<int, TaskBase> dictionaryTaskBases = new Dictionary<int, TaskBase>();
    private void Start()
    {
        foreach (TaskBase tb in taskBases)
            dictionaryTaskBases.Add(tb.GetId(), tb);
    }
    public BuildingIds GetId() { return id; }
    public int GetLevel() { return level; }
    public List<TaskBase> GetTasks() { return taskBases; }
    public List<Task> GetActiveTasks() { return tasksActive; }
    public List<TaskBase> GetUnlockedTasks() { return taskBases; }
    public virtual List<Item> GetLevelUpCost(int level) 
    {
        Console.WriteLine("Replace with own cost");
        return new List<Item>(); 
    }
    public virtual bool CanUnlock()
    {
        Console.WriteLine("Replace with own condition");
        return true;
    }
    public void RecalculateTasksProgressPerSecond()
    {
        foreach (Task t in GetActiveTasks())
            t.CalculateProgressPerSecond();
    }

    public Task CreateTask(int taskId)
    {
        if (dictionaryTaskBases.ContainsKey(taskId))
        {
            TaskBase tb = dictionaryTaskBases[taskId];
            draftTask = new Task(tb);
            return draftTask;
        }
        return null;
    }
    public Task GetDraftTask()
    {
        return draftTask;
    }
    public void CancelDraftTask()
    {
        draftTask = null;
    }
    public bool BeginDraftTask()
    {
        if (draftTask != null)
        {
            return draftTask.StartTask();
        }
        return false;
    }
}
