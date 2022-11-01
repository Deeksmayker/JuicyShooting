using UnityEngine;

public class Pistol : Weapon
{
    public override void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, startShootPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = GetWeaponLookDirection() * bulletSpeed;
        bullet.damage = damage;
        timeAfterShoot = 0;
    }
}
