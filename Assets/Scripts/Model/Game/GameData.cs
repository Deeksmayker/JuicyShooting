using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    [field: SerializeField, Min(0)] public int Money { get; private set; }
    [field: SerializeField] public int Level { get; private set; } = 1;
    [field: SerializeField] public string WeaponName { get; private set; }
    [field: SerializeField] public Weapon CurrentWeapon;

    [SerializeField] private List<SpawnEnemiesData> levelsEnemySpawnData = new();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void AddMoney(int value)
    {
        Money += value;
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
