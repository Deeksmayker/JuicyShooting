using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    private bool _alreadyLose;

    private void OnTriggerEnter(Collider other)
    {
        if (_alreadyLose)
            return;
    
        _alreadyLose = true;
        GameManager.OnLose.Invoke();
    }
}
