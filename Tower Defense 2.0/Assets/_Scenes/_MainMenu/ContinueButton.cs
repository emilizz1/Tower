using System.Collections.Generic;
using UnityEngine;
using Towers.Core;

namespace Towers.Scenes.MainMenu
{
    public class ContinueButton : MonoBehaviour
    {
        SaveLoad saveLoad;

        void Start()
        {
            saveLoad = FindObjectOfType<SaveLoad>();
            List<int> completedLevels = saveLoad.LoadCompletedLevels();
            if (completedLevels.Count < 1 || completedLevels.Count > 6)
            {
                gameObject.SetActive(false);
            }
        }

        public void ContinuePlaying()
        {
            saveLoad.LoadPlayerResources();
            FindObjectOfType<LifePoints>().GiveNewLifepoints(saveLoad.LoadIntInfo("Lifepoints"));
            foreach (int level in saveLoad.LoadCompletedLevels())
            {
                FindObjectOfType<LevelCounter>().LevelFinished(level);
            }
            FindObjectOfType<LoadLevel>().LoadScene(3);
        }
    }
}
