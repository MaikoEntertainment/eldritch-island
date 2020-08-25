using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationLevelUp
{
    [SerializeField]
    protected Notification notificationBase;

    protected Sprite icon;
    protected string levelUpName;
    protected int newLevel;

    public NotificationLevelUp(Notification notBase, Sprite icon, string levelUpName, int newLevel)
    {
        notificationBase = notBase;
        this.icon = icon;
        this.levelUpName = levelUpName;
        this.newLevel = newLevel;
    }

    public Sprite GetIcon() { return icon; }
    public string GetTitle() { return levelUpName + " " + notificationBase.GetTitle() + " " + newLevel + "!"; }
}
