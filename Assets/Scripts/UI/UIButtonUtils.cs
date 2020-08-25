using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonUtils : MonoBehaviour
{
    public void OpenMonsterDraft()
    {
        UIMonsterPickerMaster.GetInstance().ShowMonsterDraft();
    }

    public void CloseMonsterDraft()
    {
        UIMonsterPickerMaster.GetInstance().HideMonsterDraft();
    }
}
