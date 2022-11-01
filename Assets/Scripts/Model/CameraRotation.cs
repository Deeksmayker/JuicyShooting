using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;

    private Vector3 _firstTouchPosition;
    private Vector3 _currentTouchPosition;
    float xAngle;
    float yAngle;
    float xAngleTemp;
    float yAngleTemp;

    private void Awake()
    {
        xAngle = 0;
        yAngle = 0;
        transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
    }

    private void Update()
    {
        TurnInTouchAngle();
    }

    private void TurnInTouchAngle()
    {
        if (!Input.GetMouseButton(0))
        {
            // RotateWeaponToAngle();
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _firstTouchPosition = Input.mousePosition;
            xAngleTemp = xAngle;
            yAngleTemp = yAngle;
        }

        _currentTouchPosition = Input.mousePosition;

        xAngle = xAngleTemp + (_currentTouchPosition.x - _firstTouchPosition.x) * rotationSpeed / Screen.width;
        yAngle = yAngleTemp + (_currentTouchPosition.y - _firstTouchPosition.y) * rotationSpeed / Screen.height;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-yAngle, xAngle, 0.0f), Time.deltaTime * 20);
    }
}
