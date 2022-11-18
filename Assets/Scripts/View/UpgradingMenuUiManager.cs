using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradingMenuUiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponTitle;
    [SerializeField] private Weapon weaponPrefab;
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

    private void Start()
    {
        weaponTitle.text = GameData.Instance.WeaponName;
        weaponPrefab = Instantiate(GameData.Instance.CurrentWeapon, weaponPosition);
        weaponPrefab.transform.localPosition = Vector3.zero + weaponPrefab.transform.forward / 2;

        _weaponStats = GameData.Instance.WeaponUpgradeStats;
        UpdateButtonsAndMoney(GameData.Instance.Money);

        GameData.Instance.moneyValueChanged.AddListener(UpdateButtonsAndMoney);
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
    }
    public void UpgradeSpread()
    {
        GameData.Instance.RemoveMoney(_weaponStats.GetSpreadUpgradeCost());
        _weaponStats.UpgradeSpread();
    }

    private void UpdateButtonsAndMoney(int newMoneyValue)
    {
        moneyCount.text = newMoneyValue.ToString();

        reloadCost.text = _weaponStats.GetReloadUpgradeCost().ToString();
        if (_weaponStats.GetReloadUpgradeCost() > newMoneyValue || _weaponStats.ReloadLevel > 10)
            reloadButton.interactable = false;
        else
            reloadButton.interactable = true;

        spreadCost.text = _weaponStats.GetSpreadUpgradeCost().ToString();
        if (_weaponStats.GetSpreadUpgradeCost() > newMoneyValue || _weaponStats.SpreadLevel > 10)
            spreadButton.interactable = false;
        else
            spreadButton.interactable = true;
    }
}
