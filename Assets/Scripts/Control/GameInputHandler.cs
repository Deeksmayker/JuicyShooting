using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputHandler : MonoBehaviour
{
    public Vector2 touchPosition;
    public Vector2 touchDelta;
    public bool isTouched;
    public bool touching;

    private void LateUpdate()
    {
        isTouched = false;
    }

    public void OnTouchPosition(InputValue value)
    {
        touchPosition = value.Get<Vector2>();
    }
    
    public void OnTouchDelta(InputValue value)
    {
        touchDelta = value.Get<Vector2>();
    }
    
    public void OnTouched(InputValue value)
    {
        isTouched = value.isPressed;
    }

    public void OnTouching(InputValue value)
    {
        touching = value.isPressed;
    }
}
