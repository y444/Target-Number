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

        // Create an array of game field cells
        gameFieldCells = new GameObject[rows,columns];

        // Instantiate game field cells and set their values
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Instantiate & name
                this.gameFieldCells[i, j] = Instantiate(cellPrefab, gameField.transform);
                this.gameFieldCells[i, j].name = "Button Row " + fieldGenerator.cells[i, j].row.ToString() + " Col " + fieldGenerator.cells[i, j].column.ToString();
                this.gameFieldCells[i, j].GetComponent<GameFieldCell>().gameplayManager = this.gameObject;

                // Set values from the generator
                this.gameFieldCells[i, j].GetComponent<GameFieldCell>().row = this.fieldGenerator.cells[i, j].row;
                this.gameFieldCells[i, j].GetComponent<GameFieldCell>().column = this.fieldGenerator.cells[i, j].column;
                this.gameFieldCells[i, j].GetComponent<GameFieldCell>().value = this.fieldGenerator.cells[i, j].value;
                this.gameFieldCells[i, j].GetComponent<GameFieldCell>().isTarget = this.fieldGenerator.cells[i, j].isTarget;

                // Set newly calculated values
                this.gameFieldCells[i, j].GetComponent<GameFieldCell>().isUsed = IsCellUsed(i, j);
                this.gameFieldCells[i, j].GetComponent<GameFieldCell>().isZero = IsCellZero(i, j);
                this.gameFieldCells[i, j].GetComponent<GameFieldCell>().isDead = IsCellUsed(i, j);
            }
        }

        // Set global value text in the top HUD
        targetText.GetComponent<Text>().text = targetValue.ToString();
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
        //TODO
        return false;
    }

    public void OnClicked(int row, int column)
    {
        //TODO all that jazz
    }
}