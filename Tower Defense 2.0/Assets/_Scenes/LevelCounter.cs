using UnityEngine;

namespace Towers.Scenes
{
    public class LevelCounter : MonoBehaviour
    {
        int currentLevelFinished = 0;

        public void LevelFinished(int level)
        {
            currentLevelFinished = level;
        }

        public int GetLevelFinished()
        {
            return currentLevelFinished;
        }
    }
}