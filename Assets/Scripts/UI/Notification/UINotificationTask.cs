
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINotificationTask : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI title;

    public Transform rewardsList;

    public UIItem itemRewardPrefab;
    public UITaskMonsterTool toolRewardPrefab;
    public UITaskMonsterClothes clothesRewardPrefab;

    public void Load(NotificationTaskFinish not)
    {
        icon.sprite = not.GetIcon();
        title.text = not.GetTitle();
        foreach(Item i in not.GetItemRewards())
        {
            Instantiate(itemRewardPrefab.gameObject, rewardsList).GetComponent<UIItem>().Load(i);
        }
        foreach (Tool t in not.GetToolRewards())
        {
            Instantiate(toolRewardPrefab.gameObject, rewardsList).GetComponent<UITaskMonsterTool>().Load(t);
        }
        foreach (Clothes c in not.GetClothesRewards())
        {
            Instantiate(clothesRewardPrefab.gameObject, rewardsList).GetComponent<UITaskMonsterClothes>().Load(c);
        }
    }
}
