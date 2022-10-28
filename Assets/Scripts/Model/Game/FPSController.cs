using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    [SerializeField] private int fps;
    [SerializeField] private Text fpsCounter;


    private void Update()
    {
        Application.targetFrameRate = fps;
        fpsCounter.text = (int)(1.0f / Time.deltaTime) + "";
    }
}
