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

    const int GameElementsLayer = 9;
    const string LevelFolder = "LevelFiles";
    const string LevelFileNaming = "level_";
    const int TableSize = 20;

    BoardManager boardManager;

    private static int numOfLevels;
    private static int? lastLoadedLevel;

    private void Start() {
        GameEvents.current.onLevelLoad += LoadLevel;
        GameEvents.current.onRestartLevel += RestartLevel;
        GameEvents.current.onNextLevelLoad += LoadNextLevel;
    }
    private void Awake() {
        boardManager = (BoardManager)GameObject.Find("Board").GetComponent(typeof(BoardManager));
        numOfLevels = GetNumOfLevels(new DirectoryInfo(LevelFolder));
    }


    /* =============================================================== PUBLIC METHODS =============================================================== */

    /// <summary>
    /// Loads a level given its number
    /// </summary>
    /// <param name="levelNumber"> Number of level to load </param>
    public void LoadLevel(int levelNumber) {
        SetTableElements(ReadLevelTable(levelNumber));
        SetLevelScript(levelNumber);
        lastLoadedLevel = levelNumber;
    }

    /// <summary>
    /// Restarts the current playing level
    /// </summary>
    public void RestartLevel() {
        LoadLevel((int)lastLoadedLevel);
    }

    /// <summary>
    /// Loads next level
    /// </summary>
    public void LoadNextLevel() {
        LoadLevel((int)lastLoadedLevel + 1);
    }

    /// <summary>
    /// Returns the current number of levels in the game
    /// </summary>
    /// <param name="di"> DirectoryInfo of folder that contains the level scripts </param>
    /// <returns></returns>
    public static int GetNumOfLevels(DirectoryInfo di) {
        int numOfLevels = 0; 
        FileInfo[] fiList = di.GetFiles();
        foreach (FileInfo fi in fiList) {
            if (fi.Name.Contains(LevelFileNaming) && fi.Extension.Contains("cs")) {
                numOfLevels++;
            }
        }
        return numOfLevels;
    }

    /// <summary>
    /// Check if the current playing level is the last one
    /// </summary>
    /// <returns> true if current level is the last one, false otherwise</returns>
    public static bool IsLastLevel() {
        if ((int)lastLoadedLevel == numOfLevels - 1) {
            return true;
        }
        return false;
    }

    /* =============================================================== PRIVATE METHODS =============================================================== */

    /// <summary>
    /// Sets the game elements onto the game board acording to the given level table
    /// </summary>
    /// <param name="table"> char table that contains the distribution of game objects in the board for each level </param>
    private void SetTableElements(char [,] table) {

        CleanTable();
        
        System.Random random = new System.Random();
 
        for (int i = 0; i < table.GetLength(0); i++) {
            for (int j = 0; j < table.GetLength(1); j++) {
                switch(table[i,j]) {
                    
                    case '1': { // movable rock
                        int rnd = random.Next(0, movableRocks.Length -1);
                        Instantiate(movableRocks[rnd], boardManager.GetCenterPointOfTile(i,j), Quaternion.identity, transform);
                        break;
                    }
                    case '2': { // fixed rock 
                        int rnd = random.Next(0, fixedRocks.Length -1);
                        Instantiate(fixedRocks[rnd], boardManager.GetCenterPointOfTile(i,j), Quaternion.identity, transform);
                        break;
                    }
                    case '3': { // ore
                        Instantiate(ore, boardManager.GetCenterPointOfTile(i,j), Quaternion.identity, transform);
                        break;
                    }
                    case 'u': { // character cube (looking up) (-x)
                        Instantiate(character, boardManager.GetCenterPointOfTile(i,j), transform.rotation * Quaternion.Euler (0f, 180f, 0f), transform);
                        break;
                    }
                    case 'd': { // character cube (looking down) (x)
                        Instantiate(character, boardManager.GetCenterPointOfTile(i,j), transform.rotation * Quaternion.Euler (0f, 0f, 0f), transform);
                        break;
                    }
                    case 'l': { // character cube (looking left) (-z)
                        Instantiate(character, boardManager.GetCenterPointOfTile(i,j), transform.rotation * Quaternion.Euler (0f, 90f, 0f), transform);
                        break;
                    }
                    case 'r': { // character cube (looking right) (z)
                        Instantiate(character, boardManager.GetCenterPointOfTile(i,j), transform.rotation * Quaternion.Euler (0f, -90f, 0f), transform);
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
        var string_table = File.ReadAllLines(LevelFolder + "/" + LevelFileNaming + levelNumber + ".csv").Select(l => l.Split(',').ToArray()).ToArray();
        
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
    /// Destroys all objects from Game Elements layer that exists in the current game scene
    /// </summary>
    private void CleanTable() {
        foreach (Transform child in transform) {
            if (child.gameObject.layer == GameElementsLayer) {
                Destroy(child.gameObject);
            }
        }
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
        GameEvents.current.SetOreGoal();
    }
}