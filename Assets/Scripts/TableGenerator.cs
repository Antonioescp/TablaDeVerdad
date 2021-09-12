using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class TableGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cell;

    private GameObject[,] cells;

    SpriteRenderer cellSpRenderer;

    private void Awake()
    {
        // Getting cell sprite
        cellSpRenderer = cell.GetComponent<SpriteRenderer>();

        // Generating table OnLevelLoaded
        GenerateTable(2);
    }

    /// <summary>
    /// It runs when the level starts
    /// </summary>
    private void OnLevelLoaded()
    {
        GenerateTable(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Generates a table based on the amount of variables
    /// </summary>
    /// <param name="variablesQty">Amount of variables</param>
    private void GenerateTable(int variablesQty)
    {
        // Calculating amount of cells required based on amount of variables
        int columns = variablesQty;
        int rows = (int)Mathf.Pow(2, columns) + 1;
        
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

                Debug.Log($"Cell [{row},{col}]: {position.x} {position.y}");

                cells[row, col] = Instantiate(cell, position, Quaternion.identity);
            }
        }
    }
}