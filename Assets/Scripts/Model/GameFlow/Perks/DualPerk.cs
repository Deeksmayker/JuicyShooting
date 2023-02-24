using System;
using UnityEngine;

[Serializable] 
public class DualPerk : PlayerPerk
{
    [field: Header("Duration")]
    [field: SerializeField] public int DurationLevel { get; set; }
    [field: SerializeField] public float BaseDurationUpgradeCost { get; private set; }
    [field: SerializeField] public float PerLevelDurationCostMultiplier { get; private set; }
    [field: SerializeField] public float DefaultDuration { get; private set; }
    [field: SerializeField] public float PerLevelDurationIncrease { get; private set; }

    [field: Header("Uses Count")]
    [field: SerializeField] public int UsesCount { get; set; }
    [field: SerializeField] public float BaseUsesCountUpgradeCost { get; private set; }
    [field: SerializeField] public float PerLevelUsesCountCostMultiplier { get; private set; }

    public void BuyDualPerk()
    {
        DurationLevel = 1;
        UsesCount = 1;
    }

    public void UpgradeDuration()
    {
        DurationLevel++;

        if (DurationLevel > 10)
            Debug.LogWarning("Duration in Dual Perk is upgraded more than maximum");
    }

    public void UpgradeUsesCount()
    {
        UsesCount++;

        if (UsesCount > 3)
            Debug.LogWarning("Uses count in Dual Perk is upgraded more than maximum");
    }

    public void ResetPerk()
    {
        DurationLevel = 0;
        UsesCount = 0;
    }

    public int GetDurationUpgradeCost() => (int)(BaseDurationUpgradeCost * Mathf.Pow(PerLevelDurationCostMultiplier, DurationLevel - 1));
    public float GetCurrentDuration() => DurationLevel * PerLevelDurationIncrease + DefaultDuration;
    
    public int GetUsesCountUpgradeCost() => (int)(BaseUsesCountUpgradeCost * Mathf.Pow(PerLevelUsesCountCostMultiplier, UsesCount - 1));
    public override bool PerkAvaliable() => !(DurationLevel == 0 && UsesCount == 0);
}
