using UnityEngine;

[RequireComponent(typeof(WeaponSpawner))]
public class DualPerkInGameHandler : MonoBehaviour
{
    public int DualUsesCount { get; private set; }

    private Weapon _secondHandWeapon;

    private void Awake()
    {
        DualUsesCount = GameData.Instance.PlayerDualPerk.UsesCount;

        if (DualUsesCount == 0)
            Destroy(this);
    }

    public void EnableDualPerk()
    {
        DualUsesCount--;
        _secondHandWeapon = Instantiate(GameData.Instance.CurrentWeapon, gameObject.transform);
        _secondHandWeapon.transform.localPosition = new Vector3(-transform.localPosition.x * 2, 0, 0);
        _secondHandWeapon.transform.localScale = new Vector3(-1, 1, 1);

        Invoke(nameof(DisableDualPerk), GameData.Instance.PlayerDualPerk.GetCurrentDuration());
    }

    private void DisableDualPerk()
    {
        Destroy(_secondHandWeapon.gameObject);
    }
}
