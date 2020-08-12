using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Task
{
    protected TaskBase taskBase;

    protected List<Monster> monsters = new List<Monster>();

    protected double progressPerSecond;

    protected double progress;

    protected int iterationsLeft;

    protected bool hasTaskBegun = false;

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
        this.progress = progress;
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

    public bool CheckForEnoughMonsters()
    {
        List<Monster> filteredMonsters = GetMonsters();
        foreach (Monster m in GetMonsters())
        {
            if (m.CanWork(GetTask().GetStressChange()))
                filteredMonsters.Add(m);
        }
        SetMonsters(filteredMonsters);
        return (filteredMonsters.Count > 0);
    }

    public bool CanPayTask()
    {
        Dictionary<int, Item> totalCosts = new Dictionary<int, Item>();

        List<Item> realPerMonsterCost = GetTask().GetCostPerMonster();

        foreach (Monster m in GetMonsters())
        {
            realPerMonsterCost = m.GetTaskItemCostForThisMonster(this, realPerMonsterCost);
            m.AddStress(GetTask().GetStressChange());
        }
        foreach (Item i in realPerMonsterCost)
        {
            totalCosts.Add(i.GetId(), i);
        }
        foreach (Item i in taskBase.GetItemCost())
        {
            if (totalCosts.ContainsKey(i.GetId()))
                totalCosts[i.GetId()].ChangeAmount(i.GetAmount());
            else
                totalCosts.Add(i.GetId(), i);
        }
        foreach (Item i in totalCosts.Values.ToList())
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

        List<Item> perMonsterCost = GetTask().GetCostPerMonster();

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
                    totalCosts.Add(i.GetId(), i);
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
        if (!monsters.Contains(m))
            monsters.Remove(m);
    }

    public bool StartTask()
    {
        bool hasEnough = CheckForEnoughMonsters();
        bool canPay = CanPayTask();
        if (!hasEnough || !canPay) return false;
        hasTaskBegun = true;
        List<Item> realPerMonsterCost = GetTask().GetCostPerMonster();
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
        hasTaskBegun = false;
        if (iterationsLeft > 0)
            StartTask();
    }
}
