using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToolReward : MonoBehaviour
{
    public Image icon;
    
    public void Load(ToolBase t)
    {
        icon.sprite = t.GetIcon();
    }
}
