using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Letters/Letter")]
public class Letter : ScriptableObject
{
    public UILetter uiPrefab;
    [SerializeField]
    protected LetterId id = 0;
    [SerializeField]
    protected TextLanguageOwn title;

    public UILetter GetUIPrefab()
    {
        return uiPrefab;
    }

    public LetterId GetId() { return id; }
    public string GetTitle() { return title.GetText(); }
}
