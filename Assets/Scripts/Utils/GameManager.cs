using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static bool alreadyExist = false;

    private void Awake()
    {
        if (alreadyExist)
        {
            Destroy(gameObject);
        }
        else
        {
            alreadyExist = true;
            DontDestroyOnLoad(gameObject);
        }

        // Initializing screen utils
        ScreenUtils.Initialize();
    }
}
