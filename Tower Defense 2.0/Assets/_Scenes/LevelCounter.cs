using UnityEngine;
using System.Collections.Generic;

namespace Towers.Scenes
{
    public class LevelCounter : MonoBehaviour
    {
        List<int> allFinishedLevels = new List<int>();
        int currentLevelFinished = 0;

        public void LevelFinished(int level)
        {
            print("Level finished");
            currentLevelFinished = level;
            allFinishedLevels.Add(level);
        }

        public int GetLevelFinished()
        {
            return currentLevelFinished;
        }

        public List<int> GetAllFinishedLevels()
        {
            return allFinishedLevels;
        }
    }
}