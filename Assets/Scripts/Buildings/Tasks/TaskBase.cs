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
    protected TextLanguageOwn unlockCondition;
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
    protected List<ToolBase> toolRewards;
    [SerializeField]
    protected List<ClothesBase> clothesRewards;
    [SerializeField]
    protected Notification onEndNotification;

    public int GetId() { return id; }
    public string GetName() { return myName.GetText(); }
    public string GetDescription() { return description.GetText(); }
    public string GetUnlockCondition() { return unlockCondition.GetText(); }
    public Sprite GetIcon() { return icon; }
    public virtual double GetProgressNeeded()
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
    public virtual List<Item> GetItemCost()
    {
        return itemCosts;
    }
    public List<ItemReward> GetItemRewards()
    {
        return itemRewards;
    }
    public List<ToolBase> GetToolRewards()
    {
        
        return toolRewards;
    }
    public List<ClothesBase> GetClotheRewards()
    {
        return clothesRewards;
    }
    public virtual void OnComplete()
    {
       
    }
    public virtual bool IsAvailable()
    {
        return true;
    }

    public virtual NotificationTaskFinish GetNotificationOnEnd(Task task, List<Item> rewards, List<Tool> tools, List<Clothes> clothes)
    {
        NotificationTaskFinish ntf = new NotificationTaskFinish(onEndNotification, task, rewards, tools, clothes);
        return ntf;
    }
}
