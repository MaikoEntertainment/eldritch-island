using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIError : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Load(string text)
    {
        this.text.text = text;
    }
}
