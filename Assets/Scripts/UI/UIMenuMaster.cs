using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIMenuMaster : MonoBehaviour
{
    public static UIMenuMaster _instance;
    public Transform menuPanel;

    public UIMenuHandler menuPrefab;

    void Awake()
    {
        if (_instance)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    public static UIMenuMaster GetInstance() { return _instance; }

    public void ShowMenu()
    {
        foreach (Transform t in menuPanel)
            Destroy(t.gameObject);
        Instantiate(menuPrefab.gameObject, menuPanel);
    }
}
