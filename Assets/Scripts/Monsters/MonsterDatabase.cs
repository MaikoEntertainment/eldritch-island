using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class MonsterDatabase : ScriptableObject
{
    protected List<Monster> monsters = new List<Monster>();
    protected Dictionary<MonsterIds, Monster> monsterDictionary = new Dictionary<MonsterIds, Monster>();

    public MonsterDatabase()
    {
        InitializeDictionary();
    }

    protected void InitializeDictionary()
    {
        // The dictionary has faster load times
        foreach (Monster mon in monsters)
        {
            monsterDictionary.Add(mon.GetId(), mon);
        }
    }

    public Dictionary<MonsterIds, Monster> GetMonsters() { return monsterDictionary; }
    public Monster GetMonster(MonsterIds id)
    {
        foreach (Monster mon in monsters)
        {
            if (mon.GetId() == id) return mon;
        }
        return null;
    }
    
    
   
}
