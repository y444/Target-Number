using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{

    public GameObject buttonPrefab;
    public GameObject gameField;

    public int rows;
    public int columns;
    public int maxValue;
    public int targetValue;
    public int numberOfTargets;

    public CellMatrix cellMatrix;

    void Awake()
    {

        // TODO Get gameplay parameters from level manager

        // Generate the matrix
        cellMatrix = new CellMatrix(rows, columns, maxValue, targetValue, numberOfTargets);

        // Fix grid layout to make correct number of columns
        gameField.GetComponent<GridLayoutGroup>().constraintCount = columns;

        // Instantiate buttons
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject instantiatedButton = Instantiate(buttonPrefab, gameField.transform);
                instantiatedButton.name = "Button Row " + cellMatrix.cells[i, j].row.ToString() + " Col " + cellMatrix.cells[i, j].column.ToString();
                instantiatedButton.GetComponent<GameFieldButton>().gameplayManager = this.gameObject;
                instantiatedButton.GetComponent<GameFieldButton>().row = this.cellMatrix.cells[i, j].row;
                instantiatedButton.GetComponent<GameFieldButton>().column = this.cellMatrix.cells[i, j].column;
            }
        }
    }

    public ButtonState GetButtonState(int row, int column)
    {
        ButtonState buttonState;

        // Check for used state
        if (this.cellMatrix.cells[row, column].isUsed == true)
        {
            buttonState = ButtonState.used;
        }

        // If not check for dead state
        else if (IsDead(row, column))
        {
            buttonState = ButtonState.dead;
        }

        // If not check for zero state
        else if (this.cellMatrix.cells[row, column].value == 0)
        {
            buttonState = ButtonState.zero;
        }

        // If none of the above then it's normal
        else
        {
            buttonState = ButtonState.normal;
        }

        return buttonState;
    }

    public string GetButtonText(int row, int column)
    {
        return this.cellMatrix.cells[row, column].value.ToString();
    }

    public void GameFieldAction(int row, int column)
    {

    }

    public bool IsDead(int row, int column)
    {

        //TODO
        return false;
    }
}

public enum ButtonState { normal, used, zero, dead };