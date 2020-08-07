using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMonsterPickerManager : MonoBehaviour
{
    public Transform monsterList;

    public UIMonsterPick prefab;

    public void FillPicks(List<Monster> picks)
    {
        foreach (Transform child in monsterList)
        {
            Destroy(child.gameObject);
        }
        foreach(Monster m in picks)
        {
            UIMonsterPick uimp = Instantiate(prefab.gameObject, monsterList).GetComponent<UIMonsterPick>();
            uimp.Load(m);
        }
    }
}
