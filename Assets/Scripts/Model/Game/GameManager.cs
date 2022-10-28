using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public enum EnemyTypes
    {
        NormalZombie
    }

    [SerializeField] private Enemy normalZombiePrefab;

    [Serializable]
    public class EnemiesSpawn
    {
        public EnemyTypes[] enemiesToSpawn;
        public float spawnDelay;
    }

    [SerializeField] private Transform ground, firstRectanglePoint, secondRectanglePoint;

    [SerializeField] private bool repeatFirst;
    [SerializeField] private List<EnemiesSpawn> enemySpawns = new();

    private float delayTimer;

    private void Awake()
    {

    }

    private void Update()
    {

        if (enemySpawns.Count() == 0)
            return;
        delayTimer += Time.deltaTime;

        if (delayTimer > enemySpawns[0].spawnDelay)
        {
            SpawnEnemies();
            delayTimer = 0;
        }
    }

    private void SpawnEnemies()
    {
        foreach (var enemyType in enemySpawns[0].enemiesToSpawn)
        {
            switch (enemyType)
            {
                case EnemyTypes.NormalZombie:
                    Instantiate(normalZombiePrefab, GetRandomSpawnPosition(), Quaternion.identity);
                    break;
                default:
                    break;
            }
            
        }
        if (repeatFirst)
            return;
        enemySpawns.RemoveAt(0);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3
            (
            Random.Range(firstRectanglePoint.position.x, secondRectanglePoint.position.x),
            ground.position.y + 0.5f,
            Random.Range(firstRectanglePoint.position.z, secondRectanglePoint.position.z)
            );
    }
}
