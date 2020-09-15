using System;
using System.Collections.Generic;

[Serializable]
public class SaveFile
{
    public SaveInventory saveInventory = new SaveInventory();
    public List<SaveMonster> savedMonsters = new List<SaveMonster>();
    public List<SaveBuilding> savedBuildings = new List<SaveBuilding>();
    public List<SaveLetter> savedLetters = new List<SaveLetter>();
    public List<SaveStatisticNumber> savedStatisticsNumber = new List<SaveStatisticNumber>();
    public List<SaveUpgrade> savedUpgrades = new List<SaveUpgrade>();

    public float musicVolume = 0.5f;
    public float effectsVolume = 0.5f;

    public void SaveInventory(List<Item> items, List<Tool> tools, List<Clothes> clothes)
    {
        saveInventory = new SaveInventory();
        saveInventory.SaveItems(items);
        saveInventory.SaveTools(tools);
        saveInventory.SaveClothes(clothes);
    }

    public void SaveMonsters(List<Monster> monsters)
    {
        savedMonsters = new List<SaveMonster>();
        foreach (Monster m in monsters)
        {
            savedMonsters.Add(new SaveMonster(m));
        }
    }

    public void SaveBuildings(List<Building> buildings)
    {
        savedBuildings = new List<SaveBuilding>();
        foreach (Building b in buildings)
        {
            savedBuildings.Add(new SaveBuilding(b));
        }
    }

    public void SaveLetters(List<Letter> letters)
    {
        savedLetters = new List<SaveLetter>();
        foreach (Letter l in letters)
        {
            savedLetters.Add(new SaveLetter(l));
        }
    }

    public void SaveStatistics(List<StatisticValue> values)
    {
        savedStatisticsNumber = new List<SaveStatisticNumber>();
        foreach (StatisticValue s in values)
        {
            if (typeof(double) == s.GetValue().GetType())
                savedStatisticsNumber.Add(new SaveStatisticNumber(s));
        }
    }

    public void SaveUpgrades(List<Upgrade> upgrades)
    {
        savedUpgrades = new List<SaveUpgrade>();
        foreach (Upgrade u in upgrades)
        {
            savedUpgrades.Add(new SaveUpgrade(u));
        }
    }
}
