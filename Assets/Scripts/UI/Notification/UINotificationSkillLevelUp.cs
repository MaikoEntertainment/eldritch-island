using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINotificationSkillLevelUp : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI title;

    public void Load(NotificationLevelUp not)
    {
        icon.sprite = not.GetIcon();
        title.text = not.GetTitle();
    }
}
