using System;
using System.IO;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class for loading game levels
/// </summary>
public class LevelLoader : MonoBehaviour {

    public GameObject[] movableRocks;
    public GameObject[] fixedRocks;
    public GameObject character;
    public GameObject ore;

    const string LevelFolderPath = "LevelFiles/";
    const string LevelFileNaming = "level_";
    const int TableSize = 20;
    BoardManager boardManager;

    private void Start() {
        GameEvents.current.onLevelLoad += LoadLevel;
    }
    private void Awake() {
        boardManager = (BoardManager)GameObject.Find("Board").GetComponent(typeof(BoardManager));
    }

    public void LoadLevel(int levelNumber) {
        SetTableElements(ReadLevelTable(levelNumber));
        SetLevelScript(levelNumber);
    }

    /// <summary>
    /// Sets the game elements onto the game board acording to the given level table
    /// </summary>
    /// <param name="table"> char table that contains the distribution of game objects in the board for each level </param>
    private void SetTableElements(char [,] table) {

        System.Random random = new System.Random();
 
        for (int i = 0; i < table.GetLength(0); i++) {
            for (int j = 0; j < table.GetLength(1); j++) {
                switch(table[i,j]) {
                    
                    case '1': { // movable rock
                        int rnd = random.Next(0, movableRocks.Length -1);
                        Instantiate(movableRocks[rnd], boardManager.GetCenterPointOfTile(i,j), Quaternion.identity);
                        break;
                    }
                    case '2': { // fixed rock 
                        int rnd = random.Next(0, fixedRocks.Length -1);
                        Instantiate(fixedRocks[rnd], boardManager.GetCenterPointOfTile(i,j), Quaternion.identity);
                        break;
                    }
                    case '3': { // ore
                        Instantiate(ore, boardManager.GetCenterPointOfTile(i,j), Quaternion.identity);
                        break;
                    }
                    case 'u': { // character cube (looking up) (-x)
                        Instantiate(character, boardManager.GetCenterPointOfTile(i,j), transform.rotation * Quaternion.Euler (0f, 180f, 0f));
                        break;
                    }
                    case 'd': { // character cube (looking down) (x)
                        Instantiate(character, boardManager.GetCenterPointOfTile(i,j), transform.rotation * Quaternion.Euler (0f, 0f, 0f));
                        break;
                    }
                    case 'l': { // character cube (looking left) (-z)
                        Instantiate(character, boardManager.GetCenterPointOfTile(i,j), transform.rotation * Quaternion.Euler (0f, 90f, 0f));
                        break;
                    }
                    case 'r': { // character cube (looking right) (z)
                        Instantiate(character, boardManager.GetCenterPointOfTile(i,j), transform.rotation * Quaternion.Euler (0f, -90f, 0f));
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Reads the required csv level file given its number and returns it as a char jagged array
    /// </summary>
    /// <param name="levelNumber"> Number of level to load </param>
    /// <returns> char table with csv file content </returns>
    private char[,] ReadLevelTable(int levelNumber) {

        char[,] table = new char[TableSize,TableSize];
        var string_table = File.ReadAllLines(LevelFolderPath + LevelFileNaming + levelNumber + ".csv").Select(l => l.Split(',').ToArray()).ToArray();
        
        try {
            for (int i = 0; i < TableSize; i++) {
                for (int j = 0; j < TableSize; j++) {

                    char letter;
                    bool ok = char.TryParse(string_table[i][j], out letter);

                    if (ok) table[i,j] = letter;
                    else table[i,j] = '0';
                }
            }
        } catch (InvalidOperationException) { throw new InvalidOperationException("The given jagged array is not rectangular.");}

        return table;
    }

    /// <summary>
    /// Locates all character cubes in the scene and adds the propper level script to them
    /// </summary>
    /// <param name="levelNumber">Number of level to load</param>
    private void SetLevelScript(int levelNumber) {
        GameObject[] characterCubes = GameObject.FindGameObjectsWithTag("CharacterCube");
        foreach(GameObject cc in characterCubes) {
            cc.AddComponent(Type.GetType("Level" + levelNumber));
        }
    }
}