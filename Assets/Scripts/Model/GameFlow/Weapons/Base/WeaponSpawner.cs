using Assets.Scripts;
using System.Linq;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    private void Start()
    {
        transform.localPosition = GameData.Instance.GetWeaponPositionRelatedToPlayer();
        var weapon = Instantiate(GameData.Instance.CurrentWeapon, gameObject.transform);
        weapon.transform.localPosition = Vector3.zero;

        Utils.SetWeaponStats(weapon);
    }
}
