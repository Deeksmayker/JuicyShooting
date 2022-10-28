using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] private int fps;


    private void Update()
    {
        Application.targetFrameRate = fps;
    }
}
