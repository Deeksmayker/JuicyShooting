using System;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
[Serializable]
public class WeaponUpgradeStats
{
    public int ReloadLevel { get; set; }
    public int SpreadLevel { get; set; }


    [field: SerializeField] public int CostToBuy { get; private set; }
    [field: SerializeField] public float ReduceReloadTimePerLevel { get; private set; }
    [field: SerializeField] public float ReduceSpreadPerLevel { get; private set; }
    [field: SerializeField] public float BaseReloadUpgradeCost { get; private set; }
    [field: SerializeField] public float BaseSpreadUpgradeCost { get; private set; }
    [field: SerializeField] public float PerLevelReloadCostMultiplier { get; private set; }
    [field: SerializeField] public float PerLevelSpreadCostMultiplier { get; private set; }

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
