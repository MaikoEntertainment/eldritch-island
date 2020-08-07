using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonUtils : MonoBehaviour
{
    public void OpenMonsterDraft()
    {
        List<Monster> monsters = MonsterMaster.GetInstance().GetMonsterDraft();
        UIMonsterPickerMaster.GetInstance().ShowMonsterDraft(monsters);
    }

    public void CloseMonsterDraft()
    {
        UIMonsterPickerMaster.GetInstance().HideMonsterDraft();
    }
}
