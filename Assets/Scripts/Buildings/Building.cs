using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    protected BuildingIds id;
    [SerializeField]
    protected int initialTaskSlots = 1;
    protected int level = 0;

    [SerializeField]
    protected TextLanguageOwn myName;

    [SerializeField]
    protected List<TaskBase> taskBases;
    protected Dictionary<int, TaskBase> dictionaryTaskBases = new Dictionary<int, TaskBase>();
    protected List<Task> tasksActive = new List<Task>();

    protected Task draftTask;

    public delegate void TaskUpdated();
    public TaskUpdated onTasksUpdated;
    public delegate void LevelUped(Building building);
    public LevelUped onLevelUp;

    protected virtual void Awake()
    {
        foreach (TaskBase tb in taskBases)
            dictionaryTaskBases.Add(tb.GetId(), tb);
    }

    protected virtual void Start()
    {

    }

    public void Load(SaveBuilding sb)
    {
        level = sb.GetLevel();
        foreach(SaveTask st in sb.GetTasks())
        {
            TaskBase tb = dictionaryTaskBases[st.GetId()];
            Task t = new Task(tb, st);
            tasksActive.Add(t);
            t.PrepareTask();
            t.SetBuildingProgress(GetBuildingProgressMultiplier());
            t.onClear += DeleteTask;
        }
        
    }

    public BuildingIds GetId() { return id; }
    public int GetLevel() { return level; }
    public virtual void LevelUp()
    {
        level++;
        onLevelUp?.Invoke(this);
    }
    public virtual int GetTaskSlots() { return initialTaskSlots; }
    public string GetName() { return myName.GetText(); }
    public List<TaskBase> GetTasks() { return taskBases; }
    public List<Task> GetActiveTasks() { return tasksActive; }
    public List<TaskBase> GetUnlockedTasks() {
        List<TaskBase> unlocked = new List<TaskBase>();
        foreach (TaskBase tb in GetTasks())
            if (tb.IsAvailable())
                unlocked.Add(tb);
        return unlocked; 
    }
    public virtual List<Item> GetLevelUpCost(int level) 
    {
        Console.WriteLine("Replace with own cost");
        return new List<Item>(); 
    }
    public virtual double GetBuildingProgressMultiplier()
    {
        return 1 + 0.01f * GetLevel();
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
            draftTask.SetBuildingProgress(GetBuildingProgressMultiplier());
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

    public bool CanBeginDraft()
    {
        if (draftTask == null) return false;
        bool canPay = draftTask.CanPayTask();
        return canPay;
    }
    public bool BeginDraftTask()
    {
        if (CanBeginDraft())
        {
            if (tasksActive.Count >= GetTaskSlots())
            {
                int indexToRemove = tasksActive.Count - 1;
                tasksActive[indexToRemove].CancelTask();
            }
            tasksActive.Add(draftTask);
            draftTask.StartTask();
            onTasksUpdated?.Invoke();
            draftTask.onClear += DeleteTask;
            CancelDraftTask();
            return true;
        }
        return false;
    }
}
