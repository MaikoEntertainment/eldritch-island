using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITaskLocked : MonoBehaviour
{
    public TextMeshProUGUI condition;

    public void Load(TaskBase task)
    {
        condition.text = task.GetUnlockCondition();
    }
}
