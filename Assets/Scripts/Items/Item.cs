using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Item
{
    [SerializeField]
    protected ItemBase itemBase;
    [SerializeField]
    protected long amount;

    public Item(ItemBase itemBase, long amount)
    {
        this.itemBase = itemBase;
        this.amount = amount;
    }

    public Item Clone(Item i)
    {
        return new Item(i.GetItemBase(), i.GetAmount());
    }

    public ItemBase GetItemBase() { return itemBase; }
    public int GetId()
    {
        return itemBase.GetId();
    }

    public long GetAmount()
    {
        return amount;
    }

    public long ChangeAmount(long change)
    {
        amount += change;
        return amount;
    }

    public CategoryIds GetCategory()
    {
        return itemBase.GetCategory();
    }

    public Sprite GetIcon()
    {
        return itemBase.GetIcon();
    }

    public void Use()
    {
        itemBase.Use();
    }
}
