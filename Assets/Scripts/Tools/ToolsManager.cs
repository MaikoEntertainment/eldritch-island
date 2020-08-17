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
            return true;
        }
        return false;
    }
    public bool UnEquipTool(int index)
    {
        if (index >= tools.Count) return false;
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
        foreach(Tool t in tools)
        {
            t.Use();
        }
    }
}
