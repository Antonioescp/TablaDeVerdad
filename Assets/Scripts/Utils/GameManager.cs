using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static bool alreadyExist = false;

    public static int LevelSelected;

    private void Awake()
    {
        ConfigurationUtils.Initialize();
        if (alreadyExist)
        {
            Destroy(gameObject);
        }
        else
        {
            alreadyExist = true;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        AudioManager.PlaySoundtrack(AudioClipName.Level1);
    }
}
