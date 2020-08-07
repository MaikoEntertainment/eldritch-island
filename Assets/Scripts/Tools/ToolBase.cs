using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ToolBase: ScriptableObject
{
    [SerializeField]
    protected int id;
    [SerializeField]
    protected Sprite icon;
    [SerializeField]
    protected long durability;
    [SerializeField]
    protected TextLanguageOwn name;
    [SerializeField]
    protected TextLanguageOwn description;
    [SerializeField]
    protected List<SkillBonus> skillBonuses;
    [SerializeField]
    protected Tag[] tags;

    public int GetId() { return id; }
    public double GetDurability() { return durability; }
    public Tag[] GetTags() { return tags; }
    public List<SkillBonus> GetSkillBonuses() { return skillBonuses; }
    public Sprite GetIcon() { return icon; }
    public void Use()
    {
        
    }

    public virtual List<Item> GetTaskItemCostForThisMonster(Task task, Monster monster, List<Item> currentCosts)
    {
        // Change for Tools with specific abilities
        return currentCosts;
    }
    public virtual List<ItemReward> GetTaskItemRewards(Task task, Monster monster, List<ItemReward> currentRewards)
    {
        // Change for Tools with specific abilities
        return currentRewards;
    }
    public virtual List<Tool> GetTaskToolRewards(Task task, Monster monster, List<Tool> currentRewards)
    {
        // Change for Tools with specific abilities
        return currentRewards;
    }
    public virtual List<Clothes> GetTaskClothesRewards(Task task, Monster monster, List<Clothes> currentRewards)
    {
        // Change for Tools with specific abilities
        return currentRewards;
    }
}
