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
    const int TableSize = 20;

    BoardManager boardManager;
    ScoreManager scoreManager;
    InGameUICanvasActions inGameUI;

    private static int? lastLoadedLevel;

    private void Start() {
        GameEvents.current.onLevelLoad += LoadLevel;
        GameEvents.current.onRestartLevel += RestartLevel;
        GameEvents.current.onNextLevelLoad += LoadNextLevel;
        GameEvents.current.onRandomLevelLoad += LoadRandomLevel;
        GameEvents.current.onSelectLevel += CleanTable;
    }
    private void Awake() {
        boardManager = (BoardManager)GameObject.Find("Board").GetComponent(typeof(BoardManager));
        scoreManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<ScoreManager>();
        inGameUI = GameObject.FindGameObjectWithTag("InGameUI").GetComponent<InGameUICanvasActions>();
    }


    /* =============================================================== PUBLIC METHODS =============================================================== */

    /// <summary>
    /// Loads a level given its number
    /// </summary>
    /// <param name="levelNumber"> Number of level to load </param>
    public void LoadLevel(int levelNumber) {
        SetTableElements(ReadLevelTable(levelNumber));
        SetLevelScript(levelNumber);
        SetLevelTextCode(levelNumber);
        lastLoadedLevel = levelNumber;
    }

    /// <summary>
    /// Loads a random level of the given dificulty, excluding the last one played
    /// </summary>
    /// <param name="levelDificulty"> Dificulty of level to load </param>
    public void LoadRandomLevel() {
        int levelNumber =0;
        LevelDificulty ld = scoreManager.GetDificulty((int)lastLoadedLevel);
        System.Random r = new System.Random();
        switch(ld) {
            case LevelDificulty.Easy:
                levelNumber = r.Next((int)LevelInfo.FirstEasy, (int)LevelInfo.FirstMedium - 1);
                break;
            case LevelDificulty.Medium:
                levelNumber = r.Next((int)LevelInfo.FirstMedium, (int)LevelInfo.FirstHard - 1);
                break;
            case LevelDificulty.Hard:
                levelNumber = r.Next((int)LevelInfo.FirstHard, (int)LevelInfo.FirstChallenge - 1);
                break;
            case LevelDificulty.Challenge:
                levelNumber = r.Next((int)LevelInfo.FirstChallenge, (int)LevelInfo.LastLevel - 1);
                break;
        }
        if (levelNumber == lastLoadedLevel) {
            levelNumber++;
        }
        LoadLevel(levelNumber);
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

    public static bool IsLastLevel() {
        return (lastLoadedLevel == (Int32)LevelInfo.LastLevel);
    }

    public void SetLevelTextCode(int levelNumber) {
        TextAsset txt=(TextAsset)Resources.Load("Level Code/level_" + levelNumber);
        inGameUI.LoadCode(txt);
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
                        Instantiate(character, boardManager.GetCenterPointOfTile(i,j), transform.rotation * Quaternion.Euler (0f, -90f, 0f), transform);
                        break;
                    }
                    case 'd': { // character cube (looking down) (x)
                        Instantiate(character, boardManager.GetCenterPointOfTile(i,j), transform.rotation * Quaternion.Euler (0f, 90f, 0f), transform);
                        break;
                    }
                    case 'l': { // character cube (looking left) (-z)
                        Instantiate(character, boardManager.GetCenterPointOfTile(i,j), transform.rotation * Quaternion.Euler (0f, 180f, 0f), transform);
                        break;
                    }
                    case 'r': { // character cube (looking right) (z)
                        Instantiate(character, boardManager.GetCenterPointOfTile(i,j), transform.rotation * Quaternion.Euler (0f, 0f, 0f), transform);
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
        
        TextAsset csv = (TextAsset)Resources.Load("Level Tables/table_" + levelNumber);
        String[] lines  = csv.text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        var string_table = lines.Select(l => l.Split(',').ToArray()).ToArray();

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