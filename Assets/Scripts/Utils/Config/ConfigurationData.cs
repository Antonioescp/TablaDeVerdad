using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{

    const string ConfigurationDataFileName = "ConfigFiles/LevelConfig.txt";

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

    List<Table> tables;

    public List<Table> Tables => tables;

    public ConfigurationData()
    {
        StreamReader configFile = null;
        tables = new List<Table>();
        try
        {
            configFile = File.OpenText(Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName));
            string line = configFile.ReadLine();
            while(line != null)
            {
                Table newTable = new Table();

                // loading table variables
                foreach(var tableVariable in configFile.ReadLine().Split(','))
                {
                    newTable.variables.Add(tableVariable);
                }

                // loading table expressions 
                foreach(var tableExpression in configFile.ReadLine().Split(','))
                    if(!String.IsNullOrEmpty(tableExpression))
                        newTable.expressions.Add(tableExpression);

                // Calculating rows and columns amount
                int columns = newTable.variables.Count + newTable.expressions.Count;
                int rows = (int)Mathf.Pow(2, newTable.variables.Count);

                newTable.values = new CellState[rows, columns];

                // Getting truth values
                string[] col = configFile.ReadLine().Split(';').Where( x => !String.IsNullOrEmpty(x)).ToArray();

                for(int i = 0; i < col.Length; i++)
                {
                    string[] values = col[i].Split(',');
                    for(int j = 0; j < values.Length; j++)
                    {
                        if(!String.IsNullOrEmpty(values[j]))
                            newTable.values[j, i] = values[j] == "v" ? CellState.True : CellState.False;
                    }
                }

                // adding table to configuration file
                tables.Add(newTable);

                line = configFile.ReadLine();
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
        finally
        {
            if (configFile != null)
                configFile.Close();
        }
    }
}
