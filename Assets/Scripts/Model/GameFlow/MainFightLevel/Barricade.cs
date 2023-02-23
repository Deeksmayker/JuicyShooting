using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.OnLose.Invoke();
    }
}
