using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldData{

	public CellData[,] cells;

	public GameFieldData (int rows, int columns, int maxValue, int numberOfTargets) {
		cells = new CellData[rows,columns];
		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {
				//TODO proper level generation
				cells [i, j] = new CellData();
				cells [i, j].row = i;
				cells [i, j].column = j;
				cells [i, j].value = Random.Range (0, maxValue + 1);
				if (cells [i, j].value == 0) {
					cells [i, j].isUsed = true;
				}
				cells [i, j].isTarget = false;
				cells [i, j].targetValue = 0;
			}
		}
	}
}
