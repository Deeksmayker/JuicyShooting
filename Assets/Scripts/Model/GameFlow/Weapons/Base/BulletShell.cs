using UnityEngine;

public class BulletShell : MonoBehaviour
{
    public Rigidbody rb;

    [SerializeField] private float lifeTime;

    private void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
            Destroy(gameObject);
    }
}
