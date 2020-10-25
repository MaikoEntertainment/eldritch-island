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

    private Tool tool;

    public delegate void Click(Tool t);
    public event Click onClick;

    public UITaskMonsterTool Load(Tool t)
    {
        tool = t;
        icon.sprite = t.GetIcon();
        uses.text = t.GetDurabilityLeft().ToString("F0");
        tier.text = t.GetTier().ToString();
        return this;
    }

    public void OnClick()
    {
        onClick?.Invoke(tool);
    }
}
