using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade Level Exponential")]
public class UpgradeBaseLevelExp : UpgradeBase
{
    public float levelExp = 0.5f;
    public override float GetBonus(int level = 0)
    {
        return baseValue * Mathf.Pow(level, levelExp);
    }
}
