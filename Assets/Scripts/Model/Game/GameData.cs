using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    [field: SerializeField, Min(0)] public int Money { get; private set; }
    [field: SerializeField] public int Level { get; private set; } = 1;

    [SerializeField] private List<SpawnEnemiesData> levelsEnemySpawnData = new();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void ChangeMoney(int value)
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
