using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMonsterViewerMaster : MonoBehaviour
{
    public static UIMonsterViewerMaster _instance;
    public Transform monsterViewer;

    public UIMonsterViewerHandler monsterViewerPrefab;

    private void Awake()
    {
        if (_instance)
            Destroy(this);
        else
        {
            _instance = this;
        }
    }

    public static UIMonsterViewerMaster GetInstance() { return _instance; }

    public void Load(Monster m)
    {
        Close();
        Instantiate(monsterViewerPrefab.gameObject, monsterViewer).GetComponent<UIMonsterViewerHandler>().Load(m);
    }

    public void Close()
    {
        foreach (Transform t in monsterViewer)
            Destroy(t.gameObject);
    }
}
