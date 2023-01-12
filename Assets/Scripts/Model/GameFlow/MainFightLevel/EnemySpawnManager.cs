using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private Transform ground;
    [SerializeField] private Transform leftBridgeFirstSpawnPoint, leftBridgeSecondSpawnPoint;
    [SerializeField] private Transform middleBridgeFirstSpawnPoint, middleBridgeSecondSpawnPoint;
    [SerializeField] private Transform rightBridgeFirstSpawnPoint, rightBridgeSecondSpawnPoint;

    [Header("Visual")]
    [SerializeField] private ParticleSystem enemyAppearingParticles;

    private float _delayTimer;

    private SpawnEnemiesData _enemiesSpawnData;
    private int _spawnsIndex;
    private int _enemiesOnSceneCount;

    [HideInInspector] public UnityEvent AllEnemiesDied = new();
    
    void Start()
    {
        _enemiesSpawnData = GameData.Instance.GetCurrentEnemySpawnData();

        Enemy.EnemyDied.AddListener(OnEnemyDied);
    }

    void Update()
    {
        if (_spawnsIndex == _enemiesSpawnData.EnemySpawns.Count)
            return;
        _delayTimer += Time.deltaTime;

        if (_delayTimer > _enemiesSpawnData.EnemySpawns[_spawnsIndex].spawnDelay)
        {
            ChooseSpawnLocationAndSpawnEnemies();
            _delayTimer = 0;
            _enemiesOnSceneCount += _enemiesSpawnData.EnemySpawns[_spawnsIndex].enemiesToSpawn.Count();
            _spawnsIndex++;
        }
    }

    private void ChooseSpawnLocationAndSpawnEnemies()
    {
        var firstRectanglePoint = Vector3.zero;
        var secondRectanglePoint = Vector3.zero;
        
        switch (_enemiesSpawnData.EnemySpawns[_spawnsIndex].spawnLocation)
        {
            case SpawnEnemiesData.SpawnLocations.MiddleBridge:
                firstRectanglePoint = middleBridgeFirstSpawnPoint.position;
                secondRectanglePoint = middleBridgeSecondSpawnPoint.position;
                break;
            case SpawnEnemiesData.SpawnLocations.LeftBridge:
                firstRectanglePoint = leftBridgeFirstSpawnPoint.position;
                secondRectanglePoint = leftBridgeSecondSpawnPoint.position;
                break;
            case SpawnEnemiesData.SpawnLocations.RightBridge:
                firstRectanglePoint = rightBridgeFirstSpawnPoint.position;
                secondRectanglePoint = rightBridgeSecondSpawnPoint.position;
                break;
        }
        
        StartCoroutine(SpawnEnemies(firstRectanglePoint, secondRectanglePoint));
    }

    private IEnumerator SpawnEnemies(Vector3 firstRectanglePoint, Vector3 secondRectanglePoint)
    {
        foreach (var enemyType in _enemiesSpawnData.EnemySpawns[_spawnsIndex].enemiesToSpawn)
        {
            var randomSpawnPosition = GetRandomSpawnPosition(firstRectanglePoint, secondRectanglePoint);
            
            MakeEnemyAppearingEffect(randomSpawnPosition);
            yield return new WaitForSeconds(1);
            
            switch (enemyType)
            {
                case SpawnEnemiesData.EnemyTypes.NormalZombie:
                    Instantiate(_enemiesSpawnData.EnemyPrefabs.normalZombie, randomSpawnPosition, Quaternion.identity);
                    break;
            }
        }
    }

    private void MakeEnemyAppearingEffect(Vector3 position)
    {
        Instantiate(enemyAppearingParticles, position, Quaternion.identity);
    }

    public void OnEnemyDied()
    {
        _enemiesOnSceneCount--;

        if (_enemiesOnSceneCount == 0 && _spawnsIndex == _enemiesSpawnData.EnemySpawns.Count)
        {
            //AllEnemiesDied.Invoke();
        }
    }

    private Vector3 GetRandomSpawnPosition(Vector3 firstRectanglePoint, Vector3 secondRectanglePoint)
    {
        return new Vector3
            (
            Random.Range(firstRectanglePoint.x, secondRectanglePoint.x),
            ground.position.y + 0.5f,
            Random.Range(firstRectanglePoint.z, secondRectanglePoint.z)
            );
    }
}
