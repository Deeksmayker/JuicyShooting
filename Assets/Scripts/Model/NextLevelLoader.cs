using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelLoader : MonoBehaviour
{
    public void LoadNextLevel()
    {
        Debug.Log("Сам уровень не переключился, только загрузилась сцена");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
