using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStates { LostNoMoves, LostOvershoot, Won, Normal };

public class GameStateManager : MonoBehaviour
{

    public GameObject popupManager;
    public GameObject gameplayManager;
    public GameStates currentState;
    public GameObject winPopup;
    public GameObject losePopup;
    public GameObject losePopupReasonLabel;
    public string loseNoMovesText;
    public string loseOvershootText;
    public GameObject soundPlayer;
    public AudioSource winSound;
    public AudioSource loseSound;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StateChange(GameStates newState)
    {
        switch (newState)
        {
            case GameStates.LostNoMoves:
                currentState = GameStates.LostNoMoves;
                soundPlayer.GetComponent<SoundPlayer>().Play(loseSound);
                popupManager.GetComponent<PopupManager>().Show(losePopup);
                losePopupReasonLabel.GetComponent<Text>().text = loseNoMovesText;
                Debug.Log("No Moves");
                break;
            case GameStates.LostOvershoot:
                currentState = GameStates.LostOvershoot;
                soundPlayer.GetComponent<SoundPlayer>().Play(loseSound);
                popupManager.GetComponent<PopupManager>().Show(losePopup);
                losePopupReasonLabel.GetComponent<Text>().text = loseOvershootText;
                Debug.Log("Overshoot");
                break;
            case GameStates.Won:
                currentState = GameStates.Won;
                soundPlayer.GetComponent<SoundPlayer>().Play(winSound);
                popupManager.GetComponent<PopupManager>().Show(winPopup);
                Debug.Log("Won");
                break;
            case GameStates.Normal:
                currentState = GameStates.Normal;
                break;
            default:
                break;
        }
    }
}