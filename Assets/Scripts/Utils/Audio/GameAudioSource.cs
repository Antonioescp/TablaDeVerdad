using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioSource : MonoBehaviour
{
    // audio sources to play every audio in the game
    private AudioSource audioSource;
    private AudioSource soundtrackAudioSource;

    private void Awake()
    {
        if(AudioManager.Initialized)
        {
            Destroy(gameObject);
        }
        else
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            soundtrackAudioSource = gameObject.AddComponent<AudioSource>();

            AudioManager.Initialize(audioSource, soundtrackAudioSource);
        }
    }
}
