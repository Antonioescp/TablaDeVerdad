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

    public static void GoToMenu(MenuName menu)
    {
        switch(menu)
        {
            case MenuName.MainMenu:
                SceneManager.LoadScene("MainMenu");
                break;
            case MenuName.LevelMenu:
                SceneManager.LoadScene("LevelMenu");
                break;
        }
    }
}
