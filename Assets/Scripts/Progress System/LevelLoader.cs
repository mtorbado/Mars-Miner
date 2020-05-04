﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public GameObject[] movableRocks;
    public GameObject[] fixedRocks;
    public GameObject character;

    const String LevelFolderPath = "LevelFiles/";
    const string LevelFileNaming = "level_";
    const int TableSize = 20;
    BoardManager boardManager;

    private void Awake() 
    {
        boardManager = (BoardManager)GameObject.Find("Board").GetComponent(typeof(BoardManager));
        SetTableElements(ReadLevelTable(0));
    }

    private void Start() 
    {
        
    }

    /* =================================================================================================================================== */

    private void SetTableElements(char [,] table)
    {
        System.Random random = new System.Random();  
 
         for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (table[i,j] == '1') { // movable rock
                        int rnd = random.Next(0, movableRocks.Length -1);
                        Instantiate(movableRocks[rnd], boardManager.GetCenterPointOfTile(i,j), Quaternion.identity);
                    }
                    else if (table[i,j] == '2') { //fixed rock
                        int rnd = random.Next(0, fixedRocks.Length -1);
                        Instantiate(fixedRocks[rnd], boardManager.GetCenterPointOfTile(i,j), Quaternion.identity);
                    }

                    else if (table[i,j] == 'u' || table[i,j] == 'd'|| table[i,j] =='l' || table[i,j] == 'r') //character cube
                    {
                        //set character cube
                    }

                }
            }
    }

    private GameObject GetRndGameObjectOfType(int type) 
    {
        throw new NotImplementedException();
    }

    private char[,] ReadLevelTable(int levelNumber) 
    {
        char[,] table = new char[TableSize,TableSize];
        var string_table = File.ReadAllLines(LevelFolderPath + LevelFileNaming + levelNumber + ".csv").Select(l => l.Split(',').ToArray()).ToArray();
        try
        {
            for (int i = 0; i < TableSize; i++)
            {
                for (int j = 0; j < TableSize; j++)
                {
                    char letter;
                    bool ok = char.TryParse(string_table[i][j], out letter);
                    if (ok)
                    {
                        table[i,j] = letter;
                    }
                    else
                    {
                        table[i,j] = '0';
                    }
                }
            }
        } catch (InvalidOperationException)
            {
                throw new InvalidOperationException("The given jagged array is not rectangular.");
            }

        return table;
    }
}
