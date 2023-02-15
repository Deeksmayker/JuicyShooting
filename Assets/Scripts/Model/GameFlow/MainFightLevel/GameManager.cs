using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private EnemySpawnManager _spawnManager;

    public static UnityEvent OnWin = new();
    public static UnityEvent OnLose = new();
    
    private void Start()
    {
        _spawnManager = GetComponent<EnemySpawnManager>();

        _spawnManager.AllEnemiesDied.AddListener(HandleWin);
    }

    private void Update()
    {
        
    }

    [ContextMenu("Emulate Win")]
    public void HandleWin()
    {
        OnWin.Invoke();
    }
    
    [ContextMenu("Emulate Lose")]
    public void HandleLose()
    {
        OnLose.Invoke();
    }

    [ContextMenu("Emulate Kill")]
    public void DebugEmulateKill()
    {
        Enemy.EnemyDied.Invoke();
    }

    [ContextMenu("Emulate Weak Point Kill")]
    public void DebugEmulateWeakPointKill()
    {
        Enemy.EnemyDiedByWeakPoint.Invoke();
    }

    public void LoadUpgradeSceneOnLose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
