using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadein : MonoBehaviour
{
    public float time = 1;

    private void Start()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        if (!cg)
            cg = gameObject.AddComponent<CanvasGroup>();
        StartCoroutine(Fade.FadeCanvas(cg, 0, 1, time));
    }
}
