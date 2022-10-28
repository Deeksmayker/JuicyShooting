using Assets.Scripts.Model.Interfaces;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private float _maxLifeTime = 10;
    private float _lifeTime = 0;

    private void Update()
    {
        _lifeTime += Time.deltaTime;
        if (_lifeTime > _maxLifeTime)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BodyPart>(out var bodyPart))
        {
            bodyPart.OnBulletHit(GetComponent<Rigidbody>().velocity);
        }

        foreach (var spreadedParticles in collision.gameObject.GetComponents<MonoBehaviour>().OfType<ISpreadParticles>())
        {
            spreadedParticles.SpreadParticles(transform.position);
        }

        Destroy(gameObject);
    }
}
