using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {

	public int row;
	public int column;	
	public int value;
	public bool isTarget;
	public int targetValue;
	public bool isUsed;

	public Sprite buttonPressed;
	public Sprite buttonDead;

	private GameFieldData gameFieldData;
	private GameObject leftArrow;
	private GameObject rightArrow;
	private GameObject topArrow;
	private GameObject bottomArrow;
	private int rowsCount;
	private int columnsCount;
	private Text buttonText;

	private bool isDead = false;

	// Use this for initialization
	void Start () {
		//GET GAMEFIELD AND OTHER SHIT
		gameFieldData = GameObject.Find("Game Field").GetComponent<GameField> ().gameFieldData;
		leftArrow = gameObject.transform.Find ("Arrows Panel").gameObject.transform.Find ("Left Arrow").gameObject;
		rightArrow = gameObject.transform.Find ("Arrows Panel").gameObject.transform.Find ("Right Arrow").gameObject;
		topArrow = gameObject.transform.Find ("Arrows Panel").gameObject.transform.Find ("Top Arrow").gameObject;
		bottomArrow = gameObject.transform.Find ("Arrows Panel").gameObject.transform.Find ("Bottom Arrow").gameObject;
		rowsCount = GameObject.Find ("Game Field").GetComponent<GameField> ().rows - 1;
		columnsCount = GameObject.Find ("Game Field").GetComponent<GameField> ().columns - 1;
		buttonText = gameObject.transform.Find ("Text").GetComponent<Text> ();

		//SET INITIAL LOOK
		//set value texts
		buttonText.text = value.ToString();
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
		//animating value changes on buttons
		if (value != gameFieldData.cells [row, column].value) {
			GetComponent<Animator>().SetTrigger("ButtonFlashTrigger");
		}
		//refreshing values from gamefield, so all the changed buttons update themselves
		value = gameFieldData.cells [row, column].value;
		isUsed = gameFieldData.cells [row, column].isUsed;
		//updating values display on buttons
		buttonText.text = value.ToString();
		//checking for dead cells, marking, and recoloring them
		int deadCount = 0;
		if (isUsed == false) {
			if (column > 0) {
				if (gameFieldData.cells [row, column - 1].isUsed == true) {
					deadCount += 1;
				}
			}
			if (column == 0) {
				deadCount += 1;
			}
			if (column < columnsCount) {
				if (gameFieldData.cells [row, column + 1].isUsed == true) {
					deadCount += 1;
				}
			}
			if (column == columnsCount) {
				deadCount += 1;
			}
			if (row > 0) {
				if (gameFieldData.cells [row - 1, column].isUsed == true) {
					deadCount += 1;
				}
			}
			if (row == 0) {
				deadCount += 1;
			}
			if (row < rowsCount) {
				if (gameFieldData.cells [row + 1, column].isUsed == true) {
					deadCount += 1;
				}
			}
			if (row == rowsCount) {
				deadCount += 1;
			}

			if (deadCount == 4) {
				isDead = true;
				gameObject.GetComponent<Image> ().sprite = buttonDead;
				gameObject.GetComponentInChildren<Text> ().color = new Color32 (0x64, 0x64, 0x64, 0xE6);
			}
		}
	}

	public void buttonPress() {
		//update this cell data in the gamefield
		if (isUsed == false && isDead == false) { 
			gameFieldData.cells [row, column].isUsed = true;
			//update look
			gameObject.GetComponent<Image> ().sprite = buttonPressed;
			Vector2 newPosition = gameObject.GetComponentInChildren<Text> ().rectTransform.anchoredPosition;
			newPosition.y = -4f;
			gameObject.GetComponentInChildren<Text> ().rectTransform.anchoredPosition = newPosition;
			gameObject.GetComponentInChildren<Text> ().color = new Color32 (0x51, 0x96, 0x33, 0xE6);
			//turn off helper arrows instantly
			buttonHoverEnd ();
			//change adjacent cells values
			if (column > 0) {
				if (gameFieldData.cells [row, column - 1].isUsed == false) {
					gameFieldData.cells [row, column - 1].value += value;
				}
			}
			if (column < columnsCount) {
				if (gameFieldData.cells [row, column + 1].isUsed == false) {
					gameFieldData.cells [row, column + 1].value += value;
				}
			}
			if (row > 0) {
				if (gameFieldData.cells [row - 1, column].isUsed == false) {
					gameFieldData.cells [row - 1, column].value += value;
				}
			}
			if (row < rowsCount) {
				if (gameFieldData.cells [row + 1, column].isUsed == false) {
					gameFieldData.cells [row + 1, column].value += value;
				}
			}
		}
	}

	public void buttonHoverStart()
	{
		bool isCelltoLeftUsed;
		bool isCelltoRightUsed;
		bool isCelltoTopUsed;
		bool isCelltoBottomUsed;

		if (isUsed == false) {
			if (column > 0) {
				isCelltoLeftUsed = gameFieldData.cells [row, column - 1].isUsed;
				leftArrow.SetActive (!isCelltoLeftUsed);
			} 

			if (column < columnsCount) {
				isCelltoRightUsed = gameFieldData.cells [row, column + 1].isUsed;
				rightArrow.SetActive (!isCelltoRightUsed);
			}

			if (row > 0) {
				isCelltoTopUsed = gameFieldData.cells [row - 1, column].isUsed;
				topArrow.SetActive (!isCelltoTopUsed);
			}

			if (row < rowsCount) {
				isCelltoBottomUsed = gameFieldData.cells [row + 1, column].isUsed;
				bottomArrow.SetActive (!isCelltoBottomUsed);
			}
		}
		//playing animation
		GetComponent<Animator>().SetTrigger("ArrowsMoveTrigger");
	}

	public void buttonHoverEnd()
	{
		leftArrow.SetActive (false);
		rightArrow.SetActive (false);
		topArrow.SetActive (false);
		bottomArrow.SetActive (false);
	}
}
