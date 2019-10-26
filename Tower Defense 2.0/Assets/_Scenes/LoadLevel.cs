using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Towers.Scenes.Loading;

namespace Towers.Scenes
{
    public class LoadLevel : MonoBehaviour
    {
        AsyncOperation async;
        public int sceneToLoad;

        public void LoadScene(int scene)
        {
            sceneToLoad = scene;
            SceneManager.LoadScene(4);
        }

        public int GetSceneToLoad()
        {
            return sceneToLoad;
        }
    }
}