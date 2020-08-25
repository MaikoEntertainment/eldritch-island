using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NotificationTaskFinish
{
    [SerializeField]
    protected Notification notificationBase;
    protected Task task;
    protected List<Item> itemRewards;
    protected List<Tool> toolRewards;
    protected List<Clothes> clothesRewards;

    public NotificationTaskFinish(Notification not, Task task, List<Item> rewards, List<Tool> tools, List<Clothes> clothes)
    {
        this.task = task;
        notificationBase = not;
        itemRewards = rewards;
        toolRewards = tools;
        clothesRewards = clothes;
    }
    public Notification GetNotificationBase() { return notificationBase; }
    public virtual string GetTitle() { return task.GetTask().GetName() + " " + notificationBase.GetTitle(); }
    public Sprite GetIcon() { return task.GetTask().GetIcon(); }
    public List<Item> GetItemRewards() { return itemRewards; }
    public List<Tool> GetToolRewards() { return toolRewards; }
    public List<Clothes> GetClothesRewards() { return clothesRewards; }
    
}
