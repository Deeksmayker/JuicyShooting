using Assets.Scripts.Model.Interfaces;
using System.Linq;
using NTC.Global.Pool;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float damage = 1;

    /*private float _maxLifeTime = 3;
    private float _lifeTime = 0;*/

    private Vector3 _lastFrameVelocity;

    [HideInInspector] public Rigidbody Rb;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }

    /*private void Update()
    {
        _lifeTime += Time.deltaTime;
        /*if (_lifeTime > _maxLifeTime)
            NightPool.Despawn(this);#1#
    }*/

    private void LateUpdate()
    {
        _lastFrameVelocity = Rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BodyPart>(out var bodyPart))
        {
            bodyPart.OnBulletHit(damage, _lastFrameVelocity, transform.position);
        }

        foreach (var spreadedParticles in collision.gameObject.GetComponents<MonoBehaviour>().OfType<ISpreadParticles>())
        {
            spreadedParticles.SpreadParticles(transform.position);
        }

        NightPool.Despawn(this);
    }
}
