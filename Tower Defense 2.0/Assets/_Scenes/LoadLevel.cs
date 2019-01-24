using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Towers.Scenes
{
    public class LoadLevel : MonoBehaviour
    {
        AsyncOperation async;

        public void LoadScene(int scene)
        {
            StartCoroutine(LoadNewScene(0));
            StartCoroutine(LoadNewScene(scene));
        }

        IEnumerator LoadNewScene(int scene)
        {
            async = SceneManager.LoadSceneAsync(scene);

            while (!async.isDone)
            {
                yield return null;
            }
        }

        public float GetLoadingProgress()
        {
            return async.progress;
        }
    }
}