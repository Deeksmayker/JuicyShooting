using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private ParticleSystem smallParticles, hugeParticles;

    public void SpreadParticles(bool weakPoint, Vector3 pos)
    {
        Instantiate(weakPoint ? hugeParticles : smallParticles, pos, Quaternion.identity);
    }
}
