using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    private void Start()
    {
        transform.localPosition = GameData.Instance.GetWeaponPositionRelatedToPlayer();
        var weapon = Instantiate(GameData.Instance.CurrentWeapon, gameObject.transform);
        weapon.transform.localPosition = Vector3.zero;

        weapon.reloadTime -= GameData.Instance.WeaponStats.GetReloadTimeToReduce();
        weapon.spread -= GameData.Instance.WeaponStats.GetSpreadToReduce();
    }
}
