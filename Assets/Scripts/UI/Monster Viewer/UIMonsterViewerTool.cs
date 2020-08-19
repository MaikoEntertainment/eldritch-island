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

    public delegate void Click(Tool t);
    public event Click onClick;
    public delegate void Close(Tool t);
    public event Close onClose;

    public UIMonsterViewerTool Load(Tool t, Monster m)
    {
        equipedTo = m;
        tool = t;
        icon.sprite = t.GetIcon();
        uses.text = t.GetDurabilityLeft().ToString();
        tier.text = t.GetTier().ToString();
        return this;
    }

    public void Remove()
    {
        int index = equipedTo.GetTools().IndexOf(tool);
        equipedTo.UnEquipTool(index);
        OnClose();
    }

    public void OnClick()
    {
        onClick?.Invoke(tool);
    }

    public void OnClose()
    {
        onClose?.Invoke(tool);
    }
}
