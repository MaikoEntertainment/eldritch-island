using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITasklessMonster : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI text;
    public Transform equipmentList;

    public UITaskMonsterTool toolPrefab;
    public UITaskMonsterClothes clothesPrefab;

    private Monster m;

    public void Load(Monster m)
    {
        this.m = m;
        float stress = m.GetStress();
        float max = m.GetStressMax();
        text.text = stress.ToString("F1") + "/" + max;
        icon.sprite = m.GetIcon();
        if (equipmentList)
        {
            foreach (Transform t in equipmentList)
                Destroy(t.gameObject);
            foreach(Tool t in m.GetTools())
            {
                Instantiate(toolPrefab, equipmentList).GetComponent<UITaskMonsterTool>().Load(t);
            }
            foreach (Clothes c in m.GetClothes())
            {
                Instantiate(clothesPrefab, equipmentList).GetComponent<UITaskMonsterClothes>().Load(c);
            }
        }
        if (m.isOverStressed())
        {
            text.color = Utils.GetWrongColor();
        }
        else
        {
            text.color = Color.white;
        }
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
