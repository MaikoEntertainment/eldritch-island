using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class Task
{
    public static int maxMonsters = 3;

    protected TaskBase taskBase;
    [SerializeField]
    protected List<Monster> monsters = new List<Monster>();
    [SerializeField]
    protected double progressPerSecond;
    [SerializeField]
    protected double progress;
    [SerializeField]
    protected int iterationsLeft;
    [SerializeField]
    protected bool hasTaskBegun = false;
    [SerializeField]
    protected bool isInfinite = false;

    protected double progressBuildingMultiplier = 1;

    public delegate void Updated(Task task);
    public Updated onUpdate;
    public delegate void Cleared(Task task);
    public Cleared onClear;

    public Task(TaskBase taskBase)
    {
        this.taskBase = taskBase;
    }

    public Task(TaskBase tb, SaveTask st)
    {
        taskBase = tb;
        progress = st.GetProgressMade();
        iterationsLeft = st.GetIterationsLeft();
        isInfinite = st.GetIsInfinite();
        foreach(MonsterIds ids in st.GetMonsterIds())
        {
            Monster m = MonsterMaster.GetInstance().GetActiveMonster(ids);
            if (m)
            {
                AddMonsters(m);
            }
        }
        CalculateProgressPerSecond();
    }
    public List<Monster> GetMonsters() { return monsters; }
    public TaskBase GetTask() { return taskBase; }
    public float GetStressChange()
    {
        return GetTask().GetStressChange();
    }
    public double GetProgressPerSecond() { return progressPerSecond; }
    public double GetProgressGoal() { return GetTask().GetProgressNeeded(); }
    public double GetProgressMade(){ return progress; }
    // Returns overflow progress
    public double AddProgress(double progressMade)
    {
        progress += progressMade;
        double extra = Math.Max(0, progress - GetProgressGoal());
        if (extra > 0)
        {
            // Sets overflow progress to the next interation
            progress = extra;
            FinishTask();
        }
        onUpdate?.Invoke(this);
        return extra;
    }
    public void SetBuildingProgress(double progress)
    {
        progressBuildingMultiplier = progress;
        CalculateProgressPerSecond();
    }
    public double GetBuildingProgressMultiplier() { return progressBuildingMultiplier; }
    public double OnProgressClick()
    {
        double progressClick = BuildingMaster.GetInstance().GetClickProgress();
        AddProgress(progressClick);
        return progressClick;
    }
    public int GetIterationsLeft() { return iterationsLeft; }
    public void SetIterationsLeft(int iterations) { iterationsLeft = iterations; }
    public void SetInfinite(bool value) { isInfinite = value; }
    public bool GetIsInfinite() { return isInfinite; }
    public double CalculateProgressPerSecond()
    {
        float progress = 0;
        foreach(Monster m in GetMonsters())
        {
            float monsterProgress = Math.Max(1, taskBase.GetSkillsRequired().Count);
            Dictionary<SkillIds, Skill> skills = m.GetFinalSkills(this);
            foreach(SkillBonus s in taskBase.GetSkillsRequired())
            {
                int level = skills[s.GetSkillId()].GetLevelWithBonuses();
                monsterProgress += 0.1f * level;
            }
            if (taskBase.GetSkillsRequired().Count > 0)
                monsterProgress /= taskBase.GetSkillsRequired().Count;
            progress += monsterProgress;
        }
        progressPerSecond = progress * progressBuildingMultiplier;
        return progressPerSecond;
    }

    public int GetCraftingPower()
    {
        int power = 0;
        foreach (Monster m in GetMonsters())
        {
            power += m.GetFinalSkills()[SkillIds.Crafting].GetLevelWithBonuses();
        }
        return power;
    }

    public List<Item> GetTaskFinalItemMonsterCost()
    {
        List<Item> realPerMonsterCost = GetTask().GetCostPerMonster();
        foreach (Monster m in GetMonsters())
        {
            realPerMonsterCost = m.GetTaskItemCostForThisMonster(this, realPerMonsterCost);
        }
        return realPerMonsterCost;
    }
    public void CheckForMonsterSanity()
    {
        List<Monster> filteredMonsters = new List<Monster>();
        foreach (Monster m in GetMonsters())
        {
            if (m.CanWork(this))
                filteredMonsters.Add(m);
        }
        SetMonsters(filteredMonsters);
        UITasklessMonsterMaster.GetInstance()?.UpdateTasklessMonsters();
    }
    public void SetMonsters(List<Monster> monsters)
    {
        this.monsters = monsters;
    }
    public void AddMonsters(Monster m)
    {
        if (!monsters.Contains(m) && monsters.Count < maxMonsters)
            monsters.Add(m);
    }
    public void RemoveMonster(Monster m)
    {
        if (monsters.Contains(m))
            monsters.Remove(m);
    }
    public bool CanPayTask()
    {
        List<Item> totalCosts = GetItemFinalCost();
        foreach (Item i in totalCosts)
        {
            Item itemInventory = InventoryMaster.GetInstance().GetItem(i.GetId());
            if (itemInventory == null || itemInventory.GetAmount() < i.GetAmount())
                return false;
        }
        return true;
    }
    public List<Item> GetItemFinalCost()
    {
        Dictionary<int, Item> totalCosts = new Dictionary<int, Item>();

        List<Item> perMonsterCost = GetTask().GetCostPerMonster().ToList();

        // Base Costs
        foreach(Item i in GetTask().GetItemCost())
        {
            totalCosts.Add(i.GetId(), i.Clone());
        }

        //Costs per Monster
        foreach (Monster m in GetMonsters())
        {
            List<Item> realPerMonsterCost = m.GetTaskItemCostForThisMonster(this, perMonsterCost);
            foreach (Item i in realPerMonsterCost)
            {
                if (totalCosts.ContainsKey(i.GetId()))
                    totalCosts[i.GetId()].ChangeAmount(i.GetAmount());
                else
                    totalCosts.Add(i.GetId(), i.Clone());
            }
        }
        return totalCosts.Values.ToList();
    }

    public List<ItemReward> GetFinalItemRewardsPreview()
    {
        List<ItemReward> finalItemsRewards = taskBase.GetItemRewards();
        foreach (Monster m in GetMonsters())
        {
            finalItemsRewards = m.GetTaskItemRewards(this, finalItemsRewards);
        }
        return finalItemsRewards;
    }

    public bool StartTask()
    {
        PrepareTask();
        bool canPay = CanPayTask();
        if (!canPay)
        {
            ClearTask();
            return false;
        }
        foreach (Item i in GetItemFinalCost())
        {
            InventoryMaster.GetInstance().ChangeItemAmount(i.GetId(), -1 * i.GetAmount());
        }
        CalculateProgressPerSecond();
        return true;
    }

    public void PrepareTask()
    {
        CheckForMonsterSanity();
        // Check if its the first iteration of the Task
        if (!hasTaskBegun)
        {
            TimeMaster.GetInstance().OnTimePassed += PassTime;
            hasTaskBegun = true;
        }

    }
    public void FinishTask()
    {
        GetTask().OnComplete();
        List<ItemReward> finalItemsRewards = GetFinalItemRewardsPreview();
        List<ToolBase> finalToolRewards = taskBase.GetToolRewards();
        List<ClothesBase> finalClothesRewards = taskBase.GetClotheRewards();
        List<Item> itemFinalValues = new List<Item>();
        List<Tool> toolToolFinalValues = new List<Tool>();
        List<Clothes> toolClothesFinalValues = new List<Clothes>();
        foreach (Monster m in GetMonsters())
        {
            foreach (SkillBonus s in GetTask().GetSkillsRequired())
                m.AddSkillExp(s.GetSkillId(), s.GetLevelModifier());
            m.FinishWork(this);
        }
        foreach (ItemReward ir in finalItemsRewards)
        {
            Item i = ir.ObtainReward();
            if (i.GetAmount() > 0)
            {
                InventoryMaster.GetInstance().ChangeItemAmount(i.GetId(), i.GetAmount());
                itemFinalValues.Add(i);
            }
        }
        int craftingPower = GetCraftingPower();
        foreach (ToolBase t in finalToolRewards)
        {
            Tool reward = InventoryMaster.GetInstance().CreateTool(t, craftingPower);
            InventoryMaster.GetInstance().AddTool(reward);
            toolToolFinalValues.Add(reward);
        }
        foreach (ClothesBase c in finalClothesRewards)
        {
            Clothes reward = InventoryMaster.GetInstance().CreateClothes(c, craftingPower);
            InventoryMaster.GetInstance().AddClothes(reward);
            toolClothesFinalValues.Add(reward);
        }
        NotificationMaster.GetInstance().SendTaskNotification(GetTask().GetNotificationOnEnd(this, itemFinalValues, toolToolFinalValues, toolClothesFinalValues));
        if (!GetIsInfinite())
            iterationsLeft -= 1;
        if (iterationsLeft >= 0)
        {
            StartTask();
        }
        else
        {
            ClearTask();
        }

    }

    public void ClearTask()
    {
        hasTaskBegun = false;
        TimeMaster.GetInstance().OnTimePassed -= PassTime;
        monsters = new List<Monster>();
        UITasklessMonsterMaster.GetInstance()?.UpdateTasklessMonsters();
        onClear?.Invoke(this);
    }

    public void CancelTask()
    {
        List<Item> usedItems = GetItemFinalCost();
        foreach (Item ic in usedItems)
        {
            InventoryMaster.GetInstance().ChangeItemAmount(ic.GetId(), ic.GetAmount());
        }
        ClearTask();
    }

    public void PassTime(float timePassed)
    {
        double progressMade = timePassed * progressPerSecond;
        AddProgress(progressMade);
    }

}
