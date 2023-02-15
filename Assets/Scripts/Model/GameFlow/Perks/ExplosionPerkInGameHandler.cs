using System.Collections;
using UnityEngine;

public class ExplosionPerkInGameHandler : MonoBehaviour
{
    [SerializeField] private float explosionPushPower;

    [SerializeField] private float timeToChangeEffectColor;
    //[SerializeField] private GameObject explodeRadiusEffect;
    [SerializeField] private ParticleSystem explosionParticles;

    private int _hitsBeforeExplosion;

    private void Start()
    {
        if (!GameData.Instance.PlayerExplosionPerk.PerkAvaliable())
            Destroy(this);

        _hitsBeforeExplosion = GameData.Instance.PlayerExplosionPerk.GetCurrentFrequency();

        BodyPart.EnemyDamagedInWeakPointGlobalEvent.AddListener(OnWeakPointHit);
    }

    private void OnWeakPointHit(Vector3 hitPosition)
    {
        //Debug.Log(_hitsBeforeExplosion);
        _hitsBeforeExplosion--;

        if (_hitsBeforeExplosion > 0)
            return;

        _hitsBeforeExplosion = GameData.Instance.PlayerExplosionPerk.GetCurrentFrequency();
        MakeExplosion(hitPosition);
    }

    private void MakeExplosion(Vector3 startPos)
    {
        MakeExplosionEffect(startPos);

        var targetsInRadius = Physics.OverlapSphere(startPos, GameData.Instance.PlayerExplosionPerk.GetCurrentRadius());

        foreach (var target in targetsInRadius)
        {
            if (target.TryGetComponent<BodyPart>(out var bodyPart))
            {
                bodyPart.OnExplosion();
            }

            if (target.TryGetComponent<Rigidbody>(out var rb))
            {
                //Debug.Log("finds rb");
                rb.AddExplosionForce(explosionPushPower, startPos, GameData.Instance.PlayerExplosionPerk.GetCurrentRadius());
            }
        }
    }

    private void MakeExplosionEffect(Vector3 pos)
    {
        /*var diameter = GameData.Instance.PlayerExplosionPerk.GetCurrentRadius() * 2;
        var radiusEffect = Instantiate(explodeRadiusEffect);
        radiusEffect.transform.position = pos;
        radiusEffect.transform.localScale *= diameter;

        var sphereRenderer = radiusEffect.GetComponent<Renderer>();

        sphereRenderer.material.color = Color.black;
        yield return new WaitForSeconds(timeToChangeEffectColor);
        sphereRenderer.material.color = Color.white;
        yield return new WaitForSeconds(timeToChangeEffectColor);
        Destroy(radiusEffect);*/

        Instantiate(explosionParticles, pos, Quaternion.identity);
    }
}
