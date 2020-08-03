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

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        // Load from save active Monsters with skill levels, tool, clothes, etc

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
        List<Monster> shuffledList = GetShuffledList(GetInactiveMonsters().Values.ToList());
        foreach (Monster m in shuffledList)
        {
            if (draftSize < draft.Count)
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
    public void PickMonster(MonsterIds id)
    {
        Monster activatedMonster = database.GetMonster(id);
        if (activatedMonster)
        {
            InstantiateMonster(activatedMonster);
        }
    }
    // Creates the in game instance of the Monster Prefab, which can be edited
    public void InstantiateMonster(Monster m)
    {
        Monster instance = Instantiate(m);
        if (activeMonsters.ContainsKey(m.GetId()))
        {
            Monster previousMonster = activeMonsters[m.GetId()];
            activeMonsters.Remove(m.GetId());
            Destroy(previousMonster.gameObject);
        }
        activeMonsters.Add(m.GetId(), instance);
    }
}
