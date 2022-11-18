using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponUpgradeStats
{
    public int ReloadLevel { get; private set; }
    public int SpreadLevel { get; private set; }

    public float ReduceReloadTimePerLevel { get; private set; }
    public float ReduceSpreadPerLevel { get; private set; }

    public float BaseReloadUpgradeCost { get; private set; }
    public float BaseSpreadUpgradeCost { get; private set; }
    public float PerLevelReloadCostMultiplier { get; private set; }
    public float PerLevelSpreadCostMultiplier { get; private set; }

    public WeaponUpgradeStats(float reloadCost, float spreadCost, float perLevelReloadCostMultiplier, float perLevelSpreadCostMultiplier,
        float reduceReload, float reduceSpread)
    {
        BaseReloadUpgradeCost = reloadCost;
        BaseSpreadUpgradeCost = spreadCost;
        PerLevelReloadCostMultiplier = perLevelReloadCostMultiplier;
        PerLevelSpreadCostMultiplier = perLevelSpreadCostMultiplier;
        ReduceReloadTimePerLevel = reduceReload;
        ReduceSpreadPerLevel = reduceSpread;
    }

    public void UpgradeReloadTime()
    {
        ReloadLevel++;
    }

    public void UpgradeSpread()
    {
        SpreadLevel++;
    }

    public float GetReloadTimeToReduce() => ReloadLevel * ReduceReloadTimePerLevel;
    public float GetSpreadToReduce() => SpreadLevel * ReduceSpreadPerLevel;

    public int GetReloadUpgradeCost() => (int)(BaseReloadUpgradeCost * Mathf.Pow(PerLevelReloadCostMultiplier, ReloadLevel));
    public int GetSpreadUpgradeCost() => (int)(BaseSpreadUpgradeCost * Mathf.Pow(PerLevelSpreadCostMultiplier, SpreadLevel));
}
