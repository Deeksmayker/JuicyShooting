using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Destroy(gameObject);
    }
}
