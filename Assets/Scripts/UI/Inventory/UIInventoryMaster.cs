using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIInventoryMaster : MonoBehaviour
{
    public static UIInventoryMaster _instance;

    public Transform itemBarList;

    public UIInventoryBarItem inventoryItemPrefab;

    private Dictionary<int, Item> itemsChache = new Dictionary<int, Item>();

    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        UpdateItembar();
        InventoryMaster.GetInstance().OnNewItem += AddNewItemTobar;
    }

    public void UpdateItembar()
    {
        foreach(Item i in InventoryMaster.GetInstance().GetItems().Values.ToList())
        {
            if (!itemsChache.ContainsKey(i.GetId()))
                AddNewItemTobar(i);
        }
    }

    public void AddNewItemTobar(Item i)
    {
        itemsChache.Add(i.GetId(), i);
        Instantiate(inventoryItemPrefab.gameObject, itemBarList).GetComponent<UIInventoryBarItem>().Load(i);
    }

}
