using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsMaster : MonoBehaviour
{
    private static ToolsMaster _instance;

    [SerializeField]
    protected ToolsDatabase database;

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

    public static ToolsMaster GetInstance() { return _instance; }

    public Dictionary<int, ToolBase> GetToolsList()
    {
        return database.GetTools();
    }

    public ToolBase GetTool(int id)
    {
        return database.GetTool(id);
    }
}
