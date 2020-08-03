using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ToolsDatabase : ScriptableObject
{
    [SerializeField]
    protected List<ToolsDatabaseSection> sections = new List<ToolsDatabaseSection>();
    protected Dictionary<int, ToolBase> toolsDictionary = new Dictionary<int, ToolBase>();

    public ToolsDatabase()
    {
        InitializeDictionary();
    }

    protected void InitializeDictionary()
    {
        foreach (ToolsDatabaseSection section in sections)
        {
            foreach (ToolBase ib in section.GetTools())
            {
                toolsDictionary.Add(ib.GetId(), ib);
            }
        }
    }

    public Dictionary<int, ToolBase> GetTools()
    {
        return toolsDictionary;
    }

    public ToolBase GetTool(int id)
    {
        if (toolsDictionary.ContainsKey(id))
        {
            return toolsDictionary[id];
        }
        return null;
    }
}
