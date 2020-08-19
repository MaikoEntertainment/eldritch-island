using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITaskPickTask : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI text;
    public TextMeshProUGUI stress;
    public Transform itemList;
    public Transform itemMonsterList;
    public Transform resultList;
    public Color selectedColor;

    public UIItem itemPrefab;
    public UIItemReward itemRewardPrefab;
    public UIToolReward toolPrefab;
    private TaskBase task;

    public void Load(TaskBase taskBase, bool isSelected=false)
    {
        task = taskBase;
        text.text = taskBase.GetName();
        if (isSelected)
            text.color = selectedColor;
        icon.sprite = taskBase.GetIcon();
        stress.text = (taskBase.GetStressChange() >= 0 ? "+" : "") + taskBase.GetStressChange().ToString();
        ClearLists();
        foreach (Item i in taskBase.GetItemCost())
            Instantiate(itemPrefab.gameObject, itemList).GetComponent<UIItem>().Load(i);
        foreach (Item i in taskBase.GetCostPerMonster())
            Instantiate(itemPrefab.gameObject, itemMonsterList).GetComponent<UIItem>().Load(i);
        foreach (ItemReward i in taskBase.GetItemRewards())
            Instantiate(itemRewardPrefab.gameObject, resultList).GetComponent<UIItemReward>().Load(i);
        foreach (ToolBase t in taskBase.GetToolRewards())
            Instantiate(toolPrefab.gameObject, resultList).GetComponent<UIToolReward>().Load(t);
        foreach (ClothesBase c in taskBase.GetClotheRewards())
            Instantiate(toolPrefab.gameObject, resultList).GetComponent<UIToolReward>().Load(c);
    }

    public void ClearLists()
    {
        foreach (Transform child in itemList)
            Destroy(child);
        foreach (Transform child in itemMonsterList)
            Destroy(child);
        foreach (Transform child in resultList)
            Destroy(child);
    }

    public void OpenTask()
    {
        UIBuildingMaster.GetInstance().ViewTaskDetails(task);
    }
}
