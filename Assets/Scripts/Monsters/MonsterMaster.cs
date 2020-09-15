﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class MonsterMaster : MonoBehaviour
{
    private static MonsterMaster _instance;

    [SerializeField]
    protected MonsterDatabase database;
    protected Dictionary<MonsterIds, Monster> activeMonsters = new Dictionary<MonsterIds, Monster>();

    public delegate void MonsterActivated(Monster m);
    public MonsterActivated onMonsterActivated;
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            database.InitializeDictionary();
        }
        else
        {
            Destroy(this);
        }
    }

    public void Load(List<SaveMonster> monsterSaves)
    {
        foreach(SaveMonster sm in monsterSaves)
        {
            Monster m = PickMonster(sm.GetMonsterId());
            m.Load(sm);
        }
    }

    public static MonsterMaster GetInstance() { return _instance; }
    public Dictionary<MonsterIds, Monster> GetMonsters()
    {
        return database.GetMonsters();
    }

    public Dictionary<MonsterIds, Monster> GetActiveMonsters()
    {
        return activeMonsters;
    }
    public int GetAvailableSummons()
    {
        int campfireLevel = BuildingMaster.GetInstance().GetBuilding(BuildingIds.Campfire).GetLevel();
        if (campfireLevel < 1) return 0;
        int mounstersCount = activeMonsters.Count;
        int summons = 1 + (int)(campfireLevel/3f) - mounstersCount;
        return summons;
    }

    public Dictionary<MonsterIds, Monster> GetTasklessMonsters()
    {
        Dictionary<MonsterIds, Monster> activeMonster = GetActiveMonsters();
        Dictionary<MonsterIds, Monster> taskedMonsters = new Dictionary<MonsterIds, Monster>();
        Dictionary<MonsterIds, Monster> taskless = new Dictionary<MonsterIds, Monster>();
        foreach (Building b in BuildingMaster.GetInstance().GetUnlockedBuildings())
        {
            foreach(Task task in b.GetActiveTasks())
            {
                foreach(Monster m in task.GetMonsters())
                {
                    taskedMonsters.Add(m.GetId(), m);
                }
            }
        }
        foreach(Monster m in activeMonster.Values.ToList())
        {
            if (!taskedMonsters.ContainsKey(m.GetId()))
                taskless.Add(m.GetId(), m);
        }
        return taskless;
    }

    public Dictionary<MonsterIds, Monster> GetInactiveMonsters()
    {
        return GetMonsters()
            .Where(m => !activeMonsters.ContainsKey(m.Key))
            .ToDictionary(m => m.Key, m => m.Value);
    }

    // Get up to 3 random not active Monsters  for Drafting
    public List<Monster> GetMonsterDraft()
    {
        int draftSize = 3;
        List<Monster> draft = new List<Monster>();
        List<Monster> inactive = GetInactiveMonsters().Values.ToList();
        List<Monster> shuffledList = GetShuffledList(inactive);
        foreach (Monster m in shuffledList)
        {
            if (draftSize > draft.Count)
                draft.Add(m);
            else
                break;
        }
        return draft;
    }

    // Creates a new randomly ordered list
    protected List<Monster> GetShuffledList(List<Monster> list)
    {
        List<Monster> shuffled = list.ToList();
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            Monster value = shuffled[k];
            shuffled[k] = shuffled[n];
            shuffled[n] = value;
        }
        return shuffled;
    }
    public Monster GetActiveMonster(MonsterIds id)
    {
        if (activeMonsters.ContainsKey(id)) return activeMonsters[id];
        return null;
    }

    public Monster PickMonster(MonsterIds id)
    {
        Monster activatedMonster = database.GetMonster(id);
        if (activatedMonster)
        {
            return InstantiateMonster(activatedMonster);
        }
        return null;
    }
    // Creates the in game instance of the Monster Prefab, which can be edited
    public Monster InstantiateMonster(Monster m)
    {
        Monster instance = Instantiate(m);
        if (activeMonsters.ContainsKey(m.GetId()))
        {
            Monster previousMonster = activeMonsters[m.GetId()];
            activeMonsters.Remove(m.GetId());
            Destroy(previousMonster.gameObject);
        }
        activeMonsters.Add(m.GetId(), instance);
        UITasklessMonsterMaster.GetInstance()?.UpdateTasklessMonsters();
        onMonsterActivated?.Invoke(m);
        return instance;
    }
}
