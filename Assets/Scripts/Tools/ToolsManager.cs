using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ToolsManager
{
    protected List<Tool> tools = new List<Tool>();
    [SerializeField]
    protected int slots = 1;

    public int GetToolSlots() { return slots; }
    public List<Tool> GetEquippedTools() { return tools; }
    public bool EquipTool(Tool tool)
    {
        if (HasFreeSlots())
        {
            tools.Add(tool);
            InventoryMaster.GetInstance().RemoveTool(tool);
            return true;
        }
        return false;
    }
    public bool UnEquipTool(int index)
    {
        if (index >= tools.Count || index < 0) return false;
        InventoryMaster.GetInstance().AddTool(tools[index]);
        tools.RemoveAt(index);
        return true;
    }

    public bool HasFreeSlots()
    {
        return slots > tools.Count;
    }

    public void UseTools()
    {
        Console.WriteLine("Add Task to parameters");
        List<Tool> toolsWithDurabiliy = new List<Tool>();
        foreach (Tool t in tools)
        {
            bool broke = t.Use();
            if (!broke)
                toolsWithDurabiliy.Add(t);
        }
        tools = toolsWithDurabiliy;
    }
}
