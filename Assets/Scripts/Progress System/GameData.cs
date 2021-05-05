using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serializable class to save game progress
/// </summary>
[System.Serializable] public class GameData {

    public int lastLevelCompleted;
    public List<LevelData> levelScoreTable;
    public float timePlayed;

    /// <summary>
    /// Initializes a newly created GameData object 
    /// </summary>
    /// <param name="lastLevelCompleted"> number of the last level that was completed</param>
    /// <param name="scoreTable"> LevelData List with the scores for all levels played</param>
    /// <param name="timePlayed"> time played, in seconds</param>
    public GameData(int lastLevelCompleted, List<LevelData> scoreTable, float timePlayed) {
         this.lastLevelCompleted = lastLevelCompleted;
         this.levelScoreTable = scoreTable;
         this.timePlayed = timePlayed;
    }

    /// <summary>
    /// Updates an entry in the LevelData list
    /// </summary>
    /// <param name="newLD"> LevelData to update</param>
    public void UpdateLevel(LevelData newLD) {
        try { 
            LevelData oldLD = levelScoreTable[newLD.levelNumber];

            if (!oldLD.passed) {
                newLD.attempts += oldLD.attempts;
            }
            else if (newLD.points < oldLD.points) {
                newLD.points = oldLD.points;
            }
        } catch { Debug.Log("no old level data"); }
        levelScoreTable.Insert(newLD.levelNumber, newLD);
    }

    
}