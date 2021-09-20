using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class TableGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cell;
    [SerializeField] private GameEvent onInspectTable;
    [SerializeField] private GameEvent onFinishedSpawningCells;

    // Table cells
    private GameObject[,] cells;

    // To position rows and columns accordingly with respect to screen center
    SpriteRenderer cellSpRenderer;

    // Spawning timer (spawn one after another)
    Timer spawningTimer;

    // Timer to validate each cell
    Timer validationTimer;

    // Current row and column
    private int row = 0;
    private int column = 0;

    private void Awake()
    {
        // Getting cell sprite
        cellSpRenderer = cell.GetComponent<SpriteRenderer>();

        // Setting timer for spawning cells
        spawningTimer = gameObject.AddComponent<Timer>();

        // Timer for highlighting cells
        // validationTimer = gameObject.AddComponent<Timer>();
        // validationTimer.Duration = .1f;
        // validationTimer.AddListener(null);

        onInspectTable.AddListener(OnInspectTable);
    }

    /// <summary>
    /// Just to set things up
    /// </summary>
    private void Start()
    {
        spawningTimer.Duration = 0.1f;
        spawningTimer.AddListener(SpawnNextCell);

        // Generando tabla de la verdad a partir de datos de nivel
        GenerateTable(ConfigurationUtils.Tables[GameManager.LevelSelected].variables.Count, ConfigurationUtils.Tables[GameManager.LevelSelected].expressions.Count);
        SpawnNextCell();
    }

    private void OnInspectTable()
    {
        Table table = ConfigurationUtils.Tables[GameManager.LevelSelected];

        for(int row = 1; row < table.Rows; row++)
        {
            for(int col = 0; col < table.Columns; col++)
            {
                cells[row, col].GetComponent<Cell>().Check(cells[row, col].GetComponent<Cell>().State == table.values[row - 1, col]);
            }
        }
    }

    /// <summary>
    /// Sets a cell activated
    /// </summary>
    private void SpawnNextCell()
    {
        if(column < cells.GetLength(1))
        {
            if(row < cells.GetLength(0))
            {
                cells[row, column].SetActive(true);
                row++;
            }
            else
            {
                row = 0;
                column++;
            }
            spawningTimer.Run();
        }
        else
        {
            onFinishedSpawningCells.Raise();
            spawningTimer.Stop();
        }
    }

    /// <summary>
    /// Generates a table based on the amount of variables and expressions
    /// </summary>
    /// <param name="variablesQty">Amount of variables</param>
    /// <param name="expressionsQty">Amount of expressions</param>
    private void GenerateTable(int variablesQty, int expressionsQty)
    {
        // Calculating amount of cells required based on amount of variables and expressions
        int columns = variablesQty + expressionsQty;
        int rows = (int)Mathf.Pow(2, variablesQty) + 1;
        
        // Creating table
        cells = new GameObject[rows, columns];

        // Position calculation based on rows and columns amount
        Func<int, float> calculateColumnPosition;
        Func<int, float> calculateRowPosition;

        // Selecting calculation process depending on amount of columns
        if (columns % 2 == 0)
            calculateColumnPosition = (pos) => (pos - columns / 2) * cellSpRenderer.bounds.size.x + cellSpRenderer.bounds.size.x / 2;
        else
            calculateColumnPosition = (pos) => (pos - columns / 2) * cellSpRenderer.bounds.size.x;

        // Selecting calculation process depending on amount of rows
        if (rows % 2 == 0)
            calculateRowPosition = (pos) => (rows / 2 - pos) * cellSpRenderer.bounds.size.y + cellSpRenderer.bounds.size.y / 2;
        else
            calculateRowPosition = (pos) => (rows / 2 - pos) * cellSpRenderer.bounds.size.y;

        // Instantiating cells and positioning them
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 position = new Vector2();

                position.x = calculateColumnPosition(col);
                position.y = calculateRowPosition(row);

                cells[row, col] = Instantiate(cell, position, Quaternion.identity);
                cells[row, col].SetActive(false);

                // Setting cell information
                Cell currentCell = cells[row, col].GetComponent<Cell>();
                currentCell.RowNumber = row;
                currentCell.ColumnNumber = col;
            }
        }
    }
}