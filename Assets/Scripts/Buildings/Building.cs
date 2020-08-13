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
    protected Dictionary<int, TaskBase> dictionaryTaskBases = new Dictionary<int, TaskBase>();
    [SerializeField]
    protected List<Task> tasksActive = new List<Task>();

    protected Task draftTask;

    public delegate void TaskUpdated();
    public TaskUpdated onTasksUpdated;

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
    public void DeleteTask(Task t)
    {
        t.onClear -= DeleteTask;
        GetActiveTasks().Remove(t);
        onTasksUpdated?.Invoke();
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
            bool result = draftTask.CanPayTask();
            if (result)
            {
                tasksActive.Add(draftTask);
                draftTask.StartTask();
                onTasksUpdated?.Invoke();
                draftTask.onClear += DeleteTask;
                CancelDraftTask();
            }
            return result;
        }
        return false;
    }
}
