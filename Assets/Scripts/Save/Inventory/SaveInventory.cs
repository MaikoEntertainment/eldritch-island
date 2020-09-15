using System;
using System.Collections.Generic;
[Serializable]
public class SaveInventory
{
    public List<SaveItem> savedItems = new List<SaveItem>();
    public List<SaveTool> savedTools = new List<SaveTool>();
    public List<SaveClothes> savedClothes = new List<SaveClothes>();

    public void SaveItems(List<Item> items)
    {
        savedItems.Clear();
        foreach (Item i in items)
        {
            savedItems.Add(new SaveItem(i));
        }
    }
    public void SaveTools(List<Tool> tools)
    {
        savedTools.Clear();
        foreach (Tool t in tools)
        {
            savedTools.Add(new SaveTool(t));
        }
    }
    public void SaveClothes(List<Clothes> clothes)
    {
        savedClothes.Clear();
        foreach (Clothes c in clothes)
        {
            savedClothes.Add(new SaveClothes(c));
        }
    }
}
