using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { LostNoMoves, LostOvershoot, Won, Normal };

public class GameStateManager : MonoBehaviour
{

    public GameObject popupManager;
    public GameObject gameplayManager;
    public GameStates currentState;
    public GameObject winPopup;
    public GameObject losePopup;

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
                popupManager.GetComponent<PopupManager>().Show(losePopup);
                Debug.Log("No Moves");
                break;
            case GameStates.LostOvershoot:
                currentState = GameStates.LostOvershoot;
                popupManager.GetComponent<PopupManager>().Show(losePopup);
                Debug.Log("Overshoot");
                break;
            case GameStates.Won:
                currentState = GameStates.Won;
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