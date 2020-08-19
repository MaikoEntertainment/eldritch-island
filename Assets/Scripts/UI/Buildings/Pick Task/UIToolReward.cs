using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIToolReward : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI durability;
    
    public void Load(ToolBase t)
    {
        icon.sprite = t.GetIcon();
        durability.text = Utils.ToFormat(t.GetDurability());
    }
}
