using System.Collections.Generic;
using UnityEngine;

namespace Towers.Scenes
{
    [CreateAssetMenu(menuName = ("Tower defense/LevelHolder"))]
    public class PlayerCompletedLevels : ScriptableObject
    {
        List<int> allFinishedLevels = new List<int>();

        public void LevelFinished(int level)
        {
            allFinishedLevels.Add(level);
        }

        public List<int> GetAllFinishedLevels()
        {
            return allFinishedLevels;
        }

        public void Reset()
        {
            allFinishedLevels = new List<int>();
        }
    }
}
