using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet bulletPrefab;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float damage = 1;
    [SerializeField] protected Transform startShootPoint;
    public float reloadTime;
    [Tooltip("Разброс оружия. 0 - без разброса"), Min(0)] public float spread;
    [SerializeField] protected LayerMask layersToShoot;

    protected float timeAfterShoot;

    public UnityEvent fired = new();
    public UnityEvent reloading = new();

    public abstract void Shoot();

    protected virtual void Awake()
    {
        timeAfterShoot = reloadTime;
    }

    protected virtual void Update()
    {
        timeAfterShoot += Time.deltaTime;
    }

    private void CheckInputAndReloadTimeAndShoot()
    {
        if (timeAfterShoot >= reloadTime && Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        CheckInputAndReloadTimeAndShoot();
    }

    protected Vector3 GetWeaponLookDirection()
    {
        return transform.forward;
    }

    protected void ShootBulletWithSpread()
    {
        var randomNumberX = Random.Range(-spread, spread);
        var randomNumberY = Random.Range(-spread, spread);
        var randomNumberZ = Random.Range(-spread, spread);

        var bullet = Instantiate(bulletPrefab, startShootPoint.position, transform.rotation);
        bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
        bullet.Rb.velocity = bullet.transform.forward * bulletSpeed;
        bullet.damage = damage;
        timeAfterShoot = 0;
    }
}
 