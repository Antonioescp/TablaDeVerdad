using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    /// <summary>
    /// Loads Selected Level
    /// </summary>
    /// <param name="name">Level to load</param>
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
}
