using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Towers.Scenes
{
    public class LoadLevel : MonoBehaviour
    {
        public void LoadScene(int scene)
        {
            StartCoroutine(LoadNewScene(0));
            StartCoroutine(LoadNewScene(scene));
        }

        IEnumerator LoadNewScene(int scene)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(scene);

            while (!async.isDone)
            {
                yield return null;
            }
        }
    }
}