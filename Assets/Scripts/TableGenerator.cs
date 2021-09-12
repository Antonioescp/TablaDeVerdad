using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TableGenerator : MonoBehaviour
{
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private GameObject cell;

    private GameObject[,] cells;

    private void Start()
    {
        GenerateTable(SceneManager.GetActiveScene().buildIndex);
    }

    private void GenerateTable(int varibalesQty)
    {
        switch(varibalesQty)
        {
            case 1:
                cells = new GameObject[3, 1];
                for(int i = 0; i < cells.GetLength(0); i++)
                {
                    for(int j = 0; j < cells.GetLength(1); j++)
                    {
                        cells[i, j] = Instantiate(cell, mainCanvas.transform.position, Quaternion.identity);

                        cells[i, j].transform.SetParent(mainCanvas.transform);
                    }
                }
                break;
        }
    }
}