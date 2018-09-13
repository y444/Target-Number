using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldData{

	public CellData[,] cells;

	public GameFieldData (int rows, int columns, int maxValue, int numberOfTargets, int targetValue) {
		//TODO proper level generation
		cells = new CellData[rows,columns];
		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {
				cells [i, j] = new CellData();
				cells [i, j].row = i;
				cells [i, j].column = j;
				cells [i, j].value = Random.Range (0, maxValue + 1);
			}
		}
		int k = 0;
		while (k < numberOfTargets) {
			int randomRow = Random.Range (0, rows);
			int randomColumn = Random.Range (0, columns);
			if ((cells [randomRow, randomColumn].isTarget == false) && (cells [randomRow, randomColumn].value != 0)) {
				cells [randomRow, randomColumn].isTarget = true;
				cells [randomRow, randomColumn].value = 0;
				k++;
			}
		}
	}
}
