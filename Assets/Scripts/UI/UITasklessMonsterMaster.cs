using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UITasklessMonsterMaster : MonoBehaviour
{
    public static UITasklessMonsterMaster _instance;

    public UITasklessMonster tasklessMonsterPrefab;

    private void Awake()
    {
        if (_instance)
            Destroy(this);
        else
        {
            _instance = this;
        }
    }

    public static UITasklessMonsterMaster GetInstance() { return _instance; }
    public void UpdateTasklessMonsters()
    {
        List<Monster> monsters = MonsterMaster.GetInstance().GetTasklessMonsters().Values.ToList();
        ClearMonsters();
        foreach (Monster monster in monsters)
        {
            Instantiate(tasklessMonsterPrefab.gameObject, transform).GetComponent<UITasklessMonster>().Load(monster);
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
