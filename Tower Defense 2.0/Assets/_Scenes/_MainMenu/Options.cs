using UnityEngine;
using Towers.Core;
using UnityEngine.UI;
using Towers.Theme;

public class Options : MonoBehaviour
{
    const string musicVolumeSaveName = "MUSIC_VOLUME_SAVE";

    private void Start()
    {
        GetComponentInChildren<Slider>().value = FindObjectOfType<SaveLoad>().LoadFloatInfo(musicVolumeSaveName);
        FindObjectOfType<ThemePlayer>().ChangeAudioVolume(GetComponentInChildren<Slider>().value);
    }

    public void ChangeMusicVolume()
    {
        FindObjectOfType<ThemePlayer>().ChangeAudioVolume(GetComponentInChildren<Slider>().value);
        FindObjectOfType<SaveLoad>().SaveFloatInfo(musicVolumeSaveName, GetComponentInChildren<Slider>().value);
    }
}
