using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadGameplayScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadTutorialScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadTurorialOrGameplayScene()
    {
        LoadGameplayScene();
        // if (!PlayerPrefs.HasKey("tutorialComplete"))
        // {
        //     LoadTutorialScene();
        // }
        // else
        // {
        //     LoadGameplayScene();
        // }
    }
}
