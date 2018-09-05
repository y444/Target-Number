using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentCellData : MonoBehaviour {

	public GameObject currentGameField;

	public int row;
	public int column;	
	public int value;
	public bool isTarget;
	public int targetValue;
	public bool isUsed;

	public Sprite buttonPressed;

	// Use this for initialization
	void Start () {
		//GET GAMEFIELD
		currentGameField = GameObject.Find("Game Field");

		//SET INITIAL LOOK
		//turn off all the zero buttons
		//note that Level script must have already marked zero cells as used
		if (value == 0) {
			gameObject.GetComponent<Image> ().enabled = false;
			gameObject.transform.Find ("Text").gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {

		//GET CURRENT BUTTON STATE FROM GAMEFIELD

		//UPDATE THE LOOKS IF NEEDED
		//update value text
		if (isUsed == false) {
			gameObject.GetComponentInChildren<Text> ().text = value.ToString ();
		}

		//SEND UPDATED STATE TO THE GAMEFIELD
		currentGameField.GetComponent<GameField> ().level.cells[row,column].isUsed = isUsed;
	}

	public void buttonPress() {
		//LOOK
		gameObject.GetComponent<Image> ().sprite = buttonPressed;
		Vector2 newPosition = gameObject.GetComponentInChildren<Text> ().rectTransform.anchoredPosition;
		newPosition.y = -4f;
		gameObject.GetComponentInChildren<Text> ().rectTransform.anchoredPosition = newPosition;
		gameObject.GetComponentInChildren<Text> ().color = new Color32 (0x51, 0x96, 0x33, 0xE6);

		//LOGIC
		//end hover
		buttonHoverEnd ();

		//set as used
		isUsed = true;

		//report to GameField is in update

		//change adjacent
	}

	public void buttonHoverStart()
	{
		int columnsCount = currentGameField.GetComponent<GameField> ().level.columns - 1;
		int rowsCount = currentGameField.GetComponent<GameField> ().level.rows - 1;
		bool isCelltoLeftUsed;
		GameObject leftArrow = gameObject.transform.Find ("Arrows Panel").gameObject.transform.Find ("Left Arrow").gameObject;
		bool isCelltoRightUsed;
		GameObject rightArrow = gameObject.transform.Find ("Arrows Panel").gameObject.transform.Find ("Right Arrow").gameObject;
		bool isCelltoTopUsed;
		GameObject topArrow = gameObject.transform.Find ("Arrows Panel").gameObject.transform.Find ("Top Arrow").gameObject;
		bool isCelltoBottomUsed;
		GameObject bottomArrow = gameObject.transform.Find ("Arrows Panel").gameObject.transform.Find ("Bottom Arrow").gameObject;

		if (isUsed == false) {
			if (column > 0) {
				isCelltoLeftUsed = currentGameField.GetComponent<GameField> ().level.cells [row, column - 1].isUsed;
				leftArrow.SetActive (!isCelltoLeftUsed);
			} 

			if (column < columnsCount) {
				isCelltoRightUsed = currentGameField.GetComponent<GameField> ().level.cells [row, column + 1].isUsed;
				rightArrow.SetActive (!isCelltoRightUsed);
			}

			if (row > 0) {
				isCelltoTopUsed = currentGameField.GetComponent<GameField> ().level.cells [row - 1, column].isUsed;
				topArrow.SetActive (!isCelltoTopUsed);
			}

			if (row < rowsCount) {
				isCelltoBottomUsed = currentGameField.GetComponent<GameField> ().level.cells [row + 1, column].isUsed;
				bottomArrow.SetActive (!isCelltoBottomUsed);
			}
		}
	}

	public void buttonHoverEnd()
	{
		gameObject.transform.Find ("Arrows Panel").gameObject.transform.Find ("Left Arrow").gameObject.SetActive (false);
		gameObject.transform.Find ("Arrows Panel").gameObject.transform.Find ("Right Arrow").gameObject.SetActive (false);
		gameObject.transform.Find ("Arrows Panel").gameObject.transform.Find ("Top Arrow").gameObject.SetActive (false);
		gameObject.transform.Find ("Arrows Panel").gameObject.transform.Find ("Bottom Arrow").gameObject.SetActive (false);
	}
}
