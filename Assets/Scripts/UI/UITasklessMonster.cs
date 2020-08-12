using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITasklessMonster : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI text;

    public void Load(Monster m)
    {
        float stress = m.GetStress();
        float max = m.GetStressMax();
        text.text = stress + "/" + max;
        icon.sprite = m.GetIcon();
    }

    private void OnEnable()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        if (cg)
        {
            StartCoroutine(Fade.FadeCanvas(cg, 0, 1, 1));
        }
    }

}
