using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldGenerator
{

    public int rows;
    public int columns;
    public int maxValue;
    public int targetValue;
    public int numberOfTargets;
    public Cell[,] cells;

    Cell[,] probableField;

    public FieldGenerator(int rows, int columns, int maxValue, int numberOfTargets, int movesLimit)
    {
        this.rows = rows;
        this.columns = columns;
        this.maxValue = maxValue;

        cells = new Cell[rows, columns];

        // Fill values of all the cells
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                cells[i, j] = new Cell(i, j, 0);
            }
        }

        // Select random cells to be targets
        int k = 0;
        Dictionary<Cell, int> targetToMoveCount = new Dictionary<Cell, int>();
        while (k < numberOfTargets)
        {
            int randomRow = Random.Range(0, rows);
            int randomColumn = Random.Range(0, columns);
            if ((cells[randomRow, randomColumn].isTarget == false))
            {
                cells[randomRow, randomColumn].isTarget = true;
                cells[randomRow, randomColumn].value = 0;
                k++;
                targetToMoveCount[cells[randomRow, randomColumn]] = movesLimit;
            }
        }
        GenerateSolutionPattern(targetToMoveCount);
    }

    void GenerateSolutionPattern(Dictionary<Cell, int> targetToMoveCount)
    {
        Dictionary<Cell, List<Cell>> targetToSolutions = new Dictionary<Cell, List<Cell>>();
        foreach (Cell target in targetToMoveCount.Keys)
        {
            List<Cell> solutionAndTargetCells = new List<Cell>();
            int moveCount = targetToMoveCount[target];
            solutionAndTargetCells.Add(target);

            while (moveCount > 0)
            {
                bool foundCellWithNeighbors = false;

                Cell randomCell = solutionAndTargetCells.GetRandomElement();

                while (foundCellWithNeighbors == false)
                {

                    List<Cell> neighboors = GetFreeNeighbors(randomCell, solutionAndTargetCells);
                    if (neighboors.Count > 0)
                    {
                        Cell newCell = neighboors.GetRandomElement();
                        solutionAndTargetCells.Add(newCell);
                        foundCellWithNeighbors = true;
                        moveCount--;
                    }
                    else
                    {
                        randomCell = solutionAndTargetCells.GetRandomElement();
                    }
                }
            }
            targetToSolutions[target] = solutionAndTargetCells;
        }
        OrderSolution(targetToSolutions);
    }

    void OrderSolution(Dictionary<Cell, List<Cell>> targetToSolutions)
    {
        List<Cell> finalSoultion = new List<Cell>();
        foreach (Cell target in targetToSolutions.Keys)
        {
            List<Cell> solutionCells = targetToSolutions[target];

            while (solutionCells.Count > 1)
            {
                Dictionary<Cell, int> cellToNeighboorsCount = new Dictionary<Cell, int>();
                //calculate neighboorsCount
                foreach (Cell cell in solutionCells)
                {
                    if (cell.isTarget == false)
                    {
                        cellToNeighboorsCount[cell] = GetNeighboorsCount(cell, solutionCells);
                    }
                }

                List<Cell> cellsToUse = new List<Cell>(cellToNeighboorsCount.Keys);

                cellsToUse.Sort((key1, key2) => cellToNeighboorsCount[key1] - cellToNeighboorsCount[key2]);

                Cell currentCell = cellsToUse.First();
                finalSoultion.Add(currentCell);
                solutionCells.Remove(currentCell);
            }
        }
        GenerateFinalFieldBasedOnSolution(finalSoultion);
    }

    void GenerateFinalFieldBasedOnSolution(List<Cell> solution)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (cells[i, j].isTarget == false)
                {
                    cells[i, j].value = Random.Range(0, maxValue + 1);
                }
            }
        }
        probableField = CopyArrayFrom(cells);

        string debugStr = "Solution is: ";

        List<Cell> wasAlreadyClicked = new List<Cell>();

        foreach (Cell cell in solution)
        {
            if (wasAlreadyClicked.Contains(cell) == false)
            {
                debugStr += "[" + cell.row + "," + cell.column + "]";
                EmulateClickOnCell(cell);
                wasAlreadyClicked.Add(cell);
            }
        }
        Debug.Log(debugStr);

        foreach (Cell cell in cells)
        {
            if (cell.isTarget)
            {
                targetValue += cell.value;
            }
        }
        cells = CopyArrayFrom(probableField);
    }

    void EmulateClickOnCell(Cell cell)
    {
        if (IsCellExist(cell.row + 1, cell.column))
        {
            cells[cell.row + 1, cell.column].value += cell.value;
        }
        if (IsCellExist(cell.row - 1, cell.column))
        {
            cells[cell.row - 1, cell.column].value += cell.value;
        }
        if (IsCellExist(cell.row, cell.column + 1))
        {
            cells[cell.row, cell.column + 1].value += cell.value;
        }
        if (IsCellExist(cell.row, cell.column - 1))
        {
            cells[cell.row, cell.column - 1].value += cell.value;
        }

    }


    int GetNeighboorsCount(Cell cell, List<Cell> solution)
    {
        int result = 0;
        if (IsCellExist(cell.row + 1, cell.column))
        {
            if (solution.Contains(cells[cell.row + 1, cell.column]))
            {
                result++;
            }
        }
        if (IsCellExist(cell.row - 1, cell.column))
        {
            if (solution.Contains(cells[cell.row - 1, cell.column]))
            {
                result++;
            }
        }
        if (IsCellExist(cell.row, cell.column + 1))
        {
            if (solution.Contains(cells[cell.row, cell.column + 1]))
            {
                result++;
            }
        }
        if (IsCellExist(cell.row, cell.column - 1))
        {
            if (solution.Contains(cells[cell.row, cell.column - 1]))
            {
                result++;
            }
        }
        return result;
    }

    List<Cell> GetFreeNeighbors(Cell cell, List<Cell> notInclude)
    {
        List<Cell> result = new List<Cell>();
        if (IsCellExist(cell.row + 1, cell.column))
        {
            Cell neighboor = cells[cell.row + 1, cell.column];
            if (notInclude.Contains(neighboor) == false)
            {
                result.Add(neighboor);
            }
        }
        if (IsCellExist(cell.row - 1, cell.column))
        {
            Cell neighboor = cells[cell.row - 1, cell.column];
            if (notInclude.Contains(neighboor) == false)
            {
                result.Add(neighboor);
            }
        }
        if (IsCellExist(cell.row, cell.column + 1))
        {
            Cell neighboor = cells[cell.row, cell.column + 1];
            if (notInclude.Contains(neighboor) == false)
            {
                result.Add(neighboor);
            }
        }
        if (IsCellExist(cell.row, cell.column - 1))
        {
            Cell neighboor = cells[cell.row, cell.column - 1];
            if (notInclude.Contains(neighboor) == false)
            {
                result.Add(neighboor);
            }
        }

        return result;
    }

    bool IsCellExist(int row, int column)
    {
        return row >= 0 && row < rows && column >= 0 && column < columns && cells[row, column] != null;
    }

    Cell[,] CopyArrayFrom(Cell[,] from)
    {
        Cell[,] to = new Cell[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                to[i, j] = from[i, j].Clone();
            }
        }
        return to;
    }
}


public static class ListExtensions
{
    public static T GetRandomElement<T>(this List<T> source) where T : class
    {
        return source.ElementAt(Random.Range(0, source.Count));
    }
}