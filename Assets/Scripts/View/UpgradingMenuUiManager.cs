using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradingMenuUiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponTitle;
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private float weaponRotationSpeed;

    [Header("Buttons")]
    [SerializeField] private Button reloadButton;
    [SerializeField] private Button spreadButton;
    [SerializeField] private Button weaponButton;
    [SerializeField] private Button fastTurretBuyButton;
    [SerializeField] private Button rocketTurretBuyButton;

    [Header("Text fields")]
    [SerializeField] private TextMeshProUGUI moneyCount;
    [SerializeField] private TextMeshProUGUI reloadCost;
    [SerializeField] private TextMeshProUGUI spreadCost;
    [SerializeField] private TextMeshProUGUI weaponCost;
    [SerializeField] private TextMeshProUGUI fastTurretCost;
    [SerializeField] private TextMeshProUGUI rocketTurretCost;

    private WeaponUpgradeStats _weaponStats;
    private Weapon weaponPrefab;


    private void Start()
    {
        SetWeaponToRotation();

        _weaponStats = GameData.Instance.WeaponStats;
        UpdateButtonsAndMoney();
    }

    private void Update()
    {
        RotateWeaponPrefab();
    }

    private void RotateWeaponPrefab()
    {
        weaponPosition.transform.Rotate(Vector3.up, weaponRotationSpeed * Time.deltaTime);
    }

    public void UpgradeReloadTime()
    {
        GameData.Instance.RemoveMoney(_weaponStats.GetReloadUpgradeCost());
        _weaponStats.UpgradeReloadTime();
        UpdateButtonsAndMoney();
    }
    public void UpgradeSpread()
    {
        GameData.Instance.RemoveMoney(_weaponStats.GetSpreadUpgradeCost());
        _weaponStats.UpgradeSpread();
        UpdateButtonsAndMoney();
    }

    public void BuyNewWeapon()
    {
        GameData.Instance.SetWeaponToNext();
        Destroy(weaponPrefab.gameObject);
        SetWeaponToRotation();
        _weaponStats = GameData.Instance.WeaponStats;

        GameData.Instance.RemoveMoney(_weaponStats.CostToBuy);
        UpdateButtonsAndMoney();
    }

    private void UpdateButtonsAndMoney()
    {
        moneyCount.text = GameData.Instance.Money.ToString();

        reloadCost.text = _weaponStats.GetReloadUpgradeCost().ToString();
        if (_weaponStats.GetReloadUpgradeCost() > GameData.Instance.Money || _weaponStats.ReloadLevel > 10)
            reloadButton.interactable = false;
        else
            reloadButton.interactable = true;

        spreadCost.text = _weaponStats.GetSpreadUpgradeCost().ToString();
        if (_weaponStats.GetSpreadUpgradeCost() > GameData.Instance.Money || _weaponStats.SpreadLevel > 10)
            spreadButton.interactable = false;
        else
            spreadButton.interactable = true;

        if (GameData.Instance.NextWeaponExisting())
        {
            weaponCost.text = GameData.Instance.GetNextWeaponCost().ToString();
            if (GameData.Instance.GetNextWeaponCost() > GameData.Instance.Money)
                weaponButton.interactable = false;
            else
                weaponButton.interactable = true;
        }
        else
        {
            weaponCost.text = "MAX";
            weaponButton.interactable = false;
        }
    }

    private void SetWeaponToRotation()
    {
        weaponTitle.text = GameData.Instance.GetCurrentWeaponName();
        weaponPrefab = Instantiate(GameData.Instance.CurrentWeapon, weaponPosition);
        weaponPrefab.transform.localPosition = Vector3.zero + weaponPrefab.transform.forward / 2;
    }
}
