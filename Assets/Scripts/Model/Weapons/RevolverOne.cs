using UnityEngine;

public class RevolverOne : Weapon
{
    public override void Shoot()
    {
        ShootBulletWithSpread();
    }
}
