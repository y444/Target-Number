using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameplayManager : MonoBehaviour
{

    public GameObject cellPrefab;
    public GameObject gameField;
    public GameObject targetText;

    public int rows;
    public int columns;
    public int maxValue;
    public int targetValue;
    public int numberOfTargets;

    public FieldGenerator fieldGenerator;
    public GameObject[,] gameFieldCells;

    void Awake()
    {

        // TODO Get gameplay parameters from level manager

        // Fix grid layout to make correct number of columns
        gameField.GetComponent<GridLayoutGroup>().constraintCount = columns;

        // Generate the field & pass the target value
        fieldGenerator = new FieldGenerator(rows, columns, maxValue, numberOfTargets);
        targetValue = fieldGenerator.targetValue;

        // Set global value text in the top HUD
        targetText.GetComponent<Text>().text = targetValue.ToString();

        // Create an array of game field cells
        gameFieldCells = new GameObject[rows,columns];

        // Instantiate game field cells and set their values
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Instantiate & name
                gameFieldCells[i, j] = Instantiate(cellPrefab, gameField.transform);
                gameFieldCells[i, j].name = "Button Row " + fieldGenerator.cells[i, j].row.ToString() + " Col " + fieldGenerator.cells[i, j].column.ToString();
                gameFieldCells[i, j].GetComponent<GameFieldCell>().gameplayManager = gameObject;

                // Set values from the generator
                gameFieldCells[i, j].GetComponent<GameFieldCell>().row = fieldGenerator.cells[i, j].row;
                gameFieldCells[i, j].GetComponent<GameFieldCell>().column = fieldGenerator.cells[i, j].column;
                gameFieldCells[i, j].GetComponent<GameFieldCell>().value = fieldGenerator.cells[i, j].value;
                gameFieldCells[i, j].GetComponent<GameFieldCell>().isTarget = fieldGenerator.cells[i, j].isTarget;

                // Set newly calculated values
                gameFieldCells[i, j].GetComponent<GameFieldCell>().isUsed = IsCellUsed(i, j);
                gameFieldCells[i, j].GetComponent<GameFieldCell>().isZero = IsCellZero(i, j);
                gameFieldCells[i, j].GetComponent<GameFieldCell>().isDead = IsCellDead(i, j);
            }
        }
    }

    public bool IsCellUsed(int row, int column)
    {
        return false;
    }

    public bool IsCellZero(int row, int column)
    {
        if (gameFieldCells[row, column].GetComponent<GameFieldCell>().value == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsCellDead(int row, int column)
    {
        if (gameFieldCells[row, column].GetComponent<GameFieldCell>().isUsed == false)
        {
            int deadCount = 0;
            if (column > 0)
            {
                if (gameFieldCells[row, column - 1].GetComponent<GameFieldCell>().isUsed == true)
                {
                    deadCount += 1;
                }
            }
            if (column == 0)
            {
                deadCount += 1;
            }
            if (column < columns)
            {
                if (gameFieldCells[row, column + 1].GetComponent<GameFieldCell>().isUsed == true)
                {
                    deadCount += 1;
                }
            }
            if (column == columns)
            {
                deadCount += 1;
            }
            if (row > 0)
            {
                if (gameFieldCells[row - 1, column].GetComponent<GameFieldCell>().isUsed == true)
                {
                    deadCount += 1;
                }
            }
            if (row == 0)
            {
                deadCount += 1;
            }
            if (row < rows)
            {
                if (gameFieldCells[row + 1, column].GetComponent<GameFieldCell>().isUsed == true)
                {
                    deadCount += 1;
                }
            }
            if (row == rows)
            {
                deadCount += 1;
            }

            if (deadCount == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        else
        {
            return false;
        }
    }

    public void OnClicked(int row, int column)
    {
        // Handle the cell that was clicked
        gameFieldCells[row, column].GetComponent<GameFieldCell>().isUsed = true;
        gameFieldCells[row, column].GetComponent<GameFieldCell>().UpdateLook();

        // Handle its neighbours
        
        // Check if win condition "Target value reached" is met
        
        // Check if loss condition "No more moves" is met

        // Check if loss condition "Target value exceeded" is met
    }
}