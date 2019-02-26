using UnityEngine;

namespace Towers.Scenes
{
    public class LevelTheme : MonoBehaviour
    {
        [SerializeField] AudioClip[] audioClips;

        void Start()
        {
            if (FindObjectOfType<ThemePlayer>())
            {
                FindObjectOfType<ThemePlayer>().GiveAudioTheme(audioClips);
            }
        }

        public void MuteMusic()
        {
            FindObjectOfType<ThemePlayer>().ChangeAudioVolume(0f);
        }
    }
}
