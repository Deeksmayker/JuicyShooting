using TMPro;
using UnityEngine;

public class UpgradingMenuUiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponTitle;
    [SerializeField] private Weapon weaponPrefab;
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private float weaponRotationSpeed;

    private void Start()
    {
        weaponTitle.text = GameData.Instance.WeaponName;
        weaponPrefab = Instantiate(GameData.Instance.CurrentWeapon, weaponPosition.position, Quaternion.identity);
    }

    private void Update()
    {
        RotateWeaponPrefab();
    }

    private void RotateWeaponPrefab()
    {
        weaponPrefab.transform.Rotate(Vector3.up, weaponRotationSpeed * Time.deltaTime);
    }
}
