using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeHandler : MonoBehaviour
{
    public List<UIUpgradeTier> tiers;

    public Transform upgradesTierLists;
    public UIUpgrade upgradePrefab;
    public LayoutGroup tierPrefab;

    public void Load()
    {
        foreach (Transform tier in upgradesTierLists)
            Destroy(tier.gameObject);
        foreach(UIUpgradeTier tier in tiers)
        {
            GameObject tierInstance = Instantiate(tierPrefab.gameObject, upgradesTierLists);
            foreach(UpgradeId id in tier.upgradeIds)
            {
                Instantiate(upgradePrefab.gameObject, tierInstance.transform).GetComponent<UIUpgrade>().Load(id);
            }
        }
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
