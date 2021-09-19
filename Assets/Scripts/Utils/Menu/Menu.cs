using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    /// <summary>
    /// Loads Selected Level
    /// </summary>
    /// <param name="name">Level to load</param>
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void GoToMainMenu()
    {
        MenuManager.GoToMenu(MenuName.MainMenu);
    }

    public void GoToLevelMenu()
    {
        AudioManager.PlayOneShot(AudioClipName.SFXPop);
        MenuManager.GoToMenu(MenuName.LevelMenu);
    }
}
