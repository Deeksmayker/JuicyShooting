using System;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class SpotLightEnemyDetector : MonoBehaviour
{
    [SerializeField] private LayerMask layersToDetect;

    private Light _light;
    private int _enemiesInRange;

    private void Start()
    {
        _light = GetComponent<Light>();

        _light.enabled = false;
    }

    private void LateUpdate()
    {
        if (_enemiesInRange > 0 && !_light.enabled)
            _light.enabled = true;
        if (_enemiesInRange <= 0 && _light.enabled)
        {
            Invoke(nameof(DisableLight), 0.5f);
        }

        /*var pos = transform.position;

        var enemiesInRadius =
            Physics.OverlapBox(new Vector3(pos.x, pos.y + 2.5f, pos.z + 15f), new Vector3(7f, 15f, 30f), transform.rotation, layersToDetect);

        if (enemiesInRadius.Length > 0 && !_light.enabled)
            _light.enabled = true;
        if (enemiesInRadius.Length == 0 && _light.enabled)
            _light.enabled = false;*/
        _enemiesInRange = 0;
    }

    private void DisableLight()
    {
        if (_enemiesInRange > 0)
            return;
        
        _light.enabled = false;
    }

    void OnTriggerStay(Collider other)
    {
        _enemiesInRange++;
    }
}
