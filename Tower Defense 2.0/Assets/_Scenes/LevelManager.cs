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
        int currentWave = 1;

        void Start()
        {
            text = GetComponent<Text>();
            wavesCount = enemyHolder.GetLevelCount();
            CheckForWaveMaxCount();
            UpdateText();
        }

        void UpdateText()
        {
            if (currentWave <= wavesCount)
            {
                text.text = currentWave.ToString() + " / " + wavesCount.ToString();
            }
        }

        public EnemyAI[] GetCurrentLevelEnemies()
        {
            return enemyHolder.GetEnemies(currentWave - 1).GetEnemy();
        }

        public bool CheckForLevelWon()
        {
            if (currentWave == wavesCount)
            {
                return true;
            }
            return false;
        }

        public void WaveFinished()
        {
            currentWave++;
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