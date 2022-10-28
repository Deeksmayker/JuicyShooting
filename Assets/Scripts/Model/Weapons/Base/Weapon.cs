using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet bulletPrefab;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float damage = 1;
    [SerializeField] protected Transform startShootPoint;
    [SerializeField] protected float reloadTime;

    [SerializeField] private float rotationSpeed;

    [SerializeField] private Camera cam; 

    protected float timeAfterShoot;

    protected LineRenderer laser;

    protected abstract bool CheckForShootInput();
    public abstract void Shoot();

    protected virtual void Awake()
    {
        laser = GetComponent<LineRenderer>();
        laser.positionCount = 2;
        laser.SetPosition(0, startShootPoint.position);

        timeAfterShoot = reloadTime;

        xAngle = 0;
        yAngle = 0;
        transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
        cam.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
    }

    protected virtual void Update()
    {
        timeAfterShoot += Time.deltaTime;
        TurnInTouchAngle();
        DrawLaser();

        if (CheckForShootInput())
            Shoot();
    }

    #region Weapon Turning Logic

    private Vector2 _deltaSum;

    private Vector3 _firstTouchPosition;
    private Vector3 _currentTouchPosition;
    float xAngle;
    float yAngle;
    float xAngleTemp;
    float yAngleTemp;

    private void TurnInTouchAngle()
    {
        if (Input.touchCount == 0)
        {
           // RotateWeaponToAngle();
            return;
        }

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _firstTouchPosition = Input.GetTouch(0).position;
            xAngleTemp = xAngle;
            yAngleTemp = yAngle;
        }

        if (Input.GetTouch(0).phase != TouchPhase.Moved)
            return;

        _currentTouchPosition = Input.GetTouch(0).position;

        xAngle = xAngleTemp + (_currentTouchPosition.x - _firstTouchPosition.x) * rotationSpeed / Screen.width;
        yAngle = yAngleTemp + (_currentTouchPosition.y - _firstTouchPosition.y) * rotationSpeed / Screen.height;
        transform.rotation = Quaternion.Lerp(transform.rotation,  Quaternion.Euler(-yAngle, xAngle, 0.0f), Time.deltaTime * 20);
        cam.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-yAngle, xAngle, 0.0f), Time.deltaTime * 20); ;
    }

    #endregion

    protected Vector3 GetWeaponLookDirection()
    {
        return transform.forward;
    }

    protected virtual void DrawLaser()
    {
        if (Physics.Raycast(startShootPoint.position, GetWeaponLookDirection(), out var hit, 100))
        {
            laser.SetPosition(1, hit.point);
        }

        else
        {
            laser.SetPosition(1, GetWeaponLookDirection() * 300);
        }
    }
}
