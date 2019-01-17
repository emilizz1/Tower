using UnityEngine;

namespace Towers.Enemies
{
    [CreateAssetMenu(menuName = ("Tower defense/Enemy Holder"))]
    public class EnemyHolder : ScriptableObject
    {
        [Tooltip("List one enemy type fully first")] [SerializeField] EnemyRoundHolder[] enemies;

        public int GetLevelCount()
        {
            return enemies.Length;
        }

        public EnemyRoundHolder GetEnemies(int index)
        {
            return enemies[index];
        }

        public EnemyRoundHolder[] GetAllEnemies()
        {
            return enemies;
        }
    }
}