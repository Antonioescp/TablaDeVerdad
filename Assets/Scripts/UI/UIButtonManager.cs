using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonManager : MonoBehaviour
{
    [SerializeField] GameEvent onClick;

    // PlayButton
    public void OnPlay()
    {
        onClick.Raise();
        SceneManager.LoadScene("LevelMenu");
    }

    public void OnLevelButtonPressed(int level)
    {
        onClick.Raise();
        GameManager.LevelSelected = level;
        SceneManager.LoadScene("Level");
    }

    public void OnBackButton()
    {
        onClick.Raise();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
