using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ClothesDatabase : ScriptableObject
{
    [SerializeField]
    protected List<ClothesDatabaseSection> sections = new List<ClothesDatabaseSection>();
    protected Dictionary<int, ClothesBase> clothesDictionary = new Dictionary<int, ClothesBase>();

    public ClothesDatabase()
    {
        InitializeDictionary();
    }

    protected void InitializeDictionary()
    {
        foreach (ClothesDatabaseSection section in sections)
        {
            foreach (ClothesBase ib in section.GetClothes())
            {
                clothesDictionary.Add(ib.GetId(), ib);
            }
        }
    }

    public Dictionary<int, ClothesBase> GetClothesList()
    {
        return clothesDictionary;
    }

    public ClothesBase GetClothes(int id)
    {
        if (clothesDictionary.ContainsKey(id)){
            return clothesDictionary[id];
        }
        return null;
    }
}
