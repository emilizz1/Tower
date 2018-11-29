using Towers.Enemies;
using UnityEngine;
using UnityEngine.UI;

namespace Towers.Scenes
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] EnemyHolder enemyHolder;
        
        EnemyRoundHolder[] enemies;
        Text text;
        int currentLevel = 1;

        void Start()
        {
            text = GetComponent<Text>();
        }

        void Update()
        {
            text.text = currentLevel.ToString() + " / " + enemyHolder.GetLevelCount().ToString();
        }

        public EnemyAI[] GetCurrentLevelEnemies()
        {
            return enemyHolder.GetEnemies(currentLevel - 1).GetEnemy();
        }

        public bool CheckForLevelWon()
        {
            if (currentLevel == enemyHolder.GetLevelCount())
            {
                return true;
            }
            return false;
        }

        public void LevelFinished()
        {
            currentLevel++;
        }
    }
}