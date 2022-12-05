using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float speed = 5;

    [SerializeField] private float health;
    [SerializeField] private int weakPointsCount;

    [SerializeField] private int moneyByKill, moneyByKillInWeakPoint;

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
        {
            if (isWeakPoint)
            {
                EnemyDiedByWeakPoint.Invoke();
                GameData.Instance.AddMoney(moneyByKillInWeakPoint);
            }

            else
            {
                EnemyDied.Invoke();
                GameData.Instance.AddMoney(moneyByKill);
            }
            Die();
        }
    }

    public void Die()
    {
        _ch.enabled = false;
        _dead = true;
        Utils.EnableRagdoll(gameObject);
        EnemyDied.Invoke();
    }

   
}
