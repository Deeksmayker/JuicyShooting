using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private Transform ground, firstRectanglePoint, secondRectanglePoint;

    private float delayTimer;

    private SpawnEnemiesData _enemiesSpawnData;
    private int _spawnsIndex;
    private int _enemiesOnSceneCount;

    public UnityEvent AllEnemiesDied = new();
    
    void Start()
    {
        _enemiesSpawnData = GameData.Instance.GetCurrentEnemySpawnData();

        Enemy.EnemyDied.AddListener(OnEnemyDied);
    }

    void Update()
    {
        if (_spawnsIndex == _enemiesSpawnData.EnemySpawns.Count)
            return;
        delayTimer += Time.deltaTime;

        if (delayTimer > _enemiesSpawnData.EnemySpawns[_spawnsIndex].spawnDelay)
        {
            SpawnEnemies();
            delayTimer = 0;
            _enemiesOnSceneCount += _enemiesSpawnData.EnemySpawns[_spawnsIndex].enemiesToSpawn.Count();
            _spawnsIndex++;
        }
    }

    private void SpawnEnemies()
    {
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
    }

    public void OnEnemyDied()
    {
        _enemiesOnSceneCount--;

        if (_enemiesOnSceneCount == 0 && _spawnsIndex == _enemiesSpawnData.EnemySpawns.Count)
        {
            //AllEnemiesDied.Invoke();
        }
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
