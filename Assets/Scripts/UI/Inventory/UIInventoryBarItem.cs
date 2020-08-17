using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryBarItem : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amount;
    public Transform changeArea;

    public UIInventoryChange changePrefab;

    private Item i;

    public void Load(Item i)
    {
        this.i = i;
        icon.sprite = i.GetIcon();
        amount.text = Utils.ToFormat(i.GetAmount());
        i.onAmountChange += UpdateAmount;
        CanvasGroup canvas = GetComponent<CanvasGroup>();
        if (canvas)
            StartCoroutine(Fade.FadeCanvas(canvas, 0, 1, 1));
    }

    public void UpdateAmount(Item i, long change)
    {
        amount.text = Utils.ToFormat(i.GetAmount());
        if (changeArea && changePrefab)
            Instantiate(changePrefab.gameObject, changeArea).GetComponent<UIInventoryChange>().Load(change);
    }

    private void OnDisable()
    {
        i.onAmountChange -= UpdateAmount;
    }



}
