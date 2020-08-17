using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMonsterViewerClothes : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI uses;
    public TextMeshProUGUI tier;

    protected Monster equipedTo;
    protected Clothes clothes;

    public void Load(Clothes c, Monster m)
    {
        equipedTo = m;
        clothes = c;
        icon.sprite = c.GetIcon();
        uses.text = c.GetDurabilityLeft().ToString();
        tier.text = c.GetTier().ToString();
    }
    public void Remove()
    {
        int index = equipedTo.GetClothes().IndexOf(clothes);
        equipedTo.UnEquipTool(index);
    }
}
