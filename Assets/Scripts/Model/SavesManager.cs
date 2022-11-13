using UnityEngine;

public class SavesManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static void SaveGameData()
    {
        
    }
}
