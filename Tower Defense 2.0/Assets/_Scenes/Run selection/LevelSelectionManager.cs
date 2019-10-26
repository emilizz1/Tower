using UnityEngine;
using Towers.Events;
using Towers.Resources;
using Towers.Core;

namespace Towers.Scenes.RunSelection
{
    public class LevelSelectionManager : MonoBehaviour
    {
        [SerializeField] LevelSelection[] levels;
        [SerializeField] Sprite availableLevelSprite, lockedLevelSprite, completedLevelSprite;
        [SerializeField] GameObject completedLevelPS, availableLevelPS, lockedLevelPS, deckBuilder;

        bool readyToSelect = false;

        Rect screenRectOnConstruction = new Rect(0, 0, Screen.width, Screen.height);
        float maxRaycasterDepth = 1000f;
        RaycastHit hitInfo;
        LevelSelection lastCompletedLevel;

        void Start()
        {
            lastCompletedLevel = levels[FindObjectOfType<LevelCounter>().GetLevelFinished()];
            ActivateActiveLevels();
            if (FindObjectOfType<SaveLoad>().LoadIntInfo("EventFinished") == 0)
            {
                FindObjectOfType<EventManager>().PrepareEvents();
            }
            if (!FindObjectOfType<NewRunPlus>().GetCardDraft())
            {
                deckBuilder.SetActive(false);
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && readyToSelect)
            {
                PerformRaycast();
                if (hitInfo.transform.gameObject.GetComponent<LevelSelection>() &&
                    hitInfo.transform.gameObject.GetComponent<LevelSelection>().isActive)
                {
                    foreach(MovingResource movingResource in FindObjectsOfType<MovingResource>())
                    {
                        movingResource.GiveResourceInstantly();
                    }
                    FindObjectOfType<LoadLevel>().LoadScene(hitInfo.transform.gameObject.GetComponent<LevelSelection>().GetLevel());
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
                }
                levelFinished = false;
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
            readyToSelect = change;
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

        public GameObject GetCompletedLevelPS()
        {
            return completedLevelPS;
        }

        public GameObject GetAvailableLevelPs()
        {
            return availableLevelPS;
        }

        public GameObject GetLockedLevelPS()
        {
            return lockedLevelPS;
        }
    }
}