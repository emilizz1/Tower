﻿using UnityEngine;
using UnityEngine.UI;

namespace Towers.Scenes.RunSelection
{
    public class LevelSelection : MonoBehaviour
    {
        [SerializeField] int levelToLoad;
        [SerializeField] LevelSelection[] afterCompletionUnlocks;
        [SerializeField] GameObject[] events;

        enum ImageState
        {
            completed,
            available,
            locked,
            none
        }

        ImageState currenteState = ImageState.none;
        public bool isActive = false;

        public int GetLevel()
        {
            return levelToLoad;
        }

        public void IsActive(bool isActive)
        {
            if (GetComponentInChildren<Image>())
            {
                if (isActive)
                {
                    this.isActive = true;
                    currenteState = ImageState.completed;
                }
                else
                {
                    this.isActive = false;
                    currenteState = ImageState.locked;
                }
                UpdateImage();
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
            currenteState = ImageState.completed;
            UpdateImage();
        }

        void UpdateImage()
        {
            switch (currenteState)
            {
                case (ImageState.available):
                    GetComponentInChildren<Image>().sprite = FindObjectOfType<LevelSelectionManager>().GetAvailableLevelSprite();
                    break;
                case (ImageState.completed):
                    GetComponentInChildren<Image>().sprite = FindObjectOfType<LevelSelectionManager>().GetCompletedLevelSprite();
                    break;
                case (ImageState.locked):
                    GetComponentInChildren<Image>().sprite = FindObjectOfType<LevelSelectionManager>().GetLockedLevelSprite();
                    break;
            }
            print(gameObject.name + "   " + currenteState);
        }
    }
}