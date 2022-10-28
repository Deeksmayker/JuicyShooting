using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    protected override bool CheckForShootInput()
    {
        return Input.touchCount >= 1 && Input.touches[0].phase == TouchPhase.Ended && timeAfterShoot >= reloadTime;  
    }

    public override void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, startShootPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = GetWeaponLookDirection() * bulletSpeed;
        bullet.damage = damage;
        timeAfterShoot = 0;
    }
}
