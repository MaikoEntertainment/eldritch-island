using System;
using UnityEngine;


public class ItemBaseUsable : ItemBase
{
    public override CategoryIds GetCategory()
    {
        return CategoryIds.usable;
    }

    public override void Use()
    {
        Console.WriteLine("");
    }
}
