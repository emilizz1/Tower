using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemePlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] float audioVolume = 0.5f;

    bool playing = true;

    AudioSource audioSource;

    void Awake()
    {
        if(FindObjectsOfType<ThemePlayer>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void Start ()
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
}
