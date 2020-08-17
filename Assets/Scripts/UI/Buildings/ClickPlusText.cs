using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickPlusText : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Load(string text)
    {
        this.text.text = text;
        CanvasGroup cg = GetComponent<CanvasGroup>();
        if (cg)
        {
            StartCoroutine(Fade.FadeCanvas(cg, 1, 0, 0.5f));
        }
        Destroy(gameObject, 0.55f);
    }
}
