using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ScoreManager : MonoBehaviour {

    
    public Score finalScore;
    public int scoreToPass;
    public int[] maxScores;

    int attempts;
    int moves;

    LevelLoader levelLoader;
    
    void Start() {
        GameEvents.current.onLoadGameData += LoadScore;
        GameEvents.current.onExitGame += SaveGlobalData;
        GameEvents.current.onLevelLoad += Reset;
        GameEvents.current.onNextLevelLoad += Reset;
        GameEvents.current.onRandomLevelLoad += Reset;
        GameEvents.current.onPlayLevel += AddAttempt;
        GameEvents.current.onLevelPassed += SaveScore;

        levelLoader = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelLoader>();

        finalScore = new Score();
        GameEvents.current.UpdateScores();
        RetrieveScore();
    }

    /// <summary>
    /// Returns the points obtained in the current playing level
    /// </summary>
    /// <returns></returns>
    public int LevelPoints() { // TODO calculate score
        switch(levelLoader.playingDificulty) {
            case LevelDificulty.Easy: return (int)LevelDificulty.Easy;
            case LevelDificulty.Medium: return (int)LevelDificulty.Medium;
            case LevelDificulty.Hard: return (int)LevelDificulty.Hard;
            case LevelDificulty.Challenge: return (int)LevelDificulty.Challenge;
        }
        return 0;
    }

    public int MaxPoints() {
        switch(levelLoader.playingDificulty) {
            case LevelDificulty.Easy: return (int)LevelDificulty.Easy * levelLoader.easyLevels;
            case LevelDificulty.Medium: return (int)LevelDificulty.Medium * levelLoader.mediumLevels;
            case LevelDificulty.Hard: return (int)LevelDificulty.Hard * levelLoader.hardLevels;
            case LevelDificulty.Challenge: return (int)LevelDificulty.Challenge * levelLoader.challengeLevels;
        }
        return 0;
    }

    public int[] MaxPointsArray() {
        return new int[4] {
            (int)LevelDificulty.Easy * levelLoader.easyLevels,
            (int)LevelDificulty.Medium * levelLoader.mediumLevels,
            (int)LevelDificulty.Hard * levelLoader.hardLevels,
            (int)LevelDificulty.Challenge * levelLoader.challengeLevels
        };
    }


    public int LevelAttempts() {
        return attempts;
    }

    private void Reset(LevelDificulty levelDificulty, int levelNumber) {
        if (levelLoader.playingDificulty != levelDificulty || levelLoader.playingLevel != levelNumber) {
            attempts = 0;
            moves = 0;
        }        
    }

    private void Reset() {
        attempts = 0;
        moves = 0;
    }

    private void AddAttempt() {
        attempts ++;
    }

    private void AddMove() {
        moves ++;
    }
   
    /* =============================================================== LOAD SCORE ============================================================= */

    private void RetrieveScore() {
        #pragma warning disable CS0618
        Application.ExternalCall ("getCampoLibre");
        #pragma warning restore CS0618
    }

    private void LoadScore(string scoreStr) {
        try {
            if (!scoreStr.Equals("") && scoreStr != null) {
                finalScore = JsonUtility.FromJson<Score>(scoreStr);
                GameEvents.current.UpdateScores();
            }
        } catch (Exception e) {
            Debug.Log("Could not read GameData string: " + e.ToString());
        }
    }

    /* =============================================================== SAVE SCORE ============================================================= */

    private void SaveScore() {
        
        int points = Math.Max(LevelPoints() + finalScore.GetPoints(levelLoader.playingDificulty), MaxPoints());
        finalScore.SetPoints(levelLoader.playingDificulty, Math.Max(finalScore.GetPoints(levelLoader.playingDificulty), points));

        string jsonScore = JsonUtility.ToJson(finalScore);
        GameEvents.current.UpdateScores();
        
        #pragma warning disable CS0618
        Application.ExternalCall("setCampoLibre", jsonScore);
        #pragma warning restore CS0618
    }

    private void SaveGlobalData() {
        int completed = 0;
        if (finalScore.TotalScore() >= scoreToPass) completed = 1;
        int [] data = {finalScore.TotalScore(), completed};
        #pragma warning disable CS0618
        Application.ExternalCall("guardar", data);
        #pragma warning restore CS0618
    }
}
