using System.Collections;
using System.Linq;
using UnityEngine;

public class WeaponView : MonoBehaviour
{
    [SerializeField] protected Transform barrelArmature;

    [Header("Sway settings")]
    [SerializeField] private float swaySmooth;
    [SerializeField] private float swayMultiplier;
    [Header("Recoil")]
    [SerializeField] private float recoilPower;
    [SerializeField] private float recoilDuration;
    [SerializeField] private float zRecoil, yRecoil;

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

        _weapon.ReloadEnded.AddListener(() => _animator.SetBool("Reloading", false));

        _weapon.Fired.AddListener(() => StartCoroutine(MakeRecoil()));

        _targetRotation = transform.localRotation;
    }

    private void Update()
    {
        MakeWeaponSway();
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
        return _animator.GetBool("Reloading") ? barrelArmature.up : transform.forward;
    }

    private void SetAnimationToReload()
    {
        if (_animator == null)
            return;

        var animationLength = _animator.runtimeAnimatorController.animationClips
            .Where((clip) => clip.name.Contains("Reload"))
            .First()
            .length;

        _animator.SetFloat("ReloadTime", animationLength / _weapon.reloadTime);
        _animator.SetBool("Reloading", true);
    }

    private Quaternion _targetRotation;
    private Quaternion _rotationX;
    private Quaternion _rotationY;

    private bool _recoiling;

    private void MakeWeaponSway()
    {
        if (_recoiling)
        {
            return;
        }
        var mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        var mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        _rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        _rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        _targetRotation = Quaternion.Slerp(_targetRotation, _rotationX * _rotationY, Mathf.Sqrt(swaySmooth * Time.deltaTime));

        transform.localRotation = Quaternion.Slerp(transform.localRotation, _targetRotation, swaySmooth * Time.deltaTime);
    }

    private IEnumerator MakeRecoil()
    {
        _recoiling = true;
        var timer = recoilDuration;
        var afterRecoilQuaternion = Quaternion.Euler(_targetRotation.x - recoilPower, _targetRotation.y, _targetRotation.z);
        var afterRecoilLocalPosition = transform.localPosition + new Vector3(0, yRecoil, -zRecoil);
        var t = 0f;

        while (timer > 0)
        {
            t += Time.deltaTime / recoilDuration;

            transform.localRotation = Quaternion.Slerp(_targetRotation, afterRecoilQuaternion, t);
            transform.localPosition = Vector3.Slerp(Vector3.zero, afterRecoilLocalPosition, t);

            timer -= Time.deltaTime;
            yield return null;
        }
        _recoiling = false;

        timer = recoilDuration;
        t = 0f;

        while (timer > 0)
        {
            t += Time.deltaTime / recoilDuration;

            transform.localPosition = Vector3.Slerp(afterRecoilLocalPosition, Vector3.zero, t);

            timer -= Time.deltaTime;
            yield return null;
        }
    }
}
