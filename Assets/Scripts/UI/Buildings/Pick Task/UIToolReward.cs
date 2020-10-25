using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIToolReward : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI durability;
    public Image color;
    
    public void Load(ToolBase t)
    {
        icon.sprite = t.GetIcon();
        durability.text = Utils.ToFormat(t.GetDurability());
        if (t is ClothesBase)
            color.color = Utils.GetClothesColor();
    }
}
