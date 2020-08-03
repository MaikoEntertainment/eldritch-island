using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ToolsDatabaseSection
{
    [SerializeField]
    protected string category = "";
    [SerializeField]
    protected List<ToolBase> tools;

    public ToolBase GetTool(int id)
    {
        foreach (ToolBase ib in tools)
        {
            if (ib.GetId() == id)
                return ib;
        }
        return null;
    }

    public List<ToolBase> GetTools()
    {
        return tools;
    }
}
