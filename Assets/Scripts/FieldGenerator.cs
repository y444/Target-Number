using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator
{

    public int rows;
    public int columns;
    public int maxValue;
    public int targetValue;
    public int numberOfTargets;
    public Cell[,] cells;

    public FieldGenerator(int rows, int columns, int maxValue, int numberOfTargets)
    {

        this.maxValue = maxValue;

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

        // VERSION 1: Testing the general approach viability

        // 1. Set all target to random numbers (HOW TO LIMIT?), set global target 

        // 2. Check target cells neighbours, choose a random amount [1, numberOfNeighbours] of neighbours to operate on

        // 3. Subtract random numbers from target until it's zero to the selected amount of neighbours

        // 4. Take one of those neighbour cells and repeat 2.

        // 5. Subtract a random number to the selected amount of neighbours

        // 6. If the cell(s) is left with the value of [minValue, maxValue] and there are other targets to work on
        // - go work on the next target

        // 7. If the cell is left with the value of [minValue, maxValue] and there are NO other targets
        // - fill the rest of the field with junk of [minValue, maxValue]

        // 8. When working on second etc. targets if we encounter already filled cells we must not subtract random numbers but 
        // the number that equals (sum of such neighbours minus current cell)
    }
}
