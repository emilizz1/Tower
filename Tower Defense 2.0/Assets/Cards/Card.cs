using UnityEngine;
using Towers.BuildingsN;
using Towers.Resources;
using Towers.Enemies;

namespace Towers.CardN
{
    [CreateAssetMenu(menuName = ("Tower defense/Card"))]
    public class Card : ScriptableObject
    {
        [SerializeField] Resource[] GoldGiven;
        [SerializeField] BuildingsHolder buildingsPrefabs;
        [SerializeField] Texture enemyImage;
        [SerializeField] GameObject enemyPrefab;
        [SerializeField] float amount;

        public Resource[] GetEnemyResources()
        {
            return GoldGiven;
        }

        public Texture GetEnemyTexture()
        {
            return enemyImage;
        }

        public BuildingsHolder GetPrefabs()
        {
            return buildingsPrefabs;
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