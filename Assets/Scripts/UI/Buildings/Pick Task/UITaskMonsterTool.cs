using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITaskMonsterTool : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI uses;
    public TextMeshProUGUI tier;

    public void Load(Tool t)
    {
        icon.sprite = t.GetIcon();
        uses.text = t.GetDurabilityLeft().ToString();
        tier.text = t.GetTier().ToString();
    }
}
