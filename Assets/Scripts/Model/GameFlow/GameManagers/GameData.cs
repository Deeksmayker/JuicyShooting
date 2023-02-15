using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameData : MonoBehaviour
{
    [Serializable]
    private struct WeaponWithStats
    {
        public string weaponName;
        public Vector3 positionRelatedToPlayer;
        public Weapon weaponPrefab;
        public WeaponUpgradeStats weaponStats;
    }

    public static GameData Instance;

    [field: SerializeField, Min(0)] public int Money { get; private set; } = 1000;

    public int MoneyByKill { get; } = 20;
    public int MoneyByKillInWeakPoint { get; } = 20;
    
    [field: SerializeField] public int Level { get; private set; } = 1;

    [SerializeField] private WeaponWithStats[] weaponsWithStats;

    public Weapon CurrentWeapon { get; private set; }
    public WeaponUpgradeStats WeaponStats { get; private set; }
    [field: SerializeField] public DualPerk PlayerDualPerk { get; private set; }
    [field: SerializeField] public ExplosionPerk PlayerExplosionPerk { get; private set; }

    [SerializeField] private List<SpawnEnemiesData> levelsEnemySpawnData = new();

    private int _currentWeaponIndex;

    public UnityEvent<int> moneyValueChanged = new();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;

        CurrentWeapon = weaponsWithStats[_currentWeaponIndex].weaponPrefab;
        WeaponStats = weaponsWithStats[_currentWeaponIndex].weaponStats;
        
        Enemy.EnemyDied.AddListener(HandleKill);
        Enemy.EnemyDiedByWeakPoint.AddListener(HandleWeakPointKill);
    }

    public void HandleKill()
    {
        AddMoney(MoneyByKill);
    }

    public void HandleWeakPointKill()
    {
        AddMoney(MoneyByKillInWeakPoint);
    }
    
    public void AddMoney(int value)
    {
        Money += value;
        moneyValueChanged.Invoke(Money);
    }

    public void RemoveMoney(int value)
    {
        Money -= value;
        moneyValueChanged.Invoke(Money);
    }

    public void SetLevelToNext()
    {
        Level++;
    }

    public void SaveGame()
    {
        
    }

    public void LoadSaveData()
    {
        
    }

    public void ShowAdForReward()
    {
        
    }

    public void ShowAd()
    {
        
    }

    public SpawnEnemiesData GetCurrentEnemySpawnData()
    {
        return levelsEnemySpawnData[Level - 1];
    }

    public void SetWeaponToNext()
    {
        _currentWeaponIndex++;
        CurrentWeapon = weaponsWithStats[_currentWeaponIndex].weaponPrefab;
        WeaponStats = weaponsWithStats[_currentWeaponIndex].weaponStats;
    }

    public string GetCurrentWeaponName() => weaponsWithStats[_currentWeaponIndex].weaponName;
    public bool NextWeaponExisting() => _currentWeaponIndex < weaponsWithStats.Length - 1;
    public int GetNextWeaponCost()
    {
        if (_currentWeaponIndex == weaponsWithStats.Length - 1)
            return -1;
        return weaponsWithStats[_currentWeaponIndex + 1].weaponStats.CostToBuy;
    }

    public Vector3 GetWeaponPositionRelatedToPlayer() => weaponsWithStats[_currentWeaponIndex].positionRelatedToPlayer;
}
