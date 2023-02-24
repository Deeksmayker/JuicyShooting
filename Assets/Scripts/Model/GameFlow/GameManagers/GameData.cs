using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using YG;

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
    
    [field: SerializeField] public int Level { get; private set; } = 0;
    public float CurrentSpeedModifier = 1;

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

        if (YandexGame.SDKEnabled)
        {
            LoadSaveData(); 
        }
        

        if (Level == 0)
        {
            CurrentWeapon = weaponsWithStats[_currentWeaponIndex].weaponPrefab;
            WeaponStats = weaponsWithStats[_currentWeaponIndex].weaponStats;
            
            PlayerExplosionPerk.FrequencyLevel = 8;
            PlayerExplosionPerk.RadiusLevel = 6;
        }
        
        Enemy.EnemyDied.AddListener(HandleKill);
        Enemy.EnemyDiedByWeakPoint.AddListener(HandleWeakPointKill);

        CurrentSpeedModifier = Level < levelsEnemySpawnData.Count
            ? levelsEnemySpawnData[Level].speedModifier
            : (Level - levelsEnemySpawnData.Count) * 0.05f +
              levelsEnemySpawnData[levelsEnemySpawnData.Count - 1].speedModifier;
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += LoadSaveData;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= LoadSaveData;
    }

    public void HandleKill()
    {
        if (Level == 0)
            return;
        AddMoney(MoneyByKill);
    }

    public void HandleWeakPointKill()
    {
        if (Level == 0)
            return;
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

        CurrentSpeedModifier = Level < levelsEnemySpawnData.Count
            ? levelsEnemySpawnData[Level].speedModifier
            : (Level - levelsEnemySpawnData.Count) * 0.05f +
              levelsEnemySpawnData[levelsEnemySpawnData.Count - 1].speedModifier;
        if (Level == 1)
        {
            SetWeaponToNext();
            PlayerExplosionPerk.FrequencyLevel = 0;
            PlayerExplosionPerk.RadiusLevel = 0;
        }
    }

    public void SaveGame()
    {
        YandexGame.savesData.Money = Money;
        YandexGame.savesData.Level = Level;
        YandexGame.savesData.CurrentWeaponIndex = _currentWeaponIndex;
        YandexGame.savesData.ExplosionFrequency = PlayerExplosionPerk.FrequencyLevel;
        YandexGame.savesData.ExplosionRadius = PlayerExplosionPerk.RadiusLevel;
        YandexGame.savesData.DualDuration = PlayerDualPerk.DurationLevel;
        YandexGame.savesData.DualUses = PlayerDualPerk.UsesCount;
        YandexGame.savesData.ReloadLevel = WeaponStats.ReloadLevel;
        YandexGame.savesData.SpreadLevel = WeaponStats.SpreadLevel;
        YandexGame.SaveProgress();
    }

    public void LoadSaveData()
    {
        Money = YandexGame.savesData.Money;
        Level = YandexGame.savesData.Level;
        _currentWeaponIndex = YandexGame.savesData.CurrentWeaponIndex;
        CurrentWeapon = weaponsWithStats[_currentWeaponIndex].weaponPrefab;
        WeaponStats = weaponsWithStats[_currentWeaponIndex].weaponStats;

        WeaponStats.ReloadLevel = YandexGame.savesData.ReloadLevel;
        WeaponStats.SpreadLevel = YandexGame.savesData.SpreadLevel;

        if (Level == 0)
            return;
        
        CurrentSpeedModifier = Level < levelsEnemySpawnData.Count
            ? levelsEnemySpawnData[Level].speedModifier
            : (Level - levelsEnemySpawnData.Count) * 0.05f +
              levelsEnemySpawnData[levelsEnemySpawnData.Count - 1].speedModifier;
        
        PlayerDualPerk.DurationLevel = YandexGame.savesData.DualDuration;
        PlayerDualPerk.UsesCount = YandexGame.savesData.DualUses;
        PlayerExplosionPerk.FrequencyLevel = YandexGame.savesData.ExplosionFrequency;
        PlayerExplosionPerk.RadiusLevel = YandexGame.savesData.ExplosionRadius;
    }

    public void ResetSave()
    {
        Level = 0;
        
        _currentWeaponIndex = -1;
        SetWeaponToNext();

        PlayerDualPerk.ResetPerk();
        PlayerExplosionPerk.ResetPerk();

        for (var i = 0; i < weaponsWithStats.Length; i++)
        {
            weaponsWithStats[i].weaponStats.ReloadLevel = 0;
            weaponsWithStats[i].weaponStats.SpreadLevel = 0;
        }
        
        WeaponStats.ReloadLevel = 0;
        WeaponStats.SpreadLevel = 0;

        CurrentSpeedModifier = levelsEnemySpawnData[0].speedModifier;
        
        PlayerExplosionPerk.FrequencyLevel = 8;
        PlayerExplosionPerk.RadiusLevel = 6;

        Money = 0;
        
        SaveGame();
    }

    public void ShowAdForReward(int id)
    {
        YandexGame.RewVideoShow(id);
    }

    public void ShowAd()
    {
        
    }

    public SpawnEnemiesData GetCurrentEnemySpawnData()
    {
        return Level < levelsEnemySpawnData.Count ? levelsEnemySpawnData[Level] : levelsEnemySpawnData[levelsEnemySpawnData.Count - 1];
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
