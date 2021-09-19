using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Returns the current level
    public static int LevelSelected { get; set; }

    private void Awake()
    {
        // Initialize Configurations 
        if(!ConfigurationUtils.Initialized)
            ConfigurationUtils.Initialize();

        DontDestroyOnLoad(gameObject);
    }
}
