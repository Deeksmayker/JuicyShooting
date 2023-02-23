using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float speed = 5;

    [SerializeField] private float health;
    [SerializeField] private int weakPointsCount;

    private Vector3 _pointToGo;

    private CharacterController _ch;
    private bool _dead;

    public UnityEvent hited = new();
    public UnityEvent hitedInWeakPoint = new();

    public static UnityEvent EnemyDied = new();
    public static UnityEvent EnemyDiedByWeakPoint = new();

    private void Start()
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
        direction.y = -10;
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
        {
            if (isWeakPoint)
            {
                EnemyDiedByWeakPoint.Invoke();
                //GameData.Instance.HandleKill(true);
            }

            else
            {
               // GameData.Instance.HandleKill(false);
            }
            Die();
        }
    }

    [ContextMenu("Kill Enemy")]
    public void Die()
    {
        if (_dead)
            return;
        
        _ch.enabled = false;
        _dead = true;
        Utils.EnableRagdoll(gameObject);
        EnemyDied.Invoke();
        
        Invoke(nameof(DisableComponentsAfterDeath), Random.Range(5, 15));
    }

    private void DisableComponentsAfterDeath()
    {
        var joints = GetComponentsInChildren<Joint>();
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        var colliders = GetComponentsInChildren<Collider>();
        var scripts = GetComponentsInChildren<MonoBehaviour>();
        
        for (var i = 0; i < joints.Length; i++)
        {
            Destroy(joints[i]);
        }
        
        for (var i = 0; i < rigidbodies.Length; i++)
        {
            Destroy(rigidbodies[i]);
        }
        
        for (var i = 0; i < colliders.Length; i++)
        {
            Destroy(colliders[i]);
        }
        
        for (var i = 0; i < scripts.Length; i++)
        {
            Destroy(scripts[i]);
        }
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speed *= multiplier;
        GetComponent<Animator>().SetFloat("Speed", multiplier);
    }
}
