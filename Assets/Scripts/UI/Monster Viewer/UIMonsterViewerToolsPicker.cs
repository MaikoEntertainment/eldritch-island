using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMonsterViewerToolsPicker : MonoBehaviour
{
    public Transform toolList;
    public GameObject toolViewer;

    public TextMeshProUGUI toolName;
    public TextMeshProUGUI durability;
    public TextMeshProUGUI level;
    public TextMeshProUGUI description;
    public Transform skillBonusList;

    public UIMonsterViewerSkillBonus skillPrefab;
    public UITaskMonsterTool toolPrefab;

    private Tool tool;
    private Monster currentMonster;

    public void Load(Monster m)
    {
        currentMonster = m;
        LoadToolList();
    }

    public void LoadToolList()
    {
        List<Tool> tools = InventoryMaster.GetInstance().GetTools();
        foreach (Transform t in toolList)
        {
            Destroy(t.gameObject);
        }
        foreach (Tool t in tools)
        {
            UITaskMonsterTool uiTool = Instantiate(toolPrefab.gameObject, toolList).GetComponent<UITaskMonsterTool>().Load(t);
            uiTool.onClick += ViewTool;
        }
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    public void ViewTool(Tool t)
    {
        tool = t;
        toolViewer.SetActive(true);
        toolName.text = t.GetToolBase().GetName();
        description.text = t.GetToolBase().GetDescription();
        durability.text = t.GetDurabilityLeft().ToString();
        level.text = t.GetTier().ToString();
        foreach (Transform skill in skillBonusList)
            Destroy(skill.gameObject);
        foreach(SkillBonus s in tool.GetSkillBonuses())
        {
            Instantiate(skillPrefab.gameObject, skillBonusList).GetComponent<UIMonsterViewerSkillBonus>().Load(s);
        }
    }

    public void CloseViewTool()
    {
        toolViewer.SetActive(false);
    }

    public void Equip()
    {
        currentMonster.EquipTool(tool);
        CloseViewTool();
        LoadToolList();
        UIMonsterViewerMaster.GetInstance().Load(currentMonster);
    }

    public void Remove()
    {
        InventoryMaster.GetInstance().RemoveTool(tool);
        CloseViewTool();
        LoadToolList();
    }

    private void OnDisable()
    {
        
    }
}
