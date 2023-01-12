using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Levels/Level Enemies", fileName = "Level x enemies", order = 0)]
public class SpawnEnemiesData : ScriptableObject
{
    public enum EnemyTypes
    {
        NormalZombie
    }

    public enum SpawnLocations
    {
        MiddleBridge,
        LeftBridge,
        RightBridge
    }

    [Serializable]
    public struct EnemiesSpawn
    {
        public SpawnLocations spawnLocation;
        public EnemyTypes[] enemiesToSpawn;
        public float spawnDelay;
    }

    [field: SerializeField, FormerlySerializedAs("enemySpawns")] public List<EnemiesSpawn> EnemySpawns { get; private set; } = new();

    public EnemyPrefabs EnemyPrefabs;
}
