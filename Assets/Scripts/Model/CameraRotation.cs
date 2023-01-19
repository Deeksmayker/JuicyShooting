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

    private GameInputHandler _input;

    private void Awake()
    {
        _input = FindObjectOfType<GameInputHandler>();

        if (_input == null)
        {
            Debug.LogError("No GameInputHandler in scene, script will be destroyed");
            Destroy(this);
        }
        
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
        if (!_input.touching)
        {
            // RotateWeaponToAngle();
            return;
        }

        if (_input.isTouched)
        {
            _firstTouchPosition = _input.touchPosition;
            _xAngleTemp = _xAngle;
            _yAngleTemp = _yAngle;
        }

        _currentTouchPosition = _input.touchPosition;

        _xAngle = _xAngleTemp + (_currentTouchPosition.x - _firstTouchPosition.x) * rotationSpeed / Screen.width;
        _yAngle = _yAngleTemp + (_currentTouchPosition.y - _firstTouchPosition.y) * rotationSpeed / Screen.height;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-_yAngle, _xAngle, 0.0f), Time.deltaTime * 20);
    }
}
