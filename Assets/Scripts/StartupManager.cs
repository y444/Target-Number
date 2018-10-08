using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            bestResultText.text = "Best result: Level " + PlayerPrefs.GetInt("bestResult").ToString();
        }
        else
        {
            bestResultText.text = "";
        }

        if (PlayerPrefs.HasKey("currentLevel"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
