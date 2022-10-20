using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void Awake()
    {
        Utils.DisableRagdoll(gameObject);
    }

    public void OnHit(bool isWeakPoint)
    {
        Utils.EnableRagdoll(gameObject);
    }
}
