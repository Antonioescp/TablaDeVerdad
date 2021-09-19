using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIButtonManager : MonoBehaviour
{
    public void SelectLevel(int level)
    {
        GameManager.LevelSelected = level;
        SceneManager.LoadScene("Level");
    }

    public void BackButton(string name)
    {
        SceneManager.LoadScene(name);
    }
}