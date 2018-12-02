using UnityEngine;

namespace Towers.Scenes
{
    public class LevelCounter : MonoBehaviour
    {
        int currentScene;

        public void LevelFinished(int level)
        {
            currentScene = level;
        }

        public int GetLevelFinished()
        {
            return currentScene;
        }
    }
}