using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMatrix
{

    public int rows;
    public int columns;
    public int maxValue;
    public int targetValue;
    public int numberOfTargets;
    public Cell[,] cells;

    public CellMatrix(int rows, int columns, int maxValue, int targetValue, int numberOfTargets)
    {

        this.maxValue = maxValue;
        this.targetValue = targetValue;

        cells = new Cell[rows, columns];

        // Fill values of all the cells
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                cells[i, j] = new Cell(i, j, Random.Range(0, maxValue + 1));
            }
        }

        // Select random cells to be targets and zero them
        int k = 0;
        while (k < numberOfTargets)
        {
            int randomRow = Random.Range(0, rows);
            int randomColumn = Random.Range(0, columns);
            if ((cells[randomRow, randomColumn].isTarget == false) && (cells[randomRow, randomColumn].value != 0))
            {
                cells[randomRow, randomColumn].isTarget = true;
                cells[randomRow, randomColumn].value = 0;
                k++;
            }
        }
        //TODO generate everything else
    }
}
