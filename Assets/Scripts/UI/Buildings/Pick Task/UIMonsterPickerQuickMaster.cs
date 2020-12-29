using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMonsterPickerQuickMaster : MonoBehaviour
{
    public static UIMonsterPickerQuickMaster _instance;

    public UITaskMonsterPickerQuick pickerPrefab;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
        }
        _instance = this;
    }

    public static UIMonsterPickerQuickMaster GetInstance()
    {
        return _instance;
    }

    public void ShowMonsterPickerQuick(Task task)
    {
        Instantiate(pickerPrefab.gameObject, transform).GetComponent<UITaskMonsterPickerQuick>().Load(task);
    }
}
