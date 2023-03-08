using System;
using NTC.Global.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private ParticleSystem smallParticles, hugeParticles;
    [SerializeField] private AudioSource normalHitSound, heavyHitSound;
    
    [Header("Popup")]
    [SerializeField] private HitPopup hitPopup;

    [SerializeField] private Color colorOnWeakPoint;
    //[SerializeField] private string textOnWeakPoint;
    [SerializeField] private Color colorOnNormalPoint;
    //[SerializeField] private string textOnNormalPoint;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
        _animator.SetInteger("AnimationNumber", Random.Range(1, 4));
    }

    public void MakeVisualEffectsOnHit(bool weakPoint, Vector3 pos)
    {
        SpreadParticles(weakPoint, pos);
        ShowHitPopup(weakPoint, pos);

        var a = NightPool.Spawn(weakPoint ? heavyHitSound : normalHitSound, pos, Quaternion.identity);
        a.Play();
    }

    private void SpreadParticles(bool weakPoint, Vector3 pos)
    {
        NightPool.Spawn(weakPoint ? hugeParticles : smallParticles, pos, Quaternion.identity);
    }


    private void ShowHitPopup(bool weakPoint, Vector3 pos)
    {
        var popup = NightPool.Spawn(hitPopup);
        popup.transform.position = pos;
        popup.SetupPopup(weakPoint ? Color.red : Color.white);
    }
}
