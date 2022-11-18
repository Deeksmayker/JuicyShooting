using Assets.Scripts.Model.Interfaces;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float damage = 1;

    private float _maxLifeTime = 10;
    private float _lifeTime = 0;

    private Vector3 _lastFrameVelocity;

    [HideInInspector] public Rigidbody Rb;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _lifeTime += Time.deltaTime;
        if (_lifeTime > _maxLifeTime)
            Destroy(gameObject);
    }

    private void LateUpdate()
    {
        _lastFrameVelocity = Rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BodyPart>(out var bodyPart))
        {
            bodyPart.OnBulletHit(damage, _lastFrameVelocity);
        }

        foreach (var spreadedParticles in collision.gameObject.GetComponents<MonoBehaviour>().OfType<ISpreadParticles>())
        {
            spreadedParticles.SpreadParticles(transform.position);
        }

        Destroy(gameObject);
    }
}
