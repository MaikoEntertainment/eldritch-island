using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITaskMonster : MonoBehaviour
{
    public Image icon;
    public Image stressBar;
    public Image overStressBar;

    private Monster m;

    public delegate void Drag(Monster m);
    public event Drag OnDrag;

    public void Load(Monster m)
    {
        this.m = m;
        icon.sprite = m.GetIcon();
        UpdateStressBar(m);
        m.onStressChange += UpdateStressBar;
    }

    public void UpdateStressBar(Monster m)
    {
        stressBar.fillAmount = m.GetStress() / m.GetSaneStressMax();
        overStressBar.fillAmount = (m.GetStress() - m.GetSaneStressMax()) / m.GetStressTreshold();
    }

    public void OnDisable()
    {
        m.onStressChange -= UpdateStressBar;
    }

    public void OnDetectDrag()
    {
        OnDrag?.Invoke(m);
    }
}
