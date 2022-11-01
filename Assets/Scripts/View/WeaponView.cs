using UnityEngine;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Transform startShootPoint;

    private LineRenderer _laser;

    private void Awake()
    {
        _laser = GetComponent<LineRenderer>();
        _laser.positionCount = 2;
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
}
