﻿using UnityEngine;
using UnityEngine.UI;

namespace Towers.Scenes.Loading
{
    public class LoadingBar : MonoBehaviour
    {
        [SerializeField] Image slider;

        LoadLevel loadLevel;

        void Start()
        {
            loadLevel = FindObjectOfType<LoadLevel>();
        }

        void Update()
        {
            slider.fillAmount = loadLevel.GetLoadingProgress();
        }
    }
}