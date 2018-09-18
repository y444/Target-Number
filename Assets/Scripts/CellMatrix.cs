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
		cells = new Cell[rows, columns];
		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {
				cells[i,j] = new Cell(i,j,Random.Range( 0, maxValue + 1) );
			}
		}
		//TODO generate everything else
	}
}
