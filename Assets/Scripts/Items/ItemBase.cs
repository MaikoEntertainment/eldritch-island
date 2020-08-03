using System;
using UnityEngine;

[CreateAssetMenu]
public class ItemBase: ScriptableObject
{
    [SerializeField]
    protected int id;
    [SerializeField]
    protected Sprite icon;
    [SerializeField]
    protected TextLanguageOwn name;
    [SerializeField]
    protected TextLanguageOwn description;
    [SerializeField]
    protected Tag[] tags;

    public int GetId() { return id; }
    public virtual CategoryIds GetCategory()
    {
        return CategoryIds.basic;
    }
    public virtual void Use()
    {
        Console.WriteLine("Basic Items can't be used, only consumed in crafting/task requirements");
    }
    public Sprite GetIcon() { return icon; }
    public string GetName() { return name.GetText(); }
    public string GetDescription() { return description.GetText(); }
}
