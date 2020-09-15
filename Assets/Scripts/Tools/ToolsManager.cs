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

    public void Load(List<SaveTool> savedTools)
    {
        foreach(SaveTool st in savedTools)
        {
            ToolBase tb = ToolsMaster.GetInstance().GetTool(st.GetId());
            if (tb)
                tools.Add(new Tool(tb, st.GetDurabilityUsed(), st.GetTier()));
        }
    }

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

    public void UseTools(Monster m, Task t)
    {
        Console.WriteLine("Add Task to parameters");
        List<Tool> toolsWithDurabiliy = new List<Tool>();
        foreach (Tool to in tools)
        {
            bool broke = to.Use(m,t);
            if (!broke)
                toolsWithDurabiliy.Add(to);
        }
        tools = toolsWithDurabiliy;
    }
}
