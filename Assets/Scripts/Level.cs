using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level{

	public int rows = 4;
	public int columns = 10;
	public int maxValue = 10;
	public int numberOfTargets = 0;
    public Cell[,] cells { get; private set; }

    public void GenerateCells(){
		//initialize the array
		cells = new Cell[rows,columns];

		//populate the array with values
		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {
                cells[i, j] = new Cell
                {
                    row = i,
                    column = j,
                    value = Random.Range(0, maxValue + 1)
                };
            }
		}

		//treat all the zero valued buttons as used
		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {
				if (cells [i, j].value == 0) {
					cells [i, j].isUsed = true;
				}
			}
		}
	}
	
}
