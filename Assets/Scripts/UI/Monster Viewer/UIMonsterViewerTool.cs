using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMonsterViewerTool : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI uses;
    public TextMeshProUGUI tier;

    protected Monster equipedTo;
    protected Tool tool;

    public void Load(Tool t, Monster m)
    {
        equipedTo = m;
        tool = t;
        icon.sprite = t.GetIcon();
        uses.text = t.GetDurabilityLeft().ToString();
        tier.text = t.GetTier().ToString();
    }

    public void Remove()
    {
        int index = equipedTo.GetTools().IndexOf(tool);
        equipedTo.UnEquipTool(index);
    }
}
