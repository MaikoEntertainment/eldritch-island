using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UITasklessMonsterMaster : MonoBehaviour
{
    public static UITasklessMonsterMaster _instance;

    public UITasklessMonster tasklessMonsterPrefab;

    private Dictionary<MonsterIds, UITasklessMonster> tasklessCache = new Dictionary<MonsterIds, UITasklessMonster>();

    private void Awake()
    {
        if (_instance)
            Destroy(_instance);
       _instance = this;
        tasklessCache = new Dictionary<MonsterIds, UITasklessMonster>();
    }

    private void Start()
    {
        UpdateTasklessMonsters();
    }

    private void OnDisable()
    {
        _instance = null;
    }

    public static UITasklessMonsterMaster GetInstance() { return _instance; }
    public void UpdateTasklessMonsters()
    {
        Dictionary<MonsterIds, Monster> tasklessMonsters = MonsterMaster.GetInstance().GetTasklessMonsters();
        foreach (MonsterIds mId in tasklessCache.Keys.ToList())
        {
            if (!tasklessMonsters.ContainsKey(mId) || !tasklessCache[mId])
            {
                if (tasklessCache[mId])
                    Destroy(tasklessCache[mId].gameObject);
                tasklessCache.Remove(mId);
            }
        }
        foreach (Monster monster in tasklessMonsters.Values.ToList())
        {
            if (!tasklessCache.ContainsKey(monster.GetId()))
            {
                UITasklessMonster taskless = Instantiate(tasklessMonsterPrefab.gameObject, transform).GetComponent<UITasklessMonster>();
                taskless.Load(monster);
                tasklessCache.Add(monster.GetId(), taskless);
            }
            else
            {
                tasklessCache[monster.GetId()].Load(monster);
            }
        }
    }

    public void ClearMonsters()
    {
        foreach (Transform previousMonsters in transform)
        {
            Destroy(previousMonsters.gameObject);
        }
    }

}
