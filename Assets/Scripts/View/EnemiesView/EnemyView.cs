using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private ParticleSystem smallParticles, hugeParticles;
    
    [Header("Popup")]
    [SerializeField] private HitPopup hitPopup;

    [SerializeField] private Color colorOnWeakPoint;
    [SerializeField] private string textOnWeakPoint;
    [SerializeField] private Color colorOnNormalPoint;
    [SerializeField] private string textOnNormalPoint;

    public void MakeVisualEffectsOnHit(bool weakPoint, Vector3 pos)
    {
        SpreadParticles(weakPoint, pos);
        ShowHitPopup(weakPoint, pos);
    }

    private void SpreadParticles(bool weakPoint, Vector3 pos)
    {
        Instantiate(weakPoint ? hugeParticles : smallParticles, pos, Quaternion.identity);
    }

    private void ShowHitPopup(bool weakPoint, Vector3 pos)
    {
        var popup = Instantiate(hitPopup, pos, Quaternion.identity);
        popup.SetupText(weakPoint ? textOnWeakPoint : textOnNormalPoint, weakPoint ? colorOnWeakPoint : colorOnNormalPoint);
    }
}
