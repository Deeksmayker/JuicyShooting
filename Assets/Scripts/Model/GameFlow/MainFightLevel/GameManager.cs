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
        
        HitPopup.PopupPool.Clear();
    }

    private void Update()
    {
        
    }

    [ContextMenu("Emulate Win")]
    public void HandleWin()
    {
        OnWin.Invoke();
        GameData.Instance.SetLevelToNext();
    }
    
    [ContextMenu("Emulate Lose")]
    public void HandleLose()
    {
        OnLose.Invoke();
        
        if (GameData.Instance.Level == 0)
            GameData.Instance.SetLevelToNext();
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
