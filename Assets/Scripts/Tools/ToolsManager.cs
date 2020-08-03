using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsManager
{
    protected List<Tool> tools;
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
