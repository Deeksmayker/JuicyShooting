using Assets.Scripts.Model.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class BodyPart : MonoBehaviour, ISpreadParticles
{
    [SerializeField] private bool isWeakPoint;

    public static UnityEvent<Vector3> EnemyDamagedInWeakPointGlobalEvent = new();

    public void OnBulletHit(float damage, Vector3 bulletVelocity, Vector3 hitPosition)
    {
        if (isWeakPoint)
        {
            EnemyDamagedInWeakPointGlobalEvent.Invoke(hitPosition);
        }

        GetComponentInParent<Enemy>().OnHit(isWeakPoint, damage);
        GetComponent<Rigidbody>().velocity = bulletVelocity;
    }

    public void OnExplosion()
    {
        GetComponentInParent<Enemy>().Die();
    }

    public void SpreadParticles(Vector3 pos)
    {
        GetComponentInParent<EnemyView>().SpreadParticles(isWeakPoint, pos);
    }
}
