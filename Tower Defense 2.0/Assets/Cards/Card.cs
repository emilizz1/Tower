using UnityEngine;
using Towers.BuildingsN;
using Towers.Resources;

namespace Towers.CardN
{
    [CreateAssetMenu(menuName = ("Tower defense/Card"))]
    public class Card : ScriptableObject
    {
        [SerializeField] Resource[] GoldGivenFromEnemies;
        [SerializeField] Texture enemyImage;
        [SerializeField] BuildingsHolder prefabs;
        [SerializeField] GameObject enemyPrefab;
        [SerializeField] float amount;

        public Resource[] GetEnemyResources()
        {
            return GoldGivenFromEnemies;
        }

        public Texture GetEnemyTexture()
        {
            return enemyImage;
        }

        public BuildingsHolder GetPrefabs()
        {
            return prefabs;
        }

        public GameObject GetEnemyPrefab()
        {
            return enemyPrefab;
        }

        public float GetEnemyAmount()
        {
            return amount;
        }
    }
}