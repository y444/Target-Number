using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFieldButton : MonoBehaviour {

	public int row;
	public int column;

	public GameObject gameplayManager;

	// Use this for initialization
	void Start () {
		//GetComponentInChildren<Text> ().text = gameplayManager.GetComponent<GameplayManager>().cellMatrix.cells [row, column].value.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
