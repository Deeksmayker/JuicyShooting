using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform ground, firstRectanglePoint, secondRectanglePoint;

    [SerializeField] private bool repeatFirst;

    private float delayTimer;

    private SpawnEnemiesData _enemiesSpawnData;
    private int _spawnsIndex;

    private void Awake()
    {
        _enemiesSpawnData = GameData.Instance.GetCurrentEnemySpawnData();
    }

    private void Update()
    {
        if (_spawnsIndex == _enemiesSpawnData.EnemySpawns.Count - 1)
            return;
        delayTimer += Time.deltaTime;

        if (delayTimer > _enemiesSpawnData.EnemySpawns[_spawnsIndex].spawnDelay)
        {
            SpawnEnemies();
            delayTimer = 0;
            _spawnsIndex++;
        }
    }

    private void SpawnEnemies()
    {
        Debug.Log(_enemiesSpawnData.EnemySpawns[_spawnsIndex].enemiesToSpawn.Count());
        foreach (var enemyType in _enemiesSpawnData.EnemySpawns[_spawnsIndex].enemiesToSpawn)
        {
            switch (enemyType)
            {
                case SpawnEnemiesData.EnemyTypes.NormalZombie:
                    Instantiate(_enemiesSpawnData.EnemyPrefabs.normalZombie, GetRandomSpawnPosition(), Quaternion.identity);
                    break;
                default:
                    break;
            }
            
        }
        if (repeatFirst)
            return;
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
