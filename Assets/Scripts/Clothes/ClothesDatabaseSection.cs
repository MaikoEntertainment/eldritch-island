using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ClothesDatabaseSection
{
    [SerializeField]
    protected string category = "";
    [SerializeField]
    protected List<ClothesBase> clothes;

    public ClothesBase GetClothes(int id)
    {
        foreach (ClothesBase ib in clothes)
        {
            if (ib.GetId() == id)
                return ib;
        }
        return null;
    }

    public List<ClothesBase> GetClothes()
    {
        return clothes;
    }
}
