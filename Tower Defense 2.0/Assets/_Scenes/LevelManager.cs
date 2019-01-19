using Towers.Enemies;
using UnityEngine;
using UnityEngine.UI;
using Towers.CardN;

namespace Towers.Scenes
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] EnemyHolder enemyHolder;
        
        Text text;
        int wavesCount;
        int currentLevel = 1;

        void Start()
        {
            text = GetComponent<Text>();
            wavesCount = enemyHolder.GetLevelCount();
            CheckForWaveMaxCount();
            UpdateText();
        }

        void UpdateText()
        {
            if (currentLevel <= wavesCount)
            {
                text.text = currentLevel.ToString() + " / " + wavesCount.ToString();
            }
        }

        public EnemyAI[] GetCurrentLevelEnemies()
        {
            return enemyHolder.GetEnemies(currentLevel - 1).GetEnemy();
        }

        public bool CheckForLevelWon()
        {
            if (currentLevel == wavesCount)
            {
                return true;
            }
            return false;
        }

        public void LevelFinished()
        {
            currentLevel++;
            UpdateText();
        }

        void CheckForWaveMaxCount()
        {
            int playableCards = 0;
            playableCards += FindObjectOfType<Deck>().GetLevelCards().GetAllCards().Length;
            playableCards += FindObjectOfType<CardHolders>().GetAllPlayerCards().Length;
            if(playableCards - 2 < enemyHolder.GetAllEnemies().Length)
            {
                wavesCount = playableCards - 2;
            }
        }
    }
}