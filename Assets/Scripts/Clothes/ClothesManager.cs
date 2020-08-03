using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesManager
{
    protected List<Clothes> clothes;
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
