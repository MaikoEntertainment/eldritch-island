using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITasklessMonster : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI text;

    private Monster m;

    public void Load(Monster m)
    {
        this.m = m;
        float stress = m.GetStress();
        float max = m.GetStressMax();
        text.text = stress.ToString("F1") + "/" + max;
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

    public void OpenDetails()
    {
        UIMonsterViewerMaster.GetInstance().Load(m);
    }

}
