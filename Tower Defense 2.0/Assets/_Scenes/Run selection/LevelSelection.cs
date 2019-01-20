using UnityEngine;
using UnityEngine.UI;

namespace Towers.Scenes.RunSelection
{
    public class LevelSelection : MonoBehaviour
    {
        [SerializeField] int levelToLoad;
        [SerializeField] LevelSelection[] afterCompletionUnlocks;
        [SerializeField] GameObject[] events;

        public bool isActive = false;

        public int GetLevel()
        {
            return levelToLoad;
        }

        public void IsActive(bool isActive)
        {
            if (isActive)
            {
                isActive = true;
                GetComponentInChildren<Image>().sprite = FindObjectOfType<LevelSelectionManager>().GetAvailableLevelSprite();
            }
            else
            {
                isActive = false;
                GetComponentInChildren<Image>().sprite = FindObjectOfType<LevelSelectionManager>().GetLockedLevelSprite();
            }
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

        public void LevelIsFinished()
        {
            isActive = false;
            GetComponentInChildren<Image>().sprite = FindObjectOfType<LevelSelectionManager>().GetCompletedLevelSprite();
        }
    }
}