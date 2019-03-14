using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towers.Scenes;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public void ChangeMusicVolume()
    {
        FindObjectOfType<ThemePlayer>().ChangeAudioVolume(GetComponent<Slider>().value);
    }
}
