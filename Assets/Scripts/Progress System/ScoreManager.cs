using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ScoreManager : MonoBehaviour {

    private LevelData currentLevelData;
    private GameData gameData;
    
    void Start() {
        GameEvents.current.onLevelLoad += LoadLevel;
        GameEvents.current.onNextLevelLoad += LoadNextLevel;
        GameEvents.current.onPlayLevel += AddAttempt;
        GameEvents.current.onLevelPassed += LevelPassed;
        GameEvents.current.onLevelFailed += LevelFailed;
        GameEvents.current.onLoadGameData += ReadGameData;

        gameData = new GameData();
        GameEvents.current.UpdateScores();
        RetrieveGameData();
    }

    /* =============================================================== GET METHODS =============================================================== */

    public int GetCurrentPoints() {
        return Math.Max((Int32)GetDificulty(currentLevelData.levelNumber) - (100 * (currentLevelData.attempts -1)), 100);
    }

    public int GetCurrentAttempts() {
        return currentLevelData.attempts;
    }

    public int GetCurrentLevelNumber() {
        return currentLevelData.levelNumber;
    }

    public int GetLevelScore(int levelNumber) {
        return gameData.GetLevel(levelNumber).points;
    } 

    /* ============================================================ EVENT CALLED METHODS ============================================================ */

    private void LoadLevel(int levelNumber) {
        currentLevelData = gameData.GetLevel(levelNumber);
    }

    private void LoadNextLevel() {
        LoadLevel(currentLevelData.levelNumber + 1);
    }

    private void AddAttempt() {
        if (!currentLevelData.passed) {
            currentLevelData.attempts++;
        }
    }

    private void LevelPassed() {
        currentLevelData.passed = true;
        currentLevelData.points = GetCurrentPoints();
        // DEBUG_PrintAttempts();
        // DEBUG_PrintPoints();
        gameData.UpdateLevel(currentLevelData);
        SaveGameData();
    }

    private void LevelFailed() {
        gameData.UpdateLevel(currentLevelData);
        SaveGameData();
    }

    /* =============================================================== LOAD GAME DATA ============================================================= */

    private void RetrieveGameData() {
        #pragma warning disable CS0618
        Application.ExternalCall ("getCampoLibre");
        #pragma warning restore CS0618
    }

    private void ReadGameData(string gameDataStr) {
        try {
            gameData = JsonUtility.FromJson<GameData>(gameDataStr);
            GameEvents.current.UpdateScores();
        } catch (Exception e) {
            DEBUG_ReadGameDataError(e);
        }
    }

    /* =============================================================== SAVE GAME DATA ============================================================= */

    private void SaveGameData() {
        string jsonGD = JsonUtility.ToJson(gameData, true);
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
        Debug.Log("attempts: " + currentLevelData.attempts);
    }

    private void DEBUG_PrintPoints() {
        Debug.Log("points: " + currentLevelData.points + "/ " + (int) GetDificulty(currentLevelData.levelNumber));
    }

    private void DEBUG_ReadGameDataError(Exception e) {
        Debug.Log("Could not read GameData string: " + e.ToString());
    }
}
