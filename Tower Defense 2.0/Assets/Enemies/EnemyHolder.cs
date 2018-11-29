using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Tower defense/Enemy Holder"))]
public class EnemyHolder : ScriptableObject
{
    [Tooltip("List one enemy type fully first")] [SerializeField] EnemyRoundHolder[] enemies;

    public float GetLevelCount()
    {
        return enemies.Length;
    }

    public EnemyRoundHolder GetEnemies(int index)
    {
        return enemies[index];
    }
}
