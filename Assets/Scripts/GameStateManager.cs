using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameLostReasons { NoMoves, Overshoot };

public class GameStateManager : MonoBehaviour {

    public GameObject levelManager;
    public GameObject gameplayManager;
    public GameObject winPopup;
    public GameObject losePopup;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameWon()
    {

    }

    public void GameLost(GameLostReasons reason)
    {

    }
}
