﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class Task
{
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

    public delegate void Updated(Task task);
    public Updated onUpdate;
    public delegate void Cleared(Task task);
    public Cleared onClear;

    public Task(TaskBase taskBase)
    {
        this.taskBase = taskBase;
    }

    public List<Monster> GetMonsters() { return monsters; }
    public TaskBase GetTask() { return taskBase; }
    public double GetProgressPerSecond() { return progressPerSecond; }
    public double GetProgressGoal() { return GetTask().GetProgressNeeded(); }
    public double GetProgressMade(){ return progress; }
    public int GetIterationsLeft() { return iterationsLeft; }
    public double CalculateProgressPerSecond()
    {
        float progress = 0;
        foreach(Monster m in GetMonsters())
        {
            float monsterProgress = 0;
            Dictionary<SkillIds, Skill> skills = m.GetSkills();
            foreach(SkillBonus s in taskBase.GetSkillsRequired())
            {
                int level = skills[s.GetSkillId()].GetLevel();
                monsterProgress += 1 + 0.1f * level;
            }
            if (taskBase.GetSkillsRequired().Count > 0)
                monsterProgress /= taskBase.GetSkillsRequired().Count;
            progress += monsterProgress;
        }
        progressPerSecond = progress;
        return progress;
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

    public void CheckForEnoughMonsters()
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
            totalCosts.Add(i.GetId(), i);
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
                    totalCosts.Add(i.GetId(), new Item(i.GetItemBase(),i.GetAmount()));
            }
        }
        return totalCosts.Values.ToList();
    }

    public void SetMonsters(List<Monster> monsters)
    {
        this.monsters = monsters;
    }
    public void AddMonsters(Monster m)
    {
        if (!monsters.Contains(m))
            monsters.Add(m);
    }
    public void RemoveMonster(Monster m)
    {
        if (monsters.Contains(m))
            monsters.Remove(m);
    }

    public bool StartTask()
    {
        CheckForEnoughMonsters();
        bool canPay = CanPayTask();
        if (!canPay) return false;
        // Check if its the first iteration of the Task
        if (!hasTaskBegun)
        {
            TimeMaster.GetInstance().OnTimePassed += PassTime;
            hasTaskBegun = true;
        }
        List<Item> realPerMonsterCost = new List<Item>();
        foreach (Monster m in GetMonsters())
        {
            realPerMonsterCost = m.GetTaskItemCostForThisMonster(this, realPerMonsterCost);
            m.AddStress(GetTask().GetStressChange());
        }
        foreach (Item i in realPerMonsterCost)
        {
            InventoryMaster.GetInstance().ChangeItemAmount(i.GetId(), -1 * i.GetAmount());
        }
        foreach (Item i in taskBase.GetItemCost())
        {
            InventoryMaster.GetInstance().ChangeItemAmount(i.GetId(), -1 * i.GetAmount());
        }
        CalculateProgressPerSecond();
        return true;
    }
    public void FinishTask()
    {
        List<ItemReward> finalItemsRewards = taskBase.GetItemRewards();
        List<Tool> finalToolRewards = taskBase.GetToolRewards();
        List<Clothes> finalClothesRewards = taskBase.GetClotheRewards();
        foreach (Monster m in GetMonsters())
        {
            finalItemsRewards = m.GetTaskItemRewards(this, finalItemsRewards);
            foreach (SkillBonus s in GetTask().GetSkillsRequired())
                m.AddSkillExp(s.GetSkillId(), s.GetLevelModifier());
        }
        foreach (ItemReward ir in finalItemsRewards)
        {
            Item i = ir.ObtainReward();
            InventoryMaster.GetInstance().ChangeItemAmount(i.GetId(), i.GetAmount());
        }
        foreach (Tool t in finalToolRewards)
        {
            InventoryMaster.GetInstance().AddTool(t);
        }
        foreach (Clothes c in finalClothesRewards)
        {
            InventoryMaster.GetInstance().AddClothes(c);
        }
        
        iterationsLeft -= 1;
        if (iterationsLeft > 0)
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

    public void PassTime(float timePassed)
    {
        double progressMade = timePassed * progressPerSecond;
        double extra = AddProgress(progressMade);
        if (extra > 0)
        {
            // Sets overflow progress to the next interation
            progress = extra;
            FinishTask();
        }
        onUpdate?.Invoke(this);
    }
    // Returns overflow progress
    public double AddProgress(double progressMade)
    {
        progress += progressMade;
        double extraProgresss = System.Math.Max(0, progress - GetProgressGoal());
        return extraProgresss;
    } 
}
