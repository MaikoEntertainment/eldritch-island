using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UILetterList : MonoBehaviour
{
    public Transform letterList;
    public UILetterItem letterPrefab;
    public void Load()
    {
        List<Letter> list = LetterMaster.GetInstance().GetUnlockedLetters().Values.ToList();
        foreach (Letter l in list)
            Instantiate(letterPrefab.gameObject, letterList).GetComponent<UILetterItem>().Load(l);
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
