using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public int currentLevelNumber;
    public int rows;
    public int columns;
    public int maxValue;
    public int numberOfTargets;
    public int movesLimit;
    public GameObject levelNumberText;
    public GameObject gameplayManager;
    public Level[] levels;

    void Start()
    {
        // Ask prefs what level should we start at
        if (PlayerPrefs.HasKey("currentLevel"))
        {
            StartLevel(PlayerPrefs.GetInt("currentLevel"), InitIntent.NewLevel);
        }
        else
        {
            StartLevel(0, InitIntent.NewLevel);
        }
    }

    public void StartLevel(int levelNumber, InitIntent intent)
    {
        // Disable gameplay manager
        gameplayManager.GetComponent<GameplayManager>().Reset();

        // Set current level number and other parameters
        currentLevelNumber = levelNumber;

        if (currentLevelNumber < levels.Length)
        {
            rows = levels[currentLevelNumber].rows;
            columns = levels[currentLevelNumber].columns;
            maxValue = levels[currentLevelNumber].maxValue;
            numberOfTargets = levels[currentLevelNumber].numberOfTargets;
        }
        else
        {
            rows = levels[levels.Length - 1].rows;
            columns = levels[levels.Length - 1].columns;
            maxValue = levels[levels.Length - 1].maxValue;
            numberOfTargets = levels[levels.Length - 1].numberOfTargets;
        }

        // Enable gameplay manager so it can take parameters and generate grid
        gameplayManager.GetComponent<GameplayManager>().Init(intent);

        // Update current level & highscore
        PlayerPrefs.SetInt("currentLevel", currentLevelNumber);

        if (currentLevelNumber > PlayerPrefs.GetInt("bestResult"))
        {
            PlayerPrefs.SetInt("bestResult", currentLevelNumber);
        }
        PlayerPrefs.Save();

        // Set level number in top HUD
        levelNumberText.GetComponent<Text>().text = "Level " + (currentLevelNumber + 1).ToString();

        Debug.Log("Level " + currentLevelNumber.ToString() + " start");
    }

    public void NextLevel()
    {
        StartLevel(currentLevelNumber + 1, InitIntent.NewLevel);
    }

    public void RestartLevel()
    {
        StartLevel(currentLevelNumber, InitIntent.ResetLevel);
    }
}
