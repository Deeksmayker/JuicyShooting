using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;

    private Vector3 _firstTouchPosition;
    private Vector3 _currentTouchPosition;
    private float _xAngle;
    private float _yAngle;
    private float _xAngleTemp;
    private float _yAngleTemp;

    private void Awake()
    {
        _xAngle = 0;
        _yAngle = 0;
        transform.rotation = Quaternion.Euler(_yAngle, _xAngle, 0);
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
            _xAngleTemp = _xAngle;
            _yAngleTemp = _yAngle;
        }

        _currentTouchPosition = Input.mousePosition;

        _xAngle = _xAngleTemp + (_currentTouchPosition.x - _firstTouchPosition.x) * rotationSpeed / Screen.width;
        _yAngle = _yAngleTemp + (_currentTouchPosition.y - _firstTouchPosition.y) * rotationSpeed / Screen.height;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-_yAngle, _xAngle, 0.0f), Time.deltaTime * 20);
    }
}
