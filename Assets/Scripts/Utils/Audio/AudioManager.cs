using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class AudioManager
{
    #region Fields
    // audio sources to play all the soundtracks and sfx
    private static AudioSource oneShotSource;
    private static AudioSource soundtrackSource;

    // these stores the audioclips
    private static Dictionary<AudioClipName, AudioClip> audioClips;

    // to avoid calling Initialize multiple times
    private static bool initialized = false;

    #endregion

    #region Properties
    /// <summary>
    /// Audio Manager initialization state, if true it means it already has required audio sources
    /// </summary>
    public static bool Initialized => initialized;
    #endregion

    #region Methods

    /// <summary>
    /// Initializes audio manager, passing the audio sources to use the rest of the game
    /// </summary>
    /// <param name="oneShotAudioSource">Audio source to play one shot</param>
    /// <param name="soundtrackAudioSource">Audio source to play a soundtrack</param>
    /// <param name="gameAudioSource">The game object holding current and future audio sources</param>
    public static void Initialize(AudioSource oneShotAudioSource, AudioSource soundtrackAudioSource)
    {
        // sfx and soundtrack sources
        oneShotSource = oneShotAudioSource;
        soundtrackSource = soundtrackAudioSource;

        audioClips = new Dictionary<AudioClipName, AudioClip>();

        // Loading audioclips
        audioClips.Add(AudioClipName.STMain, Resources.Load<AudioClip>("Sounds/Soundtrack1"));
        audioClips.Add(AudioClipName.SFXPop, Resources.Load<AudioClip>("Sounds/SFXPop"));
        audioClips.Add(AudioClipName.SFXClick, Resources.Load<AudioClip>("Sounds/SFXClick"));

        // flagging initialization
        initialized = true;
    }

    /// <summary>
    /// Plays an audioclip using oneshot method
    /// </summary>
    /// <param name="name">Audioclip to play</param>
    public static void PlayOneShot(AudioClipName name)
    {
        oneShotSource.PlayOneShot(audioClips[name]);
    }

    /// <summary>
    /// Plays a soundfx in the given position
    /// </summary>
    /// <param name="name">Audio Clip name to play</param>
    /// <param name="position">Position at which AudioClip will be played</param>
    public static void PlayAtPosition(AudioClipName name, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioClips[name], position);
    }

    /// <summary>
    /// Plays a given soundtrack in a loop
    /// </summary>
    /// <param name="name">Audioclip name</param>
    public static void PlaySoundtrack(AudioClipName name)
    {
        if(soundtrackSource.clip != audioClips[name])
        {
            soundtrackSource.clip = audioClips[name];
            soundtrackSource.loop = true;
            soundtrackSource.Play();
        }
    }

    #endregion
}
