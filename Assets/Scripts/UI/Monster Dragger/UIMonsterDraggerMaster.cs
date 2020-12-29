using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMonsterDraggerMaster : MonoBehaviour
{
    public static UIMonsterDraggerMaster instance;

    public UIMonsterDragger monsterDraggerPrefab;

    private void Awake()
    {
        if (instance)
        {
            Destroy(instance);
        }
        instance = this;
    }

    public static UIMonsterDraggerMaster GetInstance() { return instance; }

    public void CreateMonsterDragger(Monster m, Task task=null)
    {
        Instantiate(monsterDraggerPrefab.gameObject, transform).GetComponent<UIMonsterDragger>().Load(m, task);
    }
}
