using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private EnemySpawnManager _spawnManager;

    private void Start()
    {
        _spawnManager = GetComponent<EnemySpawnManager>();
        _spawnManager.AllEnemiesDied.AddListener(() => StartCoroutine(EndLevel()));
    }

    private void Update()
    {
        
    }


    public IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
