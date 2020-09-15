using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SaveItem
{
    public int itemId;
    public long amount;

    public SaveItem(Item i)
    {
        itemId = i.GetId();
        amount = i.GetAmount();
    }

    public int GetId() { return itemId; }
    public long GetAmount() { return amount; }
}
