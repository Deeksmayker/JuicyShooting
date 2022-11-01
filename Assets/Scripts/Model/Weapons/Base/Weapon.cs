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
    [SerializeField] protected LayerMask layersToShoot;

    protected float timeAfterShoot;

    
    public abstract void Shoot();

    protected virtual void Awake()
    {
        timeAfterShoot = reloadTime;
    }

    protected virtual void Update()
    {
        timeAfterShoot += Time.deltaTime;

        if (CheckForShoot())
            Shoot();
    }

    private bool CheckForShoot()
    {
        if (Physics.Raycast(startShootPoint.position, GetWeaponLookDirection(), 100f, layersToShoot))
        {
            return timeAfterShoot >= reloadTime && Input.GetMouseButton(0);
        }
        return false;
    }

    protected Vector3 GetWeaponLookDirection()
    {
        return transform.forward;
    }
}
