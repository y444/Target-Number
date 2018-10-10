using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Playerprefs structure
// int bestResult - shows the highest level achieved
// int currentLevel - shows that the player quit the game at certain level and is able to continue it


public class StartupManager : MonoBehaviour
{

    public Text bestResultText;
    public GameObject continueButton;
    public GameObject newGameButton;
    public Sprite activeButtonSprite;
    public Color32 activeButtonTextColor;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.HasKey("bestResult"))
        {
            bestResultText.text = "Best result: Level " + (PlayerPrefs.GetInt("bestResult") + 1).ToString();
        }
        else
        {
            bestResultText.text = "";
        }

        if (PlayerPrefs.HasKey("currentLevel") && PlayerPrefs.GetInt("currentLevel") > 0)
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void LoadGameplayScene()
    {
        SceneManager.LoadScene(1);
    }
}
