using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Animation anim;

    private LineRenderer[] _lasers;
    private Animator _animator;
    private Weapon _weapon;
    private Transform[] shootPointsPositions;

    private void Start()
    {
        _lasers = GetComponentsInChildren<LineRenderer>();
        for (var i = 0; i < _lasers.Length; i++)
        {
            _lasers[i].positionCount = 2;
        }

        _animator = GetComponentInChildren<Animator>();
        _weapon = GetComponent<Weapon>();
        _weapon.ReloadStarted.AddListener(SetAnimationToReload);

        shootPointsPositions = GetComponentsInChildren<ShootPoint>().Select(shootPoint => shootPoint.transform).ToArray();
    }

    private void LateUpdate()
    {
        DrawLaser();
    }

    protected virtual void DrawLaser()
    {
        for (var i = 0; i < _lasers.Length; i++)
        {
            _lasers[i].SetPosition(0, shootPointsPositions[i].position);

            if (Physics.Raycast(shootPointsPositions[i].position, GetWeaponLookDirection(), out var hit, 100))
            {
                _lasers[i].SetPosition(1, hit.point);
            }

            else
            {
                _lasers[i].SetPosition(1, GetWeaponLookDirection() * 300);
            }
        }

        
    }

    protected Vector3 GetWeaponLookDirection()
    {
        return transform.forward;
    }

    private void SetAnimationToReload()
    {
        Debug.Log("SDF");
        //anim["Armature|Reload"].time = _weapon.reloadTime;
        anim.Play();

        if (_animator == null)
            return;
        _animator.SetTrigger("Reload");
    }
}
