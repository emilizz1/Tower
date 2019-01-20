using UnityEngine;

namespace Towers.Scenes.RunSelection
{
    public class LevelSelectionManager : MonoBehaviour
    {
        [SerializeField] LevelSelection[] levels;
        [SerializeField] Sprite completedLevelSprite;
        [SerializeField] Sprite availableLevelSprite;
        [SerializeField] Sprite lockedLevelSprite;

        bool readyTosSelect = false;

        Rect screenRectOnConstruction = new Rect(0, 0, Screen.width, Screen.height);
        float maxRaycasterDepth = 1000f;
        RaycastHit hitInfo;
        LoadLevel loadLevel;
        LevelSelection lastCompletedLevel;

        void Start()
        {
            loadLevel = GetComponent<LoadLevel>();
            lastCompletedLevel = levels[FindObjectOfType<LevelCounter>().GetLevelFinished() - 1];
            ActivateActiveLevels();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && readyTosSelect)
            {
                PerformRaycast();
                if (hitInfo.transform.gameObject.GetComponent<LevelSelection>() && hitInfo.transform.gameObject.GetComponent<LevelSelection>().isActive)
                {
                    loadLevel.LoadScene(hitInfo.transform.gameObject.GetComponent<LevelSelection>().GetLevel());
                }
            }
        }

        void ActivateActiveLevels()
        {
            var allCompletedLevels = FindObjectOfType<LevelCounter>().GetAllFinishedLevels();
            bool levelFinished = false;
            for (int i = 0; i < levels.Length; i++)
            {
                foreach(int o in allCompletedLevels)
                {
                    if (i == o)
                    {
                        levels[i].LevelIsFinished();
                        levelFinished = true;
                    }
                }
                if (!levelFinished)
                {
                    levels[i].IsActive(false);
                    levelFinished = false;
                }
            }
            lastCompletedLevel.TurnOnAllnextLevels();
        }

        void PerformRaycast()
        {
            if (screenRectOnConstruction.Contains(Input.mousePosition))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hitInfo, maxRaycasterDepth);
            }
        }

        public void ChangeReadyToSelect(bool change)
        {
            readyTosSelect = change;
        }

        public LevelSelection GetCurrentLevel()
        {
            return lastCompletedLevel;
        }

        public Sprite GetCompletedLevelSprite()
        {
            return completedLevelSprite;
        }

        public Sprite GetAvailableLevelSprite()
        {
            return availableLevelSprite;
        }

        public Sprite GetLockedLevelSprite()
        {
            return lockedLevelSprite;
        }
    }
}