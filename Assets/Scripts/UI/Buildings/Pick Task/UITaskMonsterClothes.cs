using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITaskMonsterClothes : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI uses;
    public TextMeshProUGUI tier;

    private Clothes clothes;

    public delegate void Click(Clothes t);
    public event Click onClick;

    public UITaskMonsterClothes Load(Clothes t)
    {
        clothes = t;
        icon.sprite = t.GetIcon();
        uses.text = t.GetDurabilityLeft().ToString("F0");
        tier.text = t.GetTier().ToString();
        return this;
    }

    public void OnClick()
    {
        onClick?.Invoke(clothes);
    }
}
