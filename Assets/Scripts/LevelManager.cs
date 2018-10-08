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
    public GameObject levelNumberText;
    public GameObject gameplayManager;
    public Level[] levels;

    void Start()
    {
        // TODO Ask prefs if we are continuing the game

        // TODO Start either first or continuing level

        //TEMP
        StartLevel(0);

    }

    public void StartLevel(int levelNumber)
    {
        // Disable gameplay manager
        gameplayManager.SetActive(false);

        // Set current level number and other parameters
        currentLevelNumber = levelNumber;

        if (currentLevelNumber <= levels.Length)
        {
            rows = levels[currentLevelNumber].rows;
            columns = levels[currentLevelNumber].columns;
            maxValue = levels[currentLevelNumber].maxValue;
            numberOfTargets = levels[currentLevelNumber].numberOfTargets;
        }
        else
        {
            rows = levels[levels.Length].rows;
            columns = levels[levels.Length].columns;
            maxValue = levels[levels.Length].maxValue;
            numberOfTargets = levels[levels.Length].numberOfTargets;
        }

        // Enable gameplay manager so it can take parameters and generate grid
        gameplayManager.SetActive(true);

        // Set level number in top HUD
        levelNumberText.GetComponent<Text>().text = "Level " + (currentLevelNumber + 1).ToString();
    }

    public void NextLevel()
    {
        StartLevel(currentLevelNumber + 1);
    }
}
