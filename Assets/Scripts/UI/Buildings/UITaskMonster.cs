﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITaskMonster : MonoBehaviour
{
    public Image icon;
    public Image stressBar;

    private Monster m;

    public void Load(Monster m)
    {
        this.m = m;
        icon.sprite = m.GetIcon();
        UpdateStressBar(m);
        m.onStressChange += UpdateStressBar;
    }

    public void UpdateStressBar(Monster m)
    {
        stressBar.fillAmount = m.GetStress() / m.GetStressMax();
    }

    public void OnDisable()
    {
        m.onStressChange -= UpdateStressBar;
    }
}
