using System.Collections.Generic;
using UnityEngine;
using Towers.Resources;
using Towers.Scenes;

namespace Towers.Core
{
    public class SaveLoad : MonoBehaviour
    {
        public static SaveLoad control;

        [SerializeField] PlayerResourceHolder resourceHolder;
        [SerializeField] PlayerCompletedLevels completedLevels;

        void Awake()
        {
            if (control == null)
            {
                DontDestroyOnLoad(gameObject);
                control = this;
            }
            else if (control != null)
            {
                Destroy(gameObject);
            }
        }

        public void SaveFloatInfo(string infoName, float savingFloat)
        {
            PlayerPrefs.SetFloat(infoName, savingFloat);
            PlayerPrefs.Save();
        }

        public float LoadFloatInfo(string infoName)
        {
            return PlayerPrefs.GetFloat(infoName);
        }

        public void SavePlayerResources()
        {
            resourceHolder.GiveResources(FindObjectOfType<ResourceHolder>().GetAllCurrentResources());
        }

        public void LoadPlayerResources()
        {
            FindObjectOfType<ResourceHolder>().SetAllNewResources(resourceHolder.GetResources());
        }

        public void SaveCompletedLevels(int levelFinished)
        {
            completedLevels.LevelFinished(levelFinished);
        }

        public List<int> LoadCompletedLevels()
        {
            return completedLevels.GetAllFinishedLevels();
        }

        public void ResetCompletedLevels()
        {
            completedLevels.Reset();
        }

        public void SaveIntInfo(string infoName, int savingInt)
        {
            PlayerPrefs.SetInt(infoName, savingInt);
            PlayerPrefs.Save();
        }

        public int LoadIntInfo(string infoName)
        {
            return PlayerPrefs.GetInt(infoName);
        }
    }
}