using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITaskPickTask : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI text;
    public TextMeshProUGUI progressNeeded;
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
        progressNeeded.text = taskBase.GetProgressNeeded().ToString("F0");
        if (isSelected)
            text.color = selectedColor;
        icon.sprite = taskBase.GetIcon();

        if (taskBase.GetStressChange() < 0)
        {
            stress.text = taskBase.GetStressChange().ToString("F1");
            stress.color = Utils.GetSuccessColor();
        }
        else if (taskBase.GetStressChange() > 0)
        {
            stress.text = "+" + taskBase.GetStressChange().ToString("F1");
            stress.color = Utils.GetWrongColor();
        }
        else
            stress.text = "+0";
        ClearLists();
        foreach (Item i in taskBase.GetItemCost())
        {
            Item inventoryItem = InventoryMaster.GetInstance().GetItem(i.GetId());
            bool hasEnough = inventoryItem != null && inventoryItem.GetAmount() >= i.GetAmount();
            Instantiate(itemPrefab.gameObject, itemList).GetComponent<UIItem>().Load(i, !hasEnough);
        }
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
