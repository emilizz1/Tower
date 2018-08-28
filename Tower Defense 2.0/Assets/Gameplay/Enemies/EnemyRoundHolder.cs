using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Tower defense/Enemy Round Holder"))]
public class EnemyRoundHolder : ScriptableObject
{
    [SerializeField] EnemyAI[] enemies;

    public EnemyAI[] GetEnemy()
    {
        return enemies;
    } 

    public float GetEnemyCount()
    {
        return enemies.Length;
    }
}
