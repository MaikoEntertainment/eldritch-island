using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMonsterPickerMaster : MonoBehaviour
{
    public static UIMonsterPickerMaster _instance;

    public UIMonsterPickerManager mosnterDraftPickerPrefab;

    private UIMonsterPickerManager pickerInstance;

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

    public static UIMonsterPickerMaster GetInstance() { return _instance; }

    public void ShowMonsterDraft()
    {
        List<Monster> monsterDraft = MonsterMaster.GetInstance().GetMonsterDraft();
        HideMonsterDraft();
        pickerInstance = Instantiate(mosnterDraftPickerPrefab.gameObject, transform).GetComponent<UIMonsterPickerManager>();
        pickerInstance.FillPicks(monsterDraft);
    }

    public void HideMonsterDraft()
    {
        if (pickerInstance)
        {
            Destroy(pickerInstance.gameObject);
        }
    }
}
