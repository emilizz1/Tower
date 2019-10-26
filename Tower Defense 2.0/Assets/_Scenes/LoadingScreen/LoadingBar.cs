using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Towers.Scenes.Loading
{
    public class LoadingBar : MonoBehaviour
    {
        [SerializeField] Image slider;

        float lerp;
        AsyncOperation async;

        private void Start()
        {
            StartCoroutine(LoadSceneAsync(FindObjectOfType<LoadLevel>().GetSceneToLoad()));
        }

        public IEnumerator LoadSceneAsync(int sceneNumber)
        {
            slider.fillAmount = 0f;

            yield return new WaitForSeconds(1f);

            AsyncOperation async  = SceneManager.LoadSceneAsync(sceneNumber);
            async.allowSceneActivation = false;

            while (!async.isDone)
            {
                if (slider.fillAmount < async.progress / 0.9f)
                {
                    slider.fillAmount += Mathf.Clamp(Time.deltaTime * (async.progress / 0.9f - slider.fillAmount), 0.0001f , 0.01f);
                }
                if(slider.fillAmount >= 0.975f)
                {
                    async.allowSceneActivation = true;
                }
                yield return null;
            }

            async.allowSceneActivation = true;
        }
    }
}