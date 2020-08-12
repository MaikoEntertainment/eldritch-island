using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemReward : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amountRange;
    public TextMeshProUGUI chance;
    
    public void Load(ItemReward ir)
    {
        icon.sprite = ir.GetItem().GetIcon();
        if (ir.GetExtraRange() != 0)
        {
            int maxValue = ir.GetMinAmount() + ir.GetExtraRange();
            amountRange.text = ir.GetMinAmount() + "-" + maxValue;
        }
        else
            amountRange.text = ir.GetMinAmount().ToString();
        if (ir.GetRewardChance() < 1)
        {
            chance.text = (ir.GetRewardChance() * 100) + "%";
        }
        else
            chance.text = "";
    }
}
