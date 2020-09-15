using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SaveUpgrade
{
    public UpgradeId id;
    public int level;

    public SaveUpgrade(Upgrade upgrade)
    {
        id = upgrade.GetUpgradeBase().id;
        level = upgrade.GetLevel();
    }
}
