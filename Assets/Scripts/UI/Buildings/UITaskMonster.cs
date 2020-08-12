using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITaskMonster : MonoBehaviour
{
    public Image icon;

    public void Load(Monster m)
    {
        icon.sprite = m.GetIcon();
    }
}
