using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsManager : MonoBehaviour
{

    public int currentLevel;
    public int bestResult;
    public int tutorialComplete;

    public void Start()
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        bestResult = PlayerPrefs.GetInt("bestResult");
        tutorialComplete = PlayerPrefs.GetInt("tutorialComplete");
    }

    public void ResetCurrentLevel()
    {
        PlayerPrefs.SetInt("currentLevel", 0);
        PlayerPrefs.Save();
    }
}
