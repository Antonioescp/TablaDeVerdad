using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Table
{
    public List<string> variables;
    public List<string> expressions;
    public CellState[,] values;

    public int Rows => (int)Mathf.Pow(2, variables.Count) + 1;
    public int Columns => variables.Count + expressions.Count;

    public Table()
    {
        variables = new List<string>();
        expressions = new List<string>();
    }

    public void Print()
    {
        string table = $"Table [{Rows}, {Columns}]\n";
        foreach(var variable in variables)
            table += variable + ",";

        table += "\n";
        foreach(var exp in expressions)
            table += exp + ",";

        table += "\n";
        table += $"Values, Rows: {values.GetLength(0)}, Columns: {values.GetLength(1)}\n";
        for(int r = 0; r < values.GetLength(0); r++)
        {
            for(int c = 0; c < values.GetLength(1); c++)
            {
                table += values[r, c] + "\t";
            }
            table += "\n";
        }

        Debug.Log(table);
    }
}