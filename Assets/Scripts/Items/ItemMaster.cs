using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMaster : MonoBehaviour
{
    private static ItemMaster _instance;

    [SerializeField]
    protected ItemDatabase database;
    
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    public static ItemMaster GetInstance() { return _instance; }

    public Dictionary<int, ItemBase> GetItems()
    {
        return database.GetItems();
    }

    public ItemBase GetItem(int id)
    {
        return database.GetItem(id);
    }
}
