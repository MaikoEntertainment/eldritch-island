using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeOut : MonoBehaviour
{
    public float time = 1;
    public bool destroyAfterTime = false;

    private void Start()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        if (!cg)
            cg = gameObject.AddComponent<CanvasGroup>();
        StartCoroutine(Fade.FadeCanvas(cg, 1, 0, time));
        if (destroyAfterTime)
            Destroy(gameObject, time + 0.1f);
    }
}
