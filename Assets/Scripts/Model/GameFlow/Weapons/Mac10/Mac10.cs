public class Mac10 : Weapon
{
    public override void Shoot()
    {
        for (var i = 0; i < shootPointsPositions.Length; i++)
        {
            ShootBulletWithSpread(shootPointsPositions[i].position);
        }
    }
}
