using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeOut : MonoBehaviour
{
    public float time = 1;
    public float delay = 0;
    public bool destroyAfterTime = false;

    private bool started = false;

    private void Update()
    {
        if (!started)
        {
            delay -= Time.deltaTime;
            if (delay < 0)
                BeginFade();
        }
    }

    public void BeginFade()
    {
        if (!started)
        {
            started = true;
            CanvasGroup cg = GetComponent<CanvasGroup>();
            if (!cg)
                cg = gameObject.AddComponent<CanvasGroup>();
            StartCoroutine(Fade.FadeCanvas(cg, 1, 0, time));
            if (destroyAfterTime)
                Destroy(gameObject, time + 0.1f);
        }
    }
}
