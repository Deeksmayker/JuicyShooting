using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet bulletPrefab;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float damage = 1;
    [SerializeField] protected Transform startShootPoint;
    [SerializeField] protected float reloadTime;

    [SerializeField] private float rotationSpeed;

    protected float timeAfterShoot;

    protected LineRenderer laser;

    protected abstract bool CheckForShootInput();
    public abstract void Shoot();

    protected virtual void Awake()
    {
        laser = GetComponent<LineRenderer>();
        laser.positionCount = 2;
        laser.SetPosition(0, startShootPoint.position);
        
        timeAfterShoot = reloadTime;
    }

    protected virtual void Update()
    {
        timeAfterShoot += Time.deltaTime;
        TurnInTouchAngle();
        DrawLaser();

        if (CheckForShootInput())
            Shoot();
    }

    #region Weapon Turning Logic

    private Quaternion _angleToRotate;

    private void TurnInTouchAngle()
    {
        if (!Input.GetMouseButton(0))
        {
            RotateWeaponToAngle();
            return;
        }
        var mousePos = Input.mousePosition;
        Ray aimObject = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(aimObject, out var hit, 100f, 0))
        {
            _angleToRotate = Quaternion.LookRotation(hit.distance * aimObject.direction);
            RotateWeaponToAngle();
        }

        else
        {
            _angleToRotate = Quaternion.LookRotation(aimObject.direction);
            RotateWeaponToAngle();
        }
    }

    private void RotateWeaponToAngle()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _angleToRotate, Time.deltaTime * rotationSpeed);
    }

    #endregion

    protected Vector3 GetWeaponLookDirection()
    {
        return transform.forward;
    }

    protected virtual void DrawLaser()
    {
        if (Physics.Raycast(startShootPoint.position, GetWeaponLookDirection(), out var hit, 100))
        {
            laser.SetPosition(1, hit.point);
        }

        else
        {
            laser.SetPosition(1, GetWeaponLookDirection() * 300);
        }
    }
}
