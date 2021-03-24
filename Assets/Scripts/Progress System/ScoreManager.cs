using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private LevelDificulty dificulty;
    private LevelData levelData;
    
    void Start() {
        GameEvents.current.onLevelLoad += LoadLevel;
        GameEvents.current.onPlayLevel += AddAttempt;
        GameEvents.current.onLevelPassed += LevelPassed;
    }

    /* =============================================================== GET METHODS =============================================================== */

    public LevelData getCurrentLevelData() {
        return levelData;
    }

    /* ============================================================ EVENT CALL METHODS ============================================================ */

    private void LoadLevel(int levelNumber) {
        if (levelData == null || levelData.levelNumber != levelNumber) {
            levelData = new LevelData(levelNumber);
        }
    }

    private void AddAttempt() {
        if (!levelData.passed) {
            levelData.numberOfAttempts++;
        }
    }

    private void LevelPassed() {
        levelData.passed = true;
        levelData.points = (int) GetDificulty(levelData.levelNumber) - (100 * (levelData.numberOfAttempts -1));
        // DEBUG_PrintAttempts();
        DEBUG_PrintPoints();
    }

    /* ============================================================= AUXILIAR METHODS ============================================================= */

    private LevelDificulty GetDificulty(int levelNumber) {
        GameObject characterCube = GameObject.FindGameObjectWithTag("CharacterCube");
        AbsLevel level = (AbsLevel)characterCube.GetComponent("Level"+levelNumber);
        return level.dificulty;
    }

    /* ============================================================= DEBUG FUNCTIONS ============================================================= */

     private void DEBUG_PrintAttempts() {
         Debug.Log("attempts: " + levelData.numberOfAttempts);
     }

     private void DEBUG_PrintPoints() {
         Debug.Log("points: " + levelData.points + "/ " + (int) GetDificulty(levelData.levelNumber));
     }

}
