using UnityEngine;
using UnityEngine.UI;

namespace Towers.Cards
{
    public class LevelEnemyCard : MonoBehaviour
    {
        [Header("1st Enemy Setup")]
        [SerializeField] GameObject[] enemyCard;
        [SerializeField] Text[] enemyName;
        [SerializeField] Text[] enemyAmount;
        [SerializeField] Text[] enemyStats;

        LevelManager levelManager;
        int differentEnemyTypes = 0;

        void Start()
        {
            levelManager = FindObjectOfType<LevelManager>();
        }

        public void PutEnemiesOnScreen()
        {
            if (levelManager.GetCurrentLevelEnemies().Length != 0)
            {
                int currentEnemyCount = 0;
                GameObject myEnemy = null;
                foreach (EnemyAI enemy in levelManager.GetCurrentLevelEnemies())
                {
                    if (myEnemy == null)
                    {
                        myEnemy = enemy.gameObject;
                        currentEnemyCount++;
                    }
                    else if (enemy.gameObject != myEnemy)
                    {
                        ListAnotherEnemy(currentEnemyCount, myEnemy);
                        myEnemy = enemy.gameObject;
                        currentEnemyCount = 1;
                    }
                    else
                    {
                        currentEnemyCount++;
                    }
                }
                ListAnotherEnemy(currentEnemyCount, myEnemy);
            }
        }

        void ListAnotherEnemy(int amount, GameObject enemy)
        {
            enemyCard[differentEnemyTypes].SetActive(true);
            enemyName[differentEnemyTypes].text = enemy.name;
            enemyAmount[differentEnemyTypes].text = "Amount: " + amount.ToString();
            enemyStats[differentEnemyTypes].text = "Health: " + enemy.GetComponent<HealthSystem>().GetMaxHP() + " Movement Speed: " + enemy.GetComponent<Character>().GetMovementSpeed();
            differentEnemyTypes++;

        }

        public void TurnOffEnemyCards()
        {
            foreach (GameObject myEnemyCard in enemyCard)
            {
                myEnemyCard.SetActive(false);
                differentEnemyTypes = 0;
            }
        }
    }
}