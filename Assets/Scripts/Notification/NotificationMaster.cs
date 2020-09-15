using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationMaster : MonoBehaviour
{
    public static NotificationMaster _instance;

    public Notification notificationLevelUpBase;

    private void Awake()
    {
        if (_instance)
            Destroy(this);
        else
        {
            _instance = this;
        }
    }

    public static NotificationMaster GetInstance() { return _instance; }

    public void SendTaskNotification(NotificationTaskFinish not)
    {
        UINotificationMaster.GetInstance().LoadTaskFinish(not);
    }
    public void SendLevelUpNotification(Sprite icon, string levelUpName, int newLevel)
    {
        UINotificationMaster.GetInstance().LoadSkillLevelUp(new NotificationLevelUp(notificationLevelUpBase, icon, levelUpName, newLevel));
    }
    public void SendSaveNotifcation()
    {
        UINotificationMaster.GetInstance().LoadSaveNotification();
    }
}
