using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ExplosionPerk : PlayerPerk
{
    [field: Header("Frequency")]
    [field: SerializeField] public int DefaultFrequency;
    [field: SerializeField] public int FrequencyLevel;
    [field: SerializeField] public int FrequencyBaseCost;
    [field: SerializeField] public float PerLevelFrequencyCostMultiplier;

    [field: Header("Explosion radius")]
    [field: SerializeField] public int RadiusLevel;
    [field: SerializeField] public int RadiusBaseCost;
    [field: SerializeField] public float PerLevelRadiusCostMultiplier;
    [field: SerializeField] public float DefaultRadius;
    [field: SerializeField] public float PerLevelRadiusIncrease;

    public void BuyExplosionPerk()
    {
        RadiusLevel = 1;
        FrequencyLevel = 1;
    }

    public void UpgradeFrequency()
    {
        FrequencyLevel++;
    }

    public void UpgradeRadius()
    {
        RadiusLevel++;
    }

    public int GetFrequencyUpgradeCost() => (int)(FrequencyBaseCost * Mathf.Pow(PerLevelFrequencyCostMultiplier, FrequencyLevel - 1));
    public int GetCurrentFrequency() => DefaultFrequency - FrequencyLevel + 1;

    public int GetRadiusUpgradeCost() => (int)(RadiusBaseCost * Mathf.Pow(PerLevelRadiusCostMultiplier, RadiusLevel - 1));
    public float GetCurrentRadius() => RadiusLevel * PerLevelRadiusIncrease + DefaultRadius;

    public override bool PerkAvaliable() => RadiusLevel > 0 && FrequencyLevel > 0;
}
