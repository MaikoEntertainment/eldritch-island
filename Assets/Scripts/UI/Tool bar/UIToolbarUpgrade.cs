using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIToolbarUpgrade : MonoBehaviour
{
    public TextMeshProUGUI upgradesAvailable;
    public Button button;
    public Color availableColor;
    public AudioClip onNewSummon;

    public void OpenUpgrades()
    {
        UIUpgradeMaster.GetInstance().OpenUpgrades();
    }
}
