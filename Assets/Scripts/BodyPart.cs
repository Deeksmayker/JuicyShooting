using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BodyPart : MonoBehaviour
{
    [SerializeField] private bool isWeakPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() == null)
            return;

        GetComponentInParent<Enemy>().OnHit(isWeakPoint);
        GetComponent<Rigidbody>().velocity = collision.gameObject.GetComponent<Rigidbody>().velocity;
    }
}
