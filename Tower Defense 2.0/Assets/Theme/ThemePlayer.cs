﻿using System.Collections;
using UnityEngine;

namespace Towers.Theme
{
    public class ThemePlayer : MonoBehaviour
    {
        float audioVolume = 0.5f;
        AudioClip[] audioClips;
        AudioSource audioSource;

        void PlayAudio()
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.loop = true;
            audioSource.Play();
        }

        IEnumerator ChangedScenes()
        {
            audioSource.volume = 0f;
            audioSource.Stop();
            PlayAudio();
            while (audioSource.volume < audioVolume)
            {
                audioSource.volume += 0.01f;
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void GiveAudioTheme(AudioClip[] audioThemes)
        {
            audioClips = audioThemes;
            audioSource.volume = audioVolume;
            PlayAudio();
            StartCoroutine(ChangedScenes());
        }

        public void ChangeAudioVolume(float volume)
        {
            audioVolume = volume;
            if(audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
            audioSource.volume = audioVolume;
        }
    }
}