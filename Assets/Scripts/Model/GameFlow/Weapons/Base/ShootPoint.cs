using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class ShootPoint : MonoBehaviour
{
    [SerializeField] private bool laserDisabledOnReload;

    private LineRenderer _lr;

    private Weapon _parentWeapon;

    public UnityEvent EnemyDetected = new();

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();

        _parentWeapon = GetComponentInParent<Weapon>();
        if (laserDisabledOnReload)
        {
            _parentWeapon.ReloadStartedEvent.AddListener(() => _lr.enabled = false);
            _parentWeapon.ReloadEndedEvent.AddListener(() => _lr.enabled = true);
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        EnemyDetected.Invoke();
    }
}
