using UnityEngine;

public class RevolverOne : Weapon
{
    public override void Shoot()
    {
        for (var i = 0; i < shootPointsPositions.Length; i++)
        {
            ShootBulletWithSpread(shootPointsPositions[i].position);
        }
    }
}
