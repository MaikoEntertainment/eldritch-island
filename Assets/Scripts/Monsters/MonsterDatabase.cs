using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MonsterDatabase : ScriptableObject
{
    [SerializeField]
    protected List<Monster> monsters;
    protected Dictionary<MonsterIds, Monster> monsterDictionary = new Dictionary<MonsterIds, Monster>();

    public void InitializeDictionary()
    {
        // The dictionary has faster load times
        foreach (Monster mon in monsters)
        {
            if (!monsterDictionary.ContainsKey(mon.GetId()))
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
