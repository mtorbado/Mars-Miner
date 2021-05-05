using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ScoreManager : MonoBehaviour {

    private LevelDificulty dificulty;
    private static GameData gameData;
    private static LevelData currentLD;
    
    void Start() {
        GameEvents.current.onLevelLoad += LoadLevel;
        GameEvents.current.onNextLevelLoad += LoadNextLevel;
        GameEvents.current.onPlayLevel += AddAttempt;
        GameEvents.current.onLevelPassed += LevelPassed;
        GameEvents.current.onLevelFailed += LevelFailed;
        GameEvents.current.onLoadGameData += ReadGameData;
        RetrieveGameData();
    }

    /* =============================================================== GET METHODS =============================================================== */

    public LevelData GetCurrentLevelData() {
        return currentLD;
    }

    public int GetCurrentScore() {
        return (int) GetDificulty(currentLD.levelNumber) - (100 * (currentLD.attempts -1));
    }

    public int GetCurrentAttempts() {
        return currentLD.attempts;
    }

    public int GetCurrentLevelNumber() {
        return currentLD.levelNumber;
    }

    public int GetLevelScore(int levelNumber) {
        return gameData.levelScoreTable[levelNumber].points;
    } 

    /* ============================================================ EVENT CALL METHODS ============================================================ */

    private void LoadLevel(int levelNumber) {
        currentLD = new LevelData(levelNumber);
    }

    private void LoadNextLevel() {
        LoadLevel(currentLD.levelNumber + 1);
    }

    private void AddAttempt() {
        if (!currentLD.passed) {
            currentLD.attempts++;
        }
    }

    private void LevelPassed() {
        currentLD.passed = true;
        currentLD.points = (int) GetDificulty(currentLD.levelNumber) - (100 * (currentLD.attempts -1));
        // DEBUG_PrintAttempts();
        // DEBUG_PrintPoints();
        gameData.UpdateLevel(currentLD);
        SaveGameData();
    }

    private void LevelFailed() {
        gameData.UpdateLevel(currentLD);
    }

    /* =============================================================== LOAD GAME DATA ============================================================= */

    private void RetrieveGameData() {
        List<LevelData> LDList = new List<LevelData>(LevelLoader.GetNumOfLevels("LevelFiles"));
        gameData = new GameData(0, LDList, 0);

        #pragma warning disable CS0618
        Application.ExternalCall ("getCampoLibre");
        #pragma warning restore CS0618
    }

    private void ReadGameData(string gameDataStr) {
        try {
            gameData = JsonUtility.FromJson<GameData>(gameDataStr);
        } catch (Exception e) {
            DEBUG_ReadGameDataError(e);
        }
    }

    /* =============================================================== SAVE GAME DATA ============================================================= */

    private void SaveGameData() {
        string jsonGD = JsonUtility.ToJson(gameData);
        #pragma warning disable CS0618
        Application.ExternalCall ("setCampoLibre", jsonGD );
        #pragma warning restore CS0618
    }

    private void SaveGlobalData() { // not called yet
        int completed = 0;
        int totalScore = 0;
        if (totalScore >= 4000) completed = 1;
        int [] data = { totalScore, completed };
        #pragma warning disable CS0618
        Application . ExternalCall ("guardar", data );
        #pragma warning restore CS0618
    }

    /* ============================================================= AUXILIAR METHODS ============================================================= */

    private LevelDificulty GetDificulty(int levelNumber) {
        GameObject characterCube = GameObject.FindGameObjectWithTag("CharacterCube");
        AbsLevel level = (AbsLevel)characterCube.GetComponent("Level"+levelNumber);
        return level.dificulty;
    }

    /* ============================================================= DEBUG FUNCTIONS ============================================================= */

    private void DEBUG_PrintAttempts() {
        Debug.Log("attempts: " + currentLD.attempts);
    }

    private void DEBUG_PrintPoints() {
        Debug.Log("points: " + currentLD.points + "/ " + (int) GetDificulty(currentLD.levelNumber));
    }

    private void DEBUG_ReadGameDataError(Exception e) {
        Debug.Log("Could not read GameData string: " + e.ToString());
    }
}
