using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Notifications/NotificationBase")]
public class Notification : ScriptableObject
{

    [SerializeField]
    protected TextLanguageOwn title;

    public Notification(Sprite icon, TextLanguageOwn title)
    {
        this.title = title;
    }

    public string GetTitle() { return title.GetText(); }
}
