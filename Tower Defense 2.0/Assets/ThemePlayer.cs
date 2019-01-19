using System.Collections;
using UnityEngine;

namespace Towers.Scenes
{
    public class ThemePlayer : MonoBehaviour
    {
        [SerializeField] float audioVolume = 0.5f;

        bool playing = true;

        AudioClip[] audioClips;
        AudioSource audioSource;

        void Awake()
        {
            if (FindObjectsOfType<ThemePlayer>().Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = audioVolume;
            StartCoroutine(PlayAudio());
        }

        IEnumerator PlayAudio()
        {
            while (playing)
            {
                audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
                audioSource.Play();
                yield return new WaitForSeconds(audioSource.clip.length);
            }
        }

        IEnumerator ChangedScenes()
        {
            while(audioSource.volume > 0f)
            {
                audioSource.volume -= 0.01f;
                yield return new WaitForSeconds(0.1f);
            }
            StopCoroutine(PlayAudio());
            StartCoroutine(PlayAudio());
            while (audioSource.volume < audioVolume)
            {
                audioSource.volume += 0.01f;
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void GiveAudioTheme(AudioClip[] audioThemes)
        {
            audioClips = audioThemes;
            StartCoroutine(ChangedScenes());
        }
    }
}