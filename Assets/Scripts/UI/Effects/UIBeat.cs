using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBeat : MonoBehaviour
{
    public Vector2 startingScale = Vector2.one;
    public Vector2 finalScale = Vector2.one;
    public float duration = 1;
    public bool pingPong = true;

    private float direction = 1;
    private float durationPassed = 0;

    void Start()
    {
        
    }

    void Update()
    {
        durationPassed += Time.deltaTime;
        if (direction > 0)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, finalScale, durationPassed / duration);
            if (Vector2.Distance(transform.localScale, finalScale) < 0.001f && pingPong)
            {
                direction *= -1;
                durationPassed = 0;
            }
        }
        else
        {
            transform.localScale = Vector2.Lerp(transform.localScale, startingScale, durationPassed / duration);
            if (Vector2.Distance(transform.localScale, startingScale) < 0.001f && pingPong)
            {
                direction *= -1;
                durationPassed = 0;
            }
        }
    }

    private void OnDisable()
    {
        transform.localScale = startingScale;
    }
}
