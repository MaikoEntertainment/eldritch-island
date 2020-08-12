using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Tasks/Taskbase")]
public class TaskBase: ScriptableObject
{
    [SerializeField]
    protected int id = 0;
    [SerializeField]
    protected double progressNeeded = 0;
    [SerializeField]
    protected Tag[] tags;
    [SerializeField]
    protected TextLanguageOwn myName;
    [SerializeField]
    protected TextLanguageOwn description;
    [SerializeField]
    protected Sprite icon;

    [SerializeField]
    protected float stressChange = 10;
    [SerializeField]
    protected List<Item> itemCosts;
    [SerializeField]
    protected List<Item> costPerMonster;
    [SerializeField]
    protected List<SkillBonus> skillsRequired;
    [SerializeField]
    protected List<ItemReward> itemRewards;
    [SerializeField]
    protected List<Tool> toolRewards;
    [SerializeField]
    protected List<Clothes> clothesRewards;

    public int GetId() { return id; }
    public string GetName() { return myName.GetText(); }
    public string GetDescription() { return description.GetText(); }
    public Sprite GetIcon() { return icon; }
    public double GetProgressNeeded()
    {
        return progressNeeded;
    }
    public Tag[] GetTags() { return tags; } 
    public float GetStressChange()
    {
        return stressChange;
    }
    public List<SkillBonus> GetSkillsRequired() { return skillsRequired; }
    public List<Item> GetCostPerMonster() { return costPerMonster; }
    public List<Item> GetItemCost()
    {
        return itemCosts;
    }

    public List<ItemReward> GetItemRewards()
    {
        return itemRewards;
    }

    public List<Tool> GetToolRewards()
    {
        return toolRewards;
    }

    public List<Clothes> GetClotheRewards()
    {
        return clothesRewards;
    }
}
