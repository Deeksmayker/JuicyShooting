using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradingMenuUiManager : MonoBehaviour
{
    [Header("Rotating weapon")]
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private float weaponRotationSpeed;

    [Header("Base")]
    [SerializeField] private TextMeshProUGUI weaponTitle;
    [SerializeField] private TextMeshProUGUI moneyCount;

    [Header("Weapon Buttons")]
    [SerializeField] private Button reloadButton;
    [SerializeField] private Button spreadButton;
    [SerializeField] private Button weaponButton;

    [Header("Weapon Text fields")]
    [SerializeField] private TextMeshProUGUI reloadCost;
    [SerializeField] private TextMeshProUGUI spreadCost;
    [SerializeField] private TextMeshProUGUI weaponCost;

    [Header("Perks Buttons")]
    [SerializeField] private Button buyDualButton;
    [SerializeField] private Button buyExplosiveButton;
    [SerializeField] private Button dualDurationButton;
    [SerializeField] private Button dualUsesButton;
    [SerializeField] private Button explosiveFrequencyButton;
    [SerializeField] private Button explosiveRadiusButton;

    [Header("Perks Text fields")]
    [SerializeField] private TextMeshProUGUI buyDualCost;
    [SerializeField] private TextMeshProUGUI buyExplosiveCost;
    [SerializeField] private TextMeshProUGUI dualDurationCost;
    [SerializeField] private TextMeshProUGUI dualUsesCost;
    [SerializeField] private TextMeshProUGUI explosiveFrequencyCost;
    [SerializeField] private TextMeshProUGUI explosiveRadiusCost;

    private WeaponUpgradeStats _weaponStats;
    private Weapon weaponPrefab;

    private void Start()
    {
        SetWeaponToRotation();

        _weaponStats = GameData.Instance.WeaponStats;
        UpdateButtonsAndMoney();
        CheckPerksButtonsVisibility();
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

    public void BuyDualPerk()
    {
        GameData.Instance.RemoveMoney(GameData.Instance.PlayerDualPerk.BuyPerkCost);
        GameData.Instance.PlayerDualPerk.BuyDualPerk();

        CheckPerksButtonsVisibility();
        UpdateButtonsAndMoney();
    }

    public void UpgradeDualDuration()
    {
        GameData.Instance.RemoveMoney(GameData.Instance.PlayerDualPerk.GetDurationUpgradeCost());
        GameData.Instance.PlayerDualPerk.UpgradeDuration();
        UpdateButtonsAndMoney();
    }
    public void UpgradeDualUses()
    {
        GameData.Instance.RemoveMoney(GameData.Instance.PlayerDualPerk.GetUsesCountUpgradeCost());
        GameData.Instance.PlayerDualPerk.UpgradeUsesCount();
        UpdateButtonsAndMoney();
    }

    public void BuyExplosionPerk()
    {
        GameData.Instance.RemoveMoney(GameData.Instance.PlayerExplosionPerk.BuyPerkCost);
        GameData.Instance.PlayerExplosionPerk.BuyExplosionPerk();

        CheckPerksButtonsVisibility();
        UpdateButtonsAndMoney();
    }

    public void UpgradeExplosionFrequency()
    {
        GameData.Instance.RemoveMoney(GameData.Instance.PlayerExplosionPerk.GetFrequencyUpgradeCost());
        GameData.Instance.PlayerExplosionPerk.UpgradeFrequency();
        UpdateButtonsAndMoney();
    }
    public void UpgradeExplosionRadius()
    {
        GameData.Instance.RemoveMoney(GameData.Instance.PlayerExplosionPerk.GetRadiusUpgradeCost());
        GameData.Instance.PlayerExplosionPerk.UpgradeRadius();
        UpdateButtonsAndMoney();
    }

    private void UpdateButtonsAndMoney()
    {
        UpdateMoneyText();

        UpdateButtonWithCost(reloadCost, _weaponStats.GetReloadUpgradeCost(), _weaponStats.ReloadLevel, reloadButton);

        UpdateButtonWithCost(spreadCost, _weaponStats.GetSpreadUpgradeCost(), _weaponStats.SpreadLevel, spreadButton);

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

        if (GameData.Instance.PlayerDualPerk.PerkAvaliable())
        {
            UpdateButtonWithCost(dualDurationCost, GameData.Instance.PlayerDualPerk.GetDurationUpgradeCost(),
                GameData.Instance.PlayerDualPerk.DurationLevel, dualDurationButton);
            UpdateButtonWithCost(dualUsesCost, GameData.Instance.PlayerDualPerk.GetUsesCountUpgradeCost(),
                GameData.Instance.PlayerDualPerk.UsesCount, dualUsesButton, 3);
        }

        if (GameData.Instance.PlayerExplosionPerk.PerkAvaliable())
        {
            UpdateButtonWithCost(explosiveFrequencyCost, GameData.Instance.PlayerExplosionPerk.GetFrequencyUpgradeCost(),
                GameData.Instance.PlayerExplosionPerk.FrequencyLevel, explosiveFrequencyButton);
            UpdateButtonWithCost(explosiveRadiusCost, GameData.Instance.PlayerExplosionPerk.GetRadiusUpgradeCost(),
                GameData.Instance.PlayerExplosionPerk.RadiusLevel, explosiveRadiusButton);
        }
    }

    private void CheckPerksButtonsVisibility()
    {
        SetPerkBuyButton(buyDualButton, buyDualCost, GameData.Instance.PlayerDualPerk, new[] { dualDurationButton, dualUsesButton });
        SetPerkBuyButton(buyExplosiveButton, buyExplosiveCost, GameData.Instance.PlayerExplosionPerk, new[] { explosiveFrequencyButton, explosiveRadiusButton });
    }

    private void SetPerkBuyButton(Button buyButton, TextMeshProUGUI buyPerkText, PlayerPerk perk, params Button[] upgradePerkButtons)
    {
        if (!perk.PerkAvaliable())
        {
            foreach (var b in upgradePerkButtons)
                b.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
            buyPerkText.text = perk.BuyPerkCost.ToString();

            if (perk.BuyPerkCost > GameData.Instance.Money)
                buyButton.interactable = false;

            return;
        }

        foreach (var b in upgradePerkButtons)
            b.gameObject.SetActive(true);
        buyButton.gameObject.SetActive(false);
        return;
    }

    private void UpdateButtonWithCost(TextMeshProUGUI costText, int upgradeCost, int level, Button upgradeButton, int maxLevel = 10)
    {
        costText.text = upgradeCost.ToString();
        if (level > maxLevel - 1)
            costText.text = "MAX";
        if (upgradeCost > GameData.Instance.Money || level > maxLevel - 1)
            upgradeButton.interactable = false;
        else
            upgradeButton.interactable = true;
    }

    private void UpdateMoneyText()
    {
        moneyCount.text = GameData.Instance.Money.ToString();
    }

    private void SetWeaponToRotation()
    {
        weaponTitle.text = GameData.Instance.GetCurrentWeaponName();
        weaponPrefab = Instantiate(GameData.Instance.CurrentWeapon, weaponPosition);
        weaponPrefab.transform.localPosition = Vector3.zero + weaponPrefab.transform.forward / 2;
    }
}
