﻿using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Towers.CardN;

namespace Towers.Scenes
{
    public class SaveLoad : MonoBehaviour
    {
        public static SaveLoad control;

        void Awake()
        {
            if (control == null)
            {
                DontDestroyOnLoad(gameObject);
                control = this;
            }
            else if (control != null)
            {
                Destroy(gameObject);
            }
        }

        public void SaveFloatInfo(string infoName, float savingFloat)
        {
            PlayerPrefs.SetFloat(infoName, savingFloat);
            PlayerPrefs.Save();
        }

        public float LoadFloatInfo(string infoName)
        {
            return PlayerPrefs.GetFloat(infoName);
        }
    }
}