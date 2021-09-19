using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneDirector : MonoBehaviour
{
    [SerializeField] GameEvent onMainMenu;
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneChanged;
    }

    private void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        switch(scene.name)
        {
            case "MainMenu":
                onMainMenu.Raise();
                break;
        }
    }
}
