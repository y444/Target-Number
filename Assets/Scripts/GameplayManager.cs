using System.Collections;
using System.Collections.Generic;
//using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

public enum InitIntent
{
    NewLevel,
    ResetLevel
};

public class GameplayManager : MonoBehaviour
{

    public GameObject gameStateManager;
    public GameObject cellPrefab;
    public GameObject gameField;
    public GameObject levelManager;
    public GameObject helpMessageManager;
    public GameObject progressBar;
    public GameObject progressBarUnderlay;
    public float progressBarDefaultWidth;
    public Color32 progressBarDefaultColor;
    public Color32 progressBarOvershootColor;

    public GameObject soundPlayer;
    public AudioSource normalPressSound;
    public AudioSource usedPressSound;
    public AudioSource arrowsOnSound;
    public AudioSource arrowsOffSound;
    public AudioSource targetSound;

    public Text globalTargetText;
    public Text currentTargetText;

    public int rows;
    public int columns;
    public int maxValue;
    public int numberOfTargets;
    public int targetValue;
    public int movesLimit;

    int currentTargetValue;
    FieldGenerator fieldGenerator;
    

    public GameFieldCell[,] gameFieldCells;

    public void Init(InitIntent intent)
    {
        // Get gameplay parameters from level manager
        rows = levelManager.GetComponent<LevelManager>().rows;
        columns = levelManager.GetComponent<LevelManager>().columns;
        maxValue = levelManager.GetComponent<LevelManager>().maxValue;
        numberOfTargets = levelManager.GetComponent<LevelManager>().numberOfTargets;
        movesLimit = levelManager.GetComponent<LevelManager>().movesLimit;

        // Fix grid layout to make correct number of columns
        gameField.GetComponent<GridLayoutGroup>().constraintCount = columns;

        // Generate the field & pass the target value
        if (intent == InitIntent.NewLevel)
        {
            fieldGenerator = new FieldGenerator(rows, columns, maxValue, numberOfTargets, movesLimit);
        }

        targetValue = fieldGenerator.targetValue;

        // Set global value text in the top HUD
        currentTargetValue = 0;
        globalTargetText.text = targetValue.ToString();
        currentTargetText.text = currentTargetValue.ToString();

        // Set progress bar
        resetProgressBar();

        // Create an array of game field cells
        gameFieldCells = new GameFieldCell[rows, columns];

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
                cell.helpMessageManager = helpMessageManager.GetComponent<HelpMessageManager>();
                cell.soundPlayer = soundPlayer.GetComponent<SoundPlayer>();
                // Here goes something ugly IMHO
                cell.normalPressSound = normalPressSound;
                cell.usedPressSound = usedPressSound;
                cell.arrowsOnSound = arrowsOnSound;
                cell.arrowsOffSound = arrowsOffSound;

                // Set values from the generator
                cell.row = fieldGenerator.cells[i, j].row;
                cell.column = fieldGenerator.cells[i, j].column;
                cell.Value = fieldGenerator.cells[i, j].value;
                cell.isTarget = fieldGenerator.cells[i, j].isTarget;
            }
        }

        // Set game state to normal
        gameStateManager.GetComponent<GameStateManager>().StateChange(GameStates.Normal);
    }

    public void Reset()
    {
        foreach (Transform child in gameField.transform)
        {
            GameObject.Destroy(child.gameObject);
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

    int GetCurrentTargetValue()
    {
        int currentTargetValue = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (gameFieldCells[i,j].isTarget == true)
                {
                    currentTargetValue += gameFieldCells[i, j].Value;
                }
            }
        }
        return currentTargetValue;
    }

    bool HasMoves()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (gameFieldCells[i, j].IsDead == false && gameFieldCells[i, j].isTarget == true)
                {
                    return true;
                }
            }
        }

        return false;
    }

    void CheckReportGameState()
    {
        int currentTargetValue = GetCurrentTargetValue();
        //Debug.Log(currentTargetValue);
        bool hasMoves = HasMoves();
        //Debug.Log(hasMoves);

        if (currentTargetValue < targetValue)
        {
            if (!hasMoves)
            {
                gameStateManager.GetComponent<GameStateManager>().StateChange(GameStates.LostNoMoves);
            }
        }

        else if (currentTargetValue == targetValue)
        {
            gameStateManager.GetComponent<GameStateManager>().StateChange(GameStates.Won);
        }

        else if (currentTargetValue > targetValue)
        {
            gameStateManager.GetComponent<GameStateManager>().StateChange(GameStates.LostOvershoot);
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

        // Check if target value changed
        if (currentTargetValue != GetCurrentTargetValue())
        {
            // Play sound
            soundPlayer.GetComponent<SoundPlayer>().Play(targetSound);

            // Animate
            currentTargetText.transform.parent.GetComponent<Animator>().SetTrigger("globalTargetGrowthTrigger");

            // Update target value display
            currentTargetValue = GetCurrentTargetValue();
            globalTargetText.text = targetValue.ToString();
            currentTargetText.text = currentTargetValue.ToString();
            growProgressBar();
        }

        // Check if the game state has changed and report to the game state manager
        CheckReportGameState();
    }

    public void growProgressBar()
    {
        float x = progressBar.GetComponent<RectTransform>().rect.width;
        float totalWidth = progressBarUnderlay.GetComponent<RectTransform>().rect.width;
        float height = progressBar.GetComponent<RectTransform>().rect.height;
        float newWidth = totalWidth * currentTargetValue / targetValue;
        RectTransform progressBarSize = progressBar.GetComponent<RectTransform>();
        if (newWidth > progressBarDefaultWidth)
        {
            progressBarSize.sizeDelta = new Vector2(newWidth, height);
        }
        if (newWidth > totalWidth)
        {
            progressBarSize.sizeDelta = new Vector2(totalWidth, height);
            progressBar.GetComponent<Image>().color = progressBarOvershootColor;    
        }
    }

    public void resetProgressBar()
    {
        float height = progressBar.GetComponent<RectTransform>().rect.height;
        RectTransform progressBarSize = progressBar.GetComponent<RectTransform>();
        progressBarSize.sizeDelta = new Vector2(progressBarDefaultWidth, height);
        progressBar.GetComponent<Image>().color = progressBarDefaultColor;
    }
}