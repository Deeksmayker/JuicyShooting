using UnityEngine;


public class SaveResetter : MonoBehaviour
{
    public void ResetSaves()
    {
        GameData.Instance.ResetSave();
    }
}
