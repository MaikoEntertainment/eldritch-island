using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesMaster : MonoBehaviour
{
    private static ClothesMaster _instance;

    [SerializeField]
    protected ClothesDatabase database;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            database.InitializeDictionary();
        }
        else
        {
            Destroy(this);
        }
    }

    public static ClothesMaster GetInstance() { return _instance; }

    public Dictionary<int,ClothesBase> GetClothesList()
    {
        return database.GetClothesList();
    }

    public ClothesBase GetClothes(int id)
    {
        return database.GetClothes(id);
    }
}
