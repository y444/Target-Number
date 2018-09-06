using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {

	public GameObject gameField;

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
		gameField = GameObject.Find("Game Field");

		//SET INITIAL LOOK
		//set value texts
		gameObject.transform.Find ("Text").GetComponent<Text>().text = value.ToString();
		//turn off all the zero buttons
		if (value == 0) {
			gameObject.GetComponent<Image> ().enabled = false;
			gameObject.transform.Find ("Text").gameObject.SetActive (false);
		}
		//properly display the target buttons
		if (isTarget == true) {
			//TODO change appearance to target etc.
		}
	}
	
	// Update is called once per frame
	void Update () {
		//refreshing values from gamefield, so all the changed buttons update themselves
		gameField.GetComponent<GameField> ().gameFieldData.cells [row, column].value = value;
		gameField.GetComponent<GameField> ().gameFieldData.cells [row, column].isUsed = isUsed;
	}

	public void buttonPress() {
		//update this cell data
		isUsed = true;
		//update look
		gameObject.GetComponent<Image> ().sprite = buttonPressed;
		Vector2 newPosition = gameObject.GetComponentInChildren<Text> ().rectTransform.anchoredPosition;
		newPosition.y = -4f;
		gameObject.GetComponentInChildren<Text> ().rectTransform.anchoredPosition = newPosition;
		gameObject.GetComponentInChildren<Text> ().color = new Color32 (0x51, 0x96, 0x33, 0xE6);
		//turn off helper arrows instantly
		buttonHoverEnd ();
		//change adjacent cells values
	}

	public void buttonHoverStart()
	{
		int columnsCount = gameField.GetComponent<GameField> ().columns - 1;
		int rowsCount = gameField.GetComponent<GameField> ().rows - 1;
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
				isCelltoLeftUsed = gameField.GetComponent<GameField> ().gameFieldData.cells [row, column - 1].isUsed;
				leftArrow.SetActive (!isCelltoLeftUsed);
			} 

			if (column < columnsCount) {
				isCelltoRightUsed = gameField.GetComponent<GameField> ().gameFieldData.cells [row, column + 1].isUsed;
				rightArrow.SetActive (!isCelltoRightUsed);
			}

			if (row > 0) {
				isCelltoTopUsed = gameField.GetComponent<GameField> ().gameFieldData.cells [row - 1, column].isUsed;
				topArrow.SetActive (!isCelltoTopUsed);
			}

			if (row < rowsCount) {
				isCelltoBottomUsed = gameField.GetComponent<GameField> ().gameFieldData.cells [row + 1, column].isUsed;
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
