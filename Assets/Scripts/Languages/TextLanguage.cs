using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLanguage : ScriptableObject
{
    public TextLanguageOwn text;

    public string GetText()
    {
        return text.GetText();
    }
}
