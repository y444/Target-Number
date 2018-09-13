using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameField : MonoBehaviour {
	[HideInInspector] public GameFieldData gameFieldData;
	public int rows;
	public int columns;
	public int maxValue;
	public int numberOfTargets;
	public int targetValue;
	public GameObject Cell;

	// Use this for initialization
	void Start () {
		gameFieldData = new GameFieldData(rows, columns, maxValue, numberOfTargets, targetValue);

		//setup grid layout
		this.GetComponent<GridLayoutGroup>().constraintCount = columns;

		//fill the field with buttons
		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {
				Cell instantiatedCell = Instantiate (Cell, this.transform).GetComponent<Cell> ();
				instantiatedCell.row = gameFieldData.cells[i,j].row;
				instantiatedCell.column = gameFieldData.cells[i,j].column;
				instantiatedCell.value = gameFieldData.cells [i, j].value;
				instantiatedCell.isUsed = gameFieldData.cells[i,j].isUsed;
				instantiatedCell.isTarget = gameFieldData.cells[i,j].isTarget;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
