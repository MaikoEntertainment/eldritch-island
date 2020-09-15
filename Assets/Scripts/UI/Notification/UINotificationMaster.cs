using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINotificationMaster : MonoBehaviour
{
    public static UINotificationMaster _instance;

    public UINotificationTask notificationTaskPrefab;
    public UINotificationSkillLevelUp levelUp;
    public GameObject savePrefab;

    public Transform notificationsList;

    private void Awake()
    {
        if (_instance)
            Destroy(this);
        else
        {
            _instance = this;
        }
    }


    public static UINotificationMaster GetInstance() { return _instance; }

    public void LoadTaskFinish(NotificationTaskFinish not)
    {
        Instantiate(notificationTaskPrefab.gameObject, notificationsList).GetComponent<UINotificationTask>().Load(not);
    }

    public void LoadSkillLevelUp(NotificationLevelUp skillLevelUp)
    {
        Instantiate(levelUp.gameObject, notificationsList).GetComponent<UINotificationSkillLevelUp>().Load(skillLevelUp);
    }

    public void LoadSaveNotification()
    {
        Instantiate(savePrefab, notificationsList);
    }
}
