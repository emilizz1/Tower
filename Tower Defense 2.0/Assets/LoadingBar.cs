using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Towers.Scenes
{
    public class LoadingBar : MonoBehaviour
    {
        [SerializeField] Slider slider;

        LoadLevel loadLevel;

        void Start()
        {
            loadLevel = FindObjectOfType<LoadLevel>();
        }

        void Update()
        {
            slider.value = loadLevel.GetLoadingProgress();
        }
    }
}
