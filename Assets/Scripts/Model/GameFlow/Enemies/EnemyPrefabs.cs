using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemies/EnemyPrefabs", menuName = "EnemyPrefabs", order = 1)]
public class EnemyPrefabs : ScriptableObject
{
    public Enemy[] normalZombies;
}
