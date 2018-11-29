using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers.Scenes.RunSelection
{
    public class LevelSelection : MonoBehaviour
    {
        [SerializeField] int levelToLoad;
        [SerializeField] ParticleSystem ps;
        [SerializeField] LevelSelection[] afterCompletionUnlocks;
        [SerializeField] GameObject[] events;

        public bool isActive = false;

        public int GetLevel()
        {
            return levelToLoad;
        }

        public void IsActive(bool isActive)
        {
            this.isActive = isActive;
            ps.gameObject.SetActive(isActive);
        }

        public void TurnOnAllnextLevels()
        {
            foreach (LevelSelection level in afterCompletionUnlocks)
            {
                level.IsActive(true);
            }
        }

        public GameObject[] GetEvents()
        {
            return events;
        }
    }
}