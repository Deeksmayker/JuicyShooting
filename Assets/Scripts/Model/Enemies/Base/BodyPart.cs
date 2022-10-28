using Assets.Scripts.Model.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BodyPart : MonoBehaviour, ISpreadParticles
{
    [SerializeField] private bool isWeakPoint;

    public void OnBulletHit(Vector3 bulletVelocity)
    {
        GetComponentInParent<Enemy>().OnHit(isWeakPoint);
        GetComponent<Rigidbody>().velocity = bulletVelocity;
    }

    public void SpreadParticles(Vector3 pos)
    {
        GetComponentInParent<Enemy>().SpreadParticles(isWeakPoint, pos);
    }
}
