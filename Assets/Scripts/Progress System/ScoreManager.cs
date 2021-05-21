using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ScoreManager : MonoBehaviour {

    
    public Score finalScore;
    public Score newScore;
    public int scoreToPass = 4000;

    private int currentLevel;
    private int attempts;
    
    void Start() {
        GameEvents.current.onLoadGameData += LoadScore;
        GameEvents.current.onExitGame += SaveGlobalData;
        GameEvents.current.onLevelLoad += LoadLevel;
        GameEvents.current.onNextLevelLoad += LoadNextLevel;
        GameEvents.current.onPlayLevel += AddAttempt;
        GameEvents.current.onLevelPassed += SaveScore;

        newScore = new Score();
        finalScore = new Score();
        GameEvents.current.UpdateScores();
        RetrieveScore();
    }

    
     public LevelDificulty GetDificulty(int levelNumber) {
        if (currentLevel < (Int32)LevelInfo.FirstMedium) {
            return LevelDificulty.Easy;
        }
        else if (currentLevel < (Int32)LevelInfo.FirstHard) {
            return LevelDificulty.Medium;
        }
        else if (currentLevel < (Int32)LevelInfo.FirstChallenge) {
            return LevelDificulty.Hard;
        }
        else {
            return LevelDificulty.Challenge;
        }
    }

     public int GetDificultyIndex(int levelNumber) {
        if (currentLevel < (Int32)LevelInfo.FirstMedium) {
            return 0;
        }
        else if (currentLevel < (Int32)LevelInfo.FirstHard) {
            return 1;
        }
        else if (currentLevel < (Int32)LevelInfo.FirstChallenge) {
            return 2;
        }
        else {
            return 3;
        }
    }

    /// <summary>
    /// Returns the points obtained in the current playing level
    /// </summary>
    /// <returns></returns>
    public int GetLevelPoints() {
        return (Int32)GetDificulty(currentLevel); //TODO calculate taking account of time and attempts

    }

    public int GetLevelAttempts() {
        return attempts;
    }

    private void LoadLevel(int levelNumber) {
        if (currentLevel != levelNumber) {
            attempts = 0;
        }
        currentLevel = levelNumber;
    }

    private void LoadNextLevel() {
        LoadLevel(currentLevel + 1);
    }

    private void AddAttempt() {
        attempts ++;
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
        newScore.scoreArray[GetDificultyIndex(currentLevel)] += GetLevelPoints();
        for (int i = 0; i < finalScore.scoreArray.Length; i++) {
            finalScore.scoreArray[i] = Math.Max(finalScore.scoreArray[i], newScore.scoreArray[i]);
        }
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
