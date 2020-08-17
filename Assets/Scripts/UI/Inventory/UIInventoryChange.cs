using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventoryChange : MonoBehaviour
{
    public TextMeshProUGUI change;

    public float duration = 1;
    public Color positive = Color.green;
    public Color negative = Color.red;

    public void Load(long change)
    {
        this.change.text = (change >= 0 ? "+": "") + change;
        this.change.color = change < 0 ? negative : positive;
        CanvasGroup canvas = GetComponent<CanvasGroup>();
        if (canvas)
            StartCoroutine(Fade.FadeCanvas(canvas, 1, 0, duration));
        Destroy(gameObject, duration+0.1f);
    }
}
