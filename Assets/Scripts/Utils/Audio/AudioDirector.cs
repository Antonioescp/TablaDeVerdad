using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Listens for events to play sounds

public class AudioDirector : MonoBehaviour
{
    [SerializeField] private GameEvent onClick;
    [SerializeField] private GameEvent onMainMenu;
    [SerializeField] private GameEvent onCellSpawn;

    private void Awake()
    {
        onClick.AddListener(OnClick);
        onMainMenu.AddListener(OnMainMenu);
        onCellSpawn.AddListener(OnCellSpawned);
    }

    // Must be called when a button is clicked
    private void OnClick()
    {
        AudioManager.PlayOneShot(AudioClipName.SFXClick);
    }

    private void OnMainMenu()
    {
        AudioManager.PlaySoundtrack(AudioClipName.STMain);
    }

    private void OnCellSpawned()
    {
        AudioManager.PlayOneShot(AudioClipName.SFXPop);
    }
}
