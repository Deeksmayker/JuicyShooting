using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private EnemySpawnManager _spawnManager;

    private void Start()
    {
        _spawnManager = GetComponent<EnemySpawnManager>();
    }

    private void Update()
    {
        
    }


    public void LoadUpgradeSceneOnLose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
