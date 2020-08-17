using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ClothesManager
{
    protected List<Clothes> clothes = new List<Clothes>();
    [SerializeField]
    protected int slots = 1;

    public int GetClothesSlots() { return slots; }
    public List<Clothes> GetEquippedClothes() { return clothes; }
    public bool EquipClothes(Clothes clothes)
    {
        if (HasFreeSlots())
        {
            this.clothes.Add(clothes);
            return true;
        }
        return false;
    }
    public bool UnEquipTool(int index)
    {
        if (index >= clothes.Count) return false;
        clothes.RemoveAt(index);
        return true;
    }
    public bool HasFreeSlots()
    {
        return slots > clothes.Count;
    }

    public void UseClothes()
    {
        Console.WriteLine("Add Task to parameters");
        foreach (Clothes c in clothes)
        {
            c.Use();
        }
    }
}
