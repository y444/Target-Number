﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { LostNoMoves, LostOvershoot, Won };

public class GameStateManager : MonoBehaviour
{

    public GameObject popupManager;
    public GameObject gameplayManager;
    public GameStates currentState;

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
                popupManager.GetComponent<PopupManager>().Show(Popups.Lose);
                Debug.Log("No Moves");
                break;
            case GameStates.LostOvershoot:
                currentState = GameStates.LostOvershoot;
                popupManager.GetComponent<PopupManager>().Show(Popups.Lose);
                Debug.Log("Overshoot");
                break;
            case GameStates.Won:
                currentState = GameStates.Won;
                popupManager.GetComponent<PopupManager>().Show(Popups.Win);
                Debug.Log("Won");
                break;
            default:
                break;
        }
    }
}