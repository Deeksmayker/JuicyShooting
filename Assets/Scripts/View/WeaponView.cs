using System.Collections;
using System.Linq;
using NTC.Global.Pool;
using UnityEngine;

public class WeaponView : MonoBehaviour
{
    [SerializeField] protected Transform barrelArmature;
    [SerializeField] private AudioSource shootAudio;

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

    private Vector3 _startLocalPosition;

    private GameInputHandler _input;

    private void Start()
    {
        _input = FindObjectOfType<GameInputHandler>();

        if (_input == null)
        {
            Debug.LogError("No GameInputHandler in scene, script will be destroyed");
            Destroy(this);
        }
        
        _lasers = GetComponentsInChildren<LineRenderer>();
        for (var i = 0; i < _lasers.Length; i++)
        {
            _lasers[i].positionCount = 2;
        }

        _animator = GetComponentInChildren<Animator>();
        _weapon = GetComponent<Weapon>();
        _weapon.ReloadStartedEvent.AddListener(OnReloadStarted);

        shootPointsPositions = GetComponentsInChildren<ShootPoint>().Select(shootPoint => shootPoint.transform).ToArray();

        _weapon.ReloadEndedEvent.AddListener(() => _animator.SetBool("Reload", false));

        _weapon.FiredEvent.AddListener(OnFired);
        _weapon.FireDelayEndedEvent.AddListener(() => _animator.SetBool("Fire", false));

        _targetRotation = transform.localRotation;

        _startLocalPosition = transform.localPosition;
        
        GameManager.OnWin.AddListener(() => Invoke(nameof(DisableGun), 1));
        GameManager.OnLose.AddListener(DisableGun);

        if (Screen.width > Screen.height)
        {
            swayMultiplier /= 5;
        }
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
        return _animator.GetBool("Reload") ? barrelArmature.up : transform.forward;
    }

    private void OnReloadStarted()
    {
        SetAnimationWithLength("Reload", "ReloadTime", _weapon.reloadTime);
    }

    private void SetAnimationWithLength(string animationParameterName, string animationLengthParameterName, float neededAnimationLength)
    {
        if (_animator == null)
            return;

        var parameterAnimation = _animator.runtimeAnimatorController.animationClips
            .Where(clip => clip.name.Contains(animationParameterName))
            .FirstOrDefault();

        if (parameterAnimation == default(AnimationClip))
            return;

        var animationLength = parameterAnimation.length;


        _animator.SetFloat(animationLengthParameterName, animationLength / neededAnimationLength);
        _animator.SetBool(animationParameterName, true);
    }

    private void OnFired()
    {
        if (shootAudio != null)
        {
            var a = NightPool.Spawn(shootAudio, transform.position, Quaternion.identity);
            a.Play();
            //NightPool.Despawn(a, 0.2f);
        }
        StartCoroutine(MakeRecoil());

        SetAnimationWithLength("Fire", "FireTime", _weapon.betweenShootDelay);
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
        var mouseX = _input.touchDelta.x * swayMultiplier;
        var mouseY = _input.touchDelta.y * swayMultiplier;

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
            transform.localPosition = Vector3.Slerp(_startLocalPosition, afterRecoilLocalPosition, t);

            timer -= Time.deltaTime;
            yield return null;
        }
        _recoiling = false;

        timer = recoilDuration;
        t = 0f;

        while (timer > 0)
        {
            t += Time.deltaTime / recoilDuration;

            transform.localPosition = Vector3.Slerp(afterRecoilLocalPosition, _startLocalPosition, t);

            timer -= Time.deltaTime;
            yield return null;
        }
    }

    private void DisableGun()
    {
        gameObject.SetActive(false);
    }
}
