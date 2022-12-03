using UnityEngine;

public class BulletShellSpawner : MonoBehaviour
{
    [SerializeField] private BulletShell shellToSpawn;
    [SerializeField] private Transform shellSpawnPoint;

    private Weapon _weapon;

    private void Start()
    {
        _weapon = GetComponent<Weapon>();
        _weapon.FiredEvent.AddListener(SpawnShell);
    }

    private void SpawnShell()
    {
        var shell = Instantiate(shellToSpawn, shellSpawnPoint.position, Quaternion.identity);

        shell.rb.AddRelativeForce(new Vector3(Random.Range(2, 4), Random.Range(2, 4)), ForceMode.Impulse);
        shell.rb.AddRelativeTorque(new Vector3(Random.Range(2, 4), Random.Range(2, 4)), ForceMode.Impulse);
    }
}
