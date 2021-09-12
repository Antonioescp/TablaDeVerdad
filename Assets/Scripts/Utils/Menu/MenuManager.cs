using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager
{
    public static void LoadLevel(LevelName level)
    {
        SceneManager.LoadScene(level.ToString());
    }
}
