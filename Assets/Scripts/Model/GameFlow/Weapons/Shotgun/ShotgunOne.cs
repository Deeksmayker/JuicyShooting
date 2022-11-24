using UnityEngine;

public class ShotgunOne : Weapon
{
    [SerializeField] private int bulletsPerShootCount;

    public override void Shoot()
    {
        for (var i = 0; i < shootPointsPositions.Length; i++)
        {
            for (var j = 0; j < bulletsPerShootCount; j++)
            {
                ShootBulletWithSpread(shootPointsPositions[i].position);
            }
        }
    }
}
