using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMaster : MonoBehaviour
{
    public static UpgradeMaster _instance;
    [SerializeField]
    protected List<UpgradeBase> upgrades;
    protected Dictionary<UpgradeId, Upgrade> upgradeDictionary = new Dictionary<UpgradeId, Upgrade>();

    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
            foreach(UpgradeBase upgrade in upgrades)
            {
                upgradeDictionary.Add(upgrade.id, new Upgrade(upgrade,0));
            }
        }
    }

    public static UpgradeMaster GetInstance() { return _instance; }

    public void Load(List<SaveUpgrade> savedUpgrades)
    {
        foreach(SaveUpgrade su in savedUpgrades)
        {
            if (upgradeDictionary.ContainsKey(su.id))
                upgradeDictionary[su.id].SetLevel(su.level);
        }
    }

    public Upgrade GetUpgrade(UpgradeId id)
    {
        if (upgradeDictionary.ContainsKey(id))
            return upgradeDictionary[id];
        return null;
    }

    public Dictionary<UpgradeId, Upgrade> GetUpgrades() { return upgradeDictionary; }

    public void LevelUpUpgrade(UpgradeId id, int levels = 1)
    {
        Upgrade u = GetUpgrade(id);
        u.LevelUp(levels);
    }
}
