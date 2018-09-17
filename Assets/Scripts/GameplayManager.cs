using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour {

	public GameObject buttonPrefab;
	public GameObject gameField;

	public int rows;
	public int columns;
	public int maxValue;
	public int targetValue;

	public CellMatrix cellMatrix;

	void Awake () {

		// TODO Get gameplay parameters from level manager

		// Generate the matrix
		cellMatrix = new CellMatrix(rows, columns, maxValue, targetValue);

		// Fix grid layout to make correct number of columns
		gameField.GetComponent<GridLayoutGroup>().constraintCount = columns;

		// Instantiate buttons
		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {
				GameObject instantiatedButton = Instantiate (buttonPrefab, gameField.transform);
				instantiatedButton.GetComponent<GameFieldButton> ().row = this.cellMatrix.cells[i,j].row;
				instantiatedButton.GetComponent<GameFieldButton> ().column = this.cellMatrix.cells[i,j].column;
				instantiatedButton.GetComponent<GameFieldButton> ().gameplayManager = this.gameObject;
			}
		}
	}

	public string GetButtonValue(int row, int column) {
		string value = cellMatrix.cells [row, column].value.ToString ();
		return value;
	}

	public ButtonState GetButtonState(int row, int column) {
		return ButtonState.normal;
	}

	public void GameFieldAction(int row, int column) {
		
	}
}

public enum ButtonState {normal, used, zero, dead};