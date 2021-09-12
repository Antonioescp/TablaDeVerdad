using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class TableGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cell;

    // Table cells
    private GameObject[,] cells;

    // To position rows and columns accordingly with respect to screen center
    SpriteRenderer cellSpRenderer;

    // Spawning timer (spawn one after another)
    Timer spawningTimer;

    // Current row and column
    private int row = 0;
    private int column = 0;

    /// <summary>
    /// Just to set things up
    /// </summary>
    private void Start()
    {
        // Getting cell sprite
        cellSpRenderer = cell.GetComponent<SpriteRenderer>();

        // Setting timer for spawning cells
        spawningTimer = gameObject.AddComponent<Timer>();
        spawningTimer.Duration = 0.15f;
        spawningTimer.AddListener(SpawnNextCell);

        // Listening for scene change
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }


    /// <summary>
    /// Called on scene change
    /// </summary>
    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch(scene.name)
        {
            case "Level1":
                OnLevelLoaded(2, 4);
                break;
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
            spawningTimer.Stop();
        }
    }

    /// <summary>
    /// It runs when the level starts
    /// </summary>
    private void OnLevelLoaded(int variables, int expressions)
    {
        GenerateTable(variables, expressions);
        SpawnNextCell();
    }

    /// <summary>
    /// Generates a table based on the amount of variables
    /// </summary>
    /// <param name="variablesQty">Amount of variables</param>
    private void GenerateTable(int variablesQty, int expressionsQty)
    {
        // Calculating amount of cells required based on amount of variables
        int columns = variablesQty + expressionsQty;
        int rows = (int)Mathf.Pow(2, variablesQty) + 1;
        
        // Creating table
        cells = new GameObject[rows, columns];

        // Position calculation based on rows and columns amount
        Func<int, float> calculateColumnPosition;
        Func<int, float> calculateRowPosition;

        // Selecting calculation process depending on amount of columns
        if (columns % 2 == 0)
            calculateColumnPosition = (pos) => (pos - columns / 2) * cellSpRenderer.size.x + cellSpRenderer.size.x / 2;
        else
            calculateColumnPosition = (pos) => (pos - columns / 2) * cellSpRenderer.size.x;

        // Selecting calculation process depending on amount of rows
        if (rows % 2 == 0)
            calculateRowPosition = (pos) => (rows / 2 - pos) * cellSpRenderer.size.y + cellSpRenderer.size.y / 2;
        else
            calculateRowPosition = (pos) => (rows / 2 - pos) * cellSpRenderer.size.y;

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

                // To make headers unclickable
                if (row == 0)
                    currentCell.IsHeader = true;
                else
                    currentCell.IsHeader = false;
            }
        }
    }
}