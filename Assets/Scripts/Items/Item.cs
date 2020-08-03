using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected ItemBase itemBase;
    protected long amount;

    public Item(ItemBase itemBase, long amount)
    {
        this.itemBase = itemBase;
        this.amount = amount;
    }
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

    public void Use()
    {
        itemBase.Use();
    }
}
