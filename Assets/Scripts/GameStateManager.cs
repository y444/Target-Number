using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { LostNoMoves, LostOvershoot, Won };

public class GameStateManager : MonoBehaviour
{

    public GameObject levelManager;
    public GameObject gameplayManager;
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
                Debug.Log("No Moves");
                break;
            case GameStates.LostOvershoot:
                Debug.Log("Overshoot");
                break;
            case GameStates.Won:
                Debug.Log("Won");
                break;
            default:
                break;
        }
    }
}