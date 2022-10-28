using Assets.Scripts.Model.Interfaces;
using UnityEngine;


public class BodyPart : MonoBehaviour, ISpreadParticles
{
    [SerializeField] private bool isWeakPoint;

    public void OnBulletHit(float damage, Vector3 bulletVelocity)
    {
        GetComponentInParent<Enemy>().OnHit(isWeakPoint, damage);
        GetComponent<Rigidbody>().velocity = bulletVelocity;
    }

    public void SpreadParticles(Vector3 pos)
    {
        GetComponentInParent<Enemy>().SpreadParticles(isWeakPoint, pos);
    }
}
