using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 5;

    [SerializeField] private float health;
    [SerializeField] private int weakPointsCount;
    [SerializeField] private ParticleSystem smallParticles, hugeParticles;

    private Vector3 _pointToGo;

    private CharacterController _ch;
    private bool _dead;

    private void Awake()
    {
        Utils.DisableRagdoll(gameObject);
        _pointToGo = FindObjectOfType<Barricade>().transform.position + Utils.GetRandomHorizontalVector(0.2f);
        _pointToGo.y = GameObject.FindWithTag("GroundPosition").transform.position.y;
        _ch = GetComponent<CharacterController>();

        speed *= Random.Range(0.7f, 1.3f);
    }

    private void Update()
    {
        if (_dead)
            return;
        var direction = (_pointToGo - transform.position).normalized;
        direction.y = -1;
        _ch.Move(direction * speed * Time.deltaTime);
        transform.LookAt(_pointToGo, Vector3.up);
    }

    public void OnHit(bool isWeakPoint, float damage)
    {
        health -= damage;

        if (isWeakPoint)
        {
            StartCoroutine(Utils.SlowTime(0.5f, 0.3f));
            weakPointsCount -= 1;
        }

        if (weakPointsCount <= 0 || health <= 0)
            Die();
    }

    private void Die()
    {
        _ch.enabled = false;
        _dead = true;
        Utils.EnableRagdoll(gameObject);
    }

    public void SpreadParticles(bool weakPoint, Vector3 pos)
    {
        Instantiate(weakPoint ? hugeParticles : smallParticles, pos, Quaternion.identity);
    }
}
