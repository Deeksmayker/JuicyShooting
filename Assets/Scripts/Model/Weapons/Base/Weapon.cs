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

    protected float timeAfterShoot;

    protected LineRenderer laser;

    protected abstract bool CheckForShootInput();
    public abstract void Shoot();

    protected virtual void Awake()
    {
        laser = GetComponent<LineRenderer>();
        laser.positionCount = 2;

        timeAfterShoot = reloadTime;
    }

    protected virtual void Update()
    {
        timeAfterShoot += Time.deltaTime;
        DrawLaser();

        if (CheckForShootInput())
            Shoot();
    }

    protected Vector3 GetWeaponLookDirection()
    {
        return transform.forward;
    }

    protected virtual void DrawLaser()
    {
        laser.SetPosition(0, startShootPoint.position);

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
