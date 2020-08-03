using System;
using UnityEngine;

[CreateAssetMenu]
public class ItemBaseConsumable : ItemBase
{
    public ItemConsumableEffect effects;
    public override CategoryIds GetCategory()
    {
        return CategoryIds.consumable;
    }

    public override void Use()
    {
        Console.WriteLine("Replace function to include Monster as parameter");
    }
}
