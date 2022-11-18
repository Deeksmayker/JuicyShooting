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
        weaponPrefab = Instantiate(GameData.Instance.CurrentWeapon, weaponPosition);
        weaponPrefab.transform.localPosition = Vector3.zero + weaponPrefab.transform.forward / 2;
    }

    private void Update()
    {
        RotateWeaponPrefab();
    }

    private void RotateWeaponPrefab()
    {
        weaponPosition.transform.Rotate(Vector3.up, weaponRotationSpeed * Time.deltaTime);
    }
}
