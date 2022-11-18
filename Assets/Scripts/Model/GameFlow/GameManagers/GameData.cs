using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    [field: SerializeField, Min(0)] public int Money { get; private set; } = 1000;
    [field: SerializeField] public int Level { get; private set; } = 1;
    [field: SerializeField] public string WeaponName { get; private set; }
    public Weapon CurrentWeapon;
    public WeaponUpgradeStats WeaponUpgradeStats;

    [SerializeField] private List<SpawnEnemiesData> levelsEnemySpawnData = new();

    public UnityEvent<int> moneyValueChanged = new();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;

        WeaponUpgradeStats = new(100, 100, 1.3f, 1.3f, 0.1f, 0.1f);
    }

    public void AddMoney(int value)
    {
        Money += value;
        Debug.Log(value);
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

    public SpawnEnemiesData GetCurrentEnemySpawnData()
    {
        return levelsEnemySpawnData[Level - 1];
    }
}
