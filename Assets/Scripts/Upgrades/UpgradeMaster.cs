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

    private void Start()
    {
        StatisticValue sv = StatisticsMaster.GetInstance().GetStatistic(StatisticIds.Dungeon1Clears);
        sv.OnValueUpdate += (object o) => CheckForLetters();
        CheckForLetters();
    }

    public void CheckForLetters()
    {
        StatisticValue sv = StatisticsMaster.GetInstance().GetStatistic(StatisticIds.Dungeon1Clears);
        double clears1 = (double)sv.GetValue();
        if (clears1 >= 5)
            LetterMaster.GetInstance().UnlockLetter(LetterId.temple1);
        if (clears1 >= 10)
            LetterMaster.GetInstance().UnlockLetter(LetterId.temple2);
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
