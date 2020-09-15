using System;
[Serializable]
public class SaveClothes
{
    public int clothesId;
    public double durabilityUsed;
    public int tier;

    public SaveClothes(Clothes c)
    {
        clothesId = c.GetId();
        durabilityUsed = c.GetDurability() - c.GetDurabilityLeft();
        tier = c.GetTier();
    }

    public int GetId() { return clothesId; }
    public double GetDurabilityUsed() { return durabilityUsed; }
    public int GetTier() { return tier; }

}
