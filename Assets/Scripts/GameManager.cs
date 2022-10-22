using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public class EnemiesSpawn
    {
        public Enemy[] enemiesToSpawn;
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
        foreach (var enemy in enemySpawns[0].enemiesToSpawn)
        {
            Instantiate(enemy, GetRandomSpawnPosition(), Quaternion.identity);
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
            ground.position.y + 2,
            Random.Range(firstRectanglePoint.position.z, secondRectanglePoint.position.z)
            );
    }
}
