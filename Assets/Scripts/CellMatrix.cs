using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMatrix {

	public int rows;
	public int columns;
	public int maxValue;
	public int targetValue;
	public Cell[,] cells;

	public CellMatrix(int rows, int columns, int maxValue, int targetValue) {
		this.maxValue = maxValue;
		this.targetValue = targetValue;
		Cell[,] cells = new Cell[rows, columns];
		Debug.Log ("created a cell matrix " + rows.ToString () + " " + columns.ToString () + " " + maxValue.ToString() + " " + targetValue.ToString ());
		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {
				cells[i,j] = new Cell(i,j,0);
				Debug.Log ("created a cell " + cells [i, j].row + " " + cells [i, j].column + " " + cells [i, j].value);
			}
		}
		//TODO generate everything else
	}
}
