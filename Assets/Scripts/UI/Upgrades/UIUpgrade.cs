using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgrade : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI level;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI bonus;
    public Transform costList;
    public Button upgradeButton;

    public GameObject unlockPanel;
    public TextMeshProUGUI unlockConditionText;

    public UIItem costPrefab;

    private Upgrade upgrade;

    public void Load(UpgradeId id)
    {
        upgrade = UpgradeMaster.GetInstance().GetUpgrade(id);
        icon.sprite = upgrade.GetUpgradeBase().GetIcon();
        title.text = upgrade.GetUpgradeBase().name.GetText();
        description.text = upgrade.GetUpgradeBase().GetDescription();
        if (upgrade.GetUpgradeBase().IsAvailable())
            unlockPanel.SetActive(false);
        else
            unlockConditionText.text = upgrade.GetUpgradeBase().GetUnlockCondition();
        View(upgrade.GetLevel());
    }
    public void View()
    {
        View(upgrade.GetLevel());
        bonus.color = Color.white;
        level.color = Color.white;
    }
    public void View(int level)
    {
        this.level.text = level.ToString();
        bonus.text = upgrade.GetUpgradeBase().GetBonusUI(level);
        foreach (Transform i in costList)
        {
            Destroy(i.gameObject);
        }
        foreach (Item i in upgrade.GetLevelUpCost())
        {
            Item inventoryItem = InventoryMaster.GetInstance().GetItem(i.GetId());
            bool cantPay = (inventoryItem == null || inventoryItem.GetAmount() < i.GetAmount());
            upgradeButton.interactable = !cantPay;
            Instantiate(costPrefab.gameObject, costList).GetComponent<UIItem>().Load(i, cantPay);
        }
    }
    public void PreviewNext()
    {
        View(upgrade.GetLevel() + 1);
        bonus.color = Utils.GetSuccessColor();
        level.color = Utils.GetSuccessColor();
    }
    public void LevelUp()
    {
        UpgradeMaster.GetInstance().LevelUpUpgrade(upgrade.GetUpgradeBase().id);
        PreviewNext();
    }
}
