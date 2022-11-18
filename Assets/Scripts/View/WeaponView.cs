using UnityEngine;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Transform startShootPoint;

    private LineRenderer _laser;
    private Animator _animator;
    private Weapon _weapon;

    private void Start()
    {
        _laser = GetComponent<LineRenderer>();
        _laser.positionCount = 2;

        _animator = GetComponentInChildren<Animator>();
        _weapon = GetComponent<Weapon>();

        _weapon.reloading.AddListener(SetAnimationToReload);
    }

    private void LateUpdate()
    {
        DrawLaser();
    }

    protected virtual void DrawLaser()
    {
        _laser.SetPosition(0, startShootPoint.position);

        if (Physics.Raycast(startShootPoint.position, GetWeaponLookDirection(), out var hit, 100))
        {
            _laser.SetPosition(1, hit.point);
        }

        else
        {
            _laser.SetPosition(1, GetWeaponLookDirection() * 300);
        }
    }

    protected Vector3 GetWeaponLookDirection()
    {
        return startShootPoint.forward;
    }

    private void SetAnimationToReload()
    {
        if (_animator == null)
            return;
        _animator.SetTrigger("Reload");
    }
}
