using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{

    public GameObject gameStateManager;
    public GameObject cellPrefab;
    public GameObject gameField;
    public Text targetText;

    public int rows;
    public int columns;
    public int maxValue;
    public int targetValue;
    public int numberOfTargets;

    public GameFieldCell[,] gameFieldCells;

    void Awake()
    {

        // TODO Get gameplay parameters from level manager

        // Fix grid layout to make correct number of columns
        gameField.GetComponent<GridLayoutGroup>().constraintCount = columns;

        // Generate the field & pass the target value
        FieldGenerator fieldGenerator = new FieldGenerator(rows, columns, maxValue, numberOfTargets);
        targetValue = fieldGenerator.targetValue;

        // Set global value text in the top HUD
        targetText.text = targetValue.ToString();

        // Create an array of game field cells
        gameFieldCells = new GameFieldCell[rows,columns];

        // Instantiate game field cells and set their values
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Instantiate & name
                var cell = Instantiate(cellPrefab, gameField.transform).GetComponent<GameFieldCell>();
                gameFieldCells[i, j] = cell;
                cell.gameObject.name = "Button Row " + fieldGenerator.cells[i, j].row.ToString() + " Col " + fieldGenerator.cells[i, j].column.ToString();
                cell.gameplayManager = gameObject.GetComponent<GameplayManager>();

                // Set values from the generator
                cell.row = fieldGenerator.cells[i, j].row;
                cell.column = fieldGenerator.cells[i, j].column;
                cell.Value = fieldGenerator.cells[i, j].value;
                cell.isTarget = fieldGenerator.cells[i, j].isTarget;
            }
        }
    }

    bool IsCellDead(int row, int column)
    {
        if (gameFieldCells[row, column].IsUsed)
        {
            return true;
        }

        bool isDead = true;

        isDead &= IsCellExist(row + 1, column) == false || gameFieldCells[row + 1, column].IsUsed;
        isDead &= IsCellExist(row - 1, column) == false || gameFieldCells[row - 1, column].IsUsed;
        isDead &= IsCellExist(row , column + 1) == false || gameFieldCells[row, column + 1].IsUsed;
        isDead &= IsCellExist(row , column - 1) == false || gameFieldCells[row, column - 1].IsUsed;

        return isDead;
    }

    void SetIsCellDeadIfExists(int row, int column)
    {
        if (IsCellExist(row, column))
        {
            gameFieldCells[row, column].IsDead = IsCellDead(row, column);
        }
    }

    bool IsCellExist(int row, int column)
    {
        return row >= 0 && row < rows && column >= 0 && column < columns && gameFieldCells[row, column] != null;
    }

    public bool IsCellExistAndIsAlive(int row, int column)
    {
        return IsCellExist(row, column) && !gameFieldCells[row, column].IsDead;
    }

    void AddIfExistsAndNotUsed(int row, int column, int value)
    {
        if (IsCellExist(row, column) && gameFieldCells[row, column].IsUsed == false)
        {
            gameFieldCells[row, column].Value += value;
        }
    }

    public void OnClicked(int row, int column)
    {
        // Handle the cell that was clicked
        var cell = gameFieldCells[row, column];

        // Add its value to live neighbours
        AddIfExistsAndNotUsed(row + 1, column, cell.Value);
        AddIfExistsAndNotUsed(row - 1, column, cell.Value);
        AddIfExistsAndNotUsed(row, column + 1, cell.Value);
        AddIfExistsAndNotUsed(row, column - 1, cell.Value);

        // Make neighbours dead if needed
        SetIsCellDeadIfExists(cell.row, cell.column);
        SetIsCellDeadIfExists(row + 1, column);
        SetIsCellDeadIfExists(row - 1, column);
        SetIsCellDeadIfExists(row, column + 1);
        SetIsCellDeadIfExists(row, column - 1);

        // Check if the game is won and report if needed

        // Check if the game is lost and report if needed
        // Lose by having no more moves

        // Lose by overshooting the target value
    }
}