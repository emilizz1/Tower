using UnityEngine;

namespace Towers.Theme
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
    }
}
