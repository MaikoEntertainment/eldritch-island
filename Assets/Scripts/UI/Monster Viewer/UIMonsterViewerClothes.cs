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

    public delegate void Click(Clothes t);
    public event Click onClick;
    public delegate void Close(Clothes t);
    public event Close onClose;

    public UIMonsterViewerClothes Load(Clothes c, Monster m)
    {
        equipedTo = m;
        clothes = c;
        icon.sprite = c.GetIcon();
        uses.text = c.GetDurabilityLeft().ToString();
        tier.text = c.GetTier().ToString();
        return this;
    }
    public void Remove()
    {
        int index = equipedTo.GetClothes().IndexOf(clothes);
        equipedTo.UnEquipClothes(index);
        OnClose();
    }

    public void OnClick()
    {
        onClick?.Invoke(clothes);
    }

    public void OnClose()
    {
        onClose?.Invoke(clothes);
    }
}
