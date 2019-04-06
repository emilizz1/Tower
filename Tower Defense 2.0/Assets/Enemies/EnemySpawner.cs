using System.Collections;
using System.Collections.Generic;
using Towers.CardN;
using Towers.Scenes;
using UnityEngine;

namespace Towers.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        CardManager cardManager;
        List<GameObject> enemies;
        EnemyAI[] levelEnemies;
        LevelManager levelManager;

        void Start()
        {
            levelManager = FindObjectOfType<LevelManager>();
            cardManager = FindObjectOfType<CardManager>();
        }

        private IEnumerator SpawningEnemies()
        {
            foreach (GameObject enemy in enemies)
            {
                Instantiate(enemy, this.transform.position, Quaternion.identity, this.transform);
                yield return new WaitForSecondsRealtime(0.25f);
            }
        }

        public void StartNextWave()
        {
            enemies = cardManager.GetEnemiesToCome();
            levelEnemies = levelManager.GetCurrentLevelEnemies();
            if (levelEnemies != null)
            {
                foreach (EnemyAI enemy in levelEnemies)
                {
                    enemies.Add(enemy.gameObject);
                }
            }
            StartCoroutine(SpawningEnemies());
        }

        public EnemyAI[] GetAllEnemies()
        {
            return GetComponentsInChildren<EnemyAI>();
        }

        public bool AreEnemiesAlive()
        {
            return GetComponentsInChildren<EnemyAI>().Length == 0;
        }
    }
}