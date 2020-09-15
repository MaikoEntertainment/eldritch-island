using System;
[Serializable]
public class SaveTool
{
    public int toolId;
    public double durabilityUsed;
    public int tier;

    public SaveTool(Tool t)
    {
        toolId = t.GetId();
        durabilityUsed = t.GetDurability() - t.GetDurabilityLeft();
        tier = t.GetTier();
    }

    public int GetId() { return toolId; }
    public double GetDurabilityUsed() { return durabilityUsed; }
    public int GetTier() { return tier; }

}
