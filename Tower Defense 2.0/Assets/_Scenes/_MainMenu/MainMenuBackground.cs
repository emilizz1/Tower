using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Towers.Scenes.MainMenu
{
    public class MainMenuBackground : MonoBehaviour
    {
        [SerializeField] Color[] bgColors;
        [SerializeField] float transitionSpeed = 0.1f;
        [SerializeField] float changeColorAfter = 8f;

        RawImage image;

        void Start()
        {
            image = GetComponent<RawImage>();
            StartCoroutine(ChangeBgColors());
        }

        IEnumerator ChangeBgColors()
        {
            float time = Time.time;
            Color changingTo = bgColors[Random.Range(0, bgColors.Length)];
            while (true)
            {
                if (Time.time - time > changeColorAfter)
                {
                    changingTo = bgColors[Random.Range(0, bgColors.Length)];
                    time = Time.time;
                }
                image.color = Color.Lerp(image.color, changingTo, transitionSpeed);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
