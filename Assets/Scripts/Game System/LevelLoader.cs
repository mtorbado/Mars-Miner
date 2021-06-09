using System;
using System.Linq;
using UnityEngine;

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

    [HideInInspector] public int? playingLevel;
    [HideInInspector] public LevelDificulty playingDificulty;

    [HideInInspector] public int easyLevels;
    [HideInInspector] public int mediumLevels;
    [HideInInspector] public int hardLevels;
    [HideInInspector] public int challengeLevels;

    private void Start() {
        GameEvents.current.onSelectLevel += CleanTable;

        easyLevels = Resources.LoadAll("Level Scripts/" + LevelDificulty.Easy.ToString()).Length;
        mediumLevels = Resources.LoadAll("Level Scripts/" + LevelDificulty.Medium.ToString()).Length;
        hardLevels = Resources.LoadAll("Level Scripts/" + LevelDificulty.Hard.ToString()).Length;
        challengeLevels = Resources.LoadAll("Level Scripts/" + LevelDificulty.Challenge.ToString()).Length;
    }

    private void Awake() {
        boardManager = (BoardManager)GameObject.Find("Board").GetComponent(typeof(BoardManager));
        scoreManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<ScoreManager>(); 
        inGameUI = GameObject.FindGameObjectWithTag("InGameUI").GetComponent<InGameUICanvasActions>();
    }


    /* =============================================================== PUBLIC METHODS =============================================================== */

    public void LoadLevel(LevelDificulty levelDificulty,int levelNumber) {
        SetTableElements(ReadLevelTable(levelDificulty, levelNumber));
        SetLevelScript(levelDificulty, levelNumber);
        SetLevelTextCode(levelDificulty, levelNumber);

        playingLevel = levelNumber;
        playingDificulty = levelDificulty;
        GameEvents.current.LoadLevel();
    }

    public void LoadRandomLevel(LevelDificulty levelDificulty) {
        int levelNumber =0;
        System.Random r = new System.Random();
        switch(levelDificulty) {
            case LevelDificulty.Easy:
                levelNumber = r.Next(easyLevels - 1);
                break;
            case LevelDificulty.Medium:
                levelNumber = r.Next(mediumLevels - 1);
                break;
            case LevelDificulty.Hard:
                levelNumber = r.Next(hardLevels - 1);
                break;
            case LevelDificulty.Challenge:
                levelNumber = r.Next(challengeLevels - 1);
                break;
        }
        if (levelNumber >= playingLevel) levelNumber++;
        LoadLevel(levelDificulty, levelNumber);
    }

    public void LoadRandomLevel() {
        LoadRandomLevel(playingDificulty);
    }

    public void RestartLevel() {
        LoadLevel(playingDificulty, (int)playingLevel);
    }

    public void LoadNextLevel() {
        LoadLevel(playingDificulty, (int)playingLevel + 1);
    }

    public void LoadNextDificulty() {
        LevelDificulty[] ldArray = (LevelDificulty[])Enum.GetValues(typeof(LevelDificulty));
        int i = Array.IndexOf<LevelDificulty>(ldArray, playingDificulty) + 1;
        LevelDificulty nextDificulty = (ldArray.Length == i) ? ldArray[0] : ldArray[i];
        LoadRandomLevel(nextDificulty);
    }

    public bool IsLastLevel() {
        switch(playingDificulty) {
            case LevelDificulty.Easy: return (easyLevels-1 == playingLevel);
            case LevelDificulty.Medium: return (mediumLevels-1 == playingLevel);
            case LevelDificulty.Hard: return (hardLevels-1 == playingLevel);
            case LevelDificulty.Challenge: return (challengeLevels-1 == playingLevel);
        }
        return false;
    }

    public bool IsNextDificultyUnlocked() {
        Debug.Log("puntos: " + scoreManager.finalScore.GetPoints(playingDificulty));
        if (scoreManager.finalScore.GetPoints(playingDificulty) >= ScoreManager.PASS_DIFICULTY_SCORE) {
            return true;
        }
        else return false;
    }

    /* =============================================================== PRIVATE METHODS =============================================================== */

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

    private char[,] ReadLevelTable(LevelDificulty levelDificulty, int levelNumber) {

        char[,] table = new char[TableSize,TableSize];
        TextAsset csv = (TextAsset)Resources.Load("Level Tables/" + levelDificulty.ToString() + levelNumber);
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

    private void CleanTable() {
        foreach (Transform child in transform) {
            if (child.gameObject.layer == GameElementsLayer) {
                Destroy(child.gameObject);
            }
        }
    }

    private void SetLevelScript(LevelDificulty levelDificulty, int levelNumber) {
        GameObject[] characterCubes = GameObject.FindGameObjectsWithTag("CharacterCube");
        foreach(GameObject cc in characterCubes) {
            Destroy(cc.GetComponent<AbsLevel>());
            cc.AddComponent(Type.GetType(levelDificulty.ToString() + levelNumber));
            cc.GetComponent<AbsLevel>().Initialize();
        }
         inGameUI.LoadOreGoal(characterCubes.Last<GameObject>().GetComponent<AbsLevel>().oreGoal);
    }

    private void SetLevelTextCode(LevelDificulty levelDificulty, int levelNumber) {
        TextAsset txt = (TextAsset)Resources.Load("Level Text/" + levelDificulty.ToString() + levelNumber);
        inGameUI.LoadCode(txt);
    }
}