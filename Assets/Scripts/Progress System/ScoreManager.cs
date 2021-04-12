using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private LevelDificulty dificulty;
    private static LevelData levelData;
    
    void Start() {
        GameEvents.current.onLevelLoad += LoadLevel;
        GameEvents.current.onNextLevelLoad += LoadNextLevel;
        GameEvents.current.onPlayLevel += AddAttempt;
        GameEvents.current.onLevelPassed += LevelPassed;
    }

    /* =============================================================== GET METHODS =============================================================== */

    public LevelData GetCurrentLevelData() {
        return levelData;
    }

    public int GetCurrentScore() {
        return (int) GetDificulty(levelData.levelNumber) - (100 * (levelData.attempts -1));
    }

    public int GetCurrentAttempts() {
        return levelData.attempts;
    }

    public int GetCurrentLevelNumber() {
        return levelData.levelNumber;
    }

    /* ============================================================ EVENT CALL METHODS ============================================================ */

    private void LoadLevel(int levelNumber) {
        //TODO: search if there's a saved game data for the level number
       // if (levelData == null || levelData.levelNumber != levelNumber) {
            levelData = new LevelData(levelNumber);
        // }
    }

    private void LoadNextLevel() {
        LoadLevel(levelData.levelNumber + 1);
    }

    private void AddAttempt() {
        if (!levelData.passed) {
            levelData.attempts++;
        }
    }

    private void LevelPassed() {
        levelData.passed = true;
        levelData.points = (int) GetDificulty(levelData.levelNumber) - (100 * (levelData.attempts -1));
        // DEBUG_PrintAttempts();
        // DEBUG_PrintPoints();
    }

    /* ============================================================= AUXILIAR METHODS ============================================================= */

    private LevelDificulty GetDificulty(int levelNumber) {
        GameObject characterCube = GameObject.FindGameObjectWithTag("CharacterCube");
        AbsLevel level = (AbsLevel)characterCube.GetComponent("Level"+levelNumber);
        return level.dificulty;
    }

    /* ============================================================= DEBUG FUNCTIONS ============================================================= */

     private void DEBUG_PrintAttempts() {
         Debug.Log("attempts: " + levelData.attempts);
     }

     private void DEBUG_PrintPoints() {
         Debug.Log("points: " + levelData.points + "/ " + (int) GetDificulty(levelData.levelNumber));
     }

}
