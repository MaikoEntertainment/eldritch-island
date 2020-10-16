using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITaskLocked : MonoBehaviour
{
    public TextMeshProUGUI condition;
    public TextMeshProUGUI taskName;

    public void Load(TaskBase task)
    {
        condition.text = task.GetUnlockCondition();
        taskName.text = task.GetName();
    }
}
