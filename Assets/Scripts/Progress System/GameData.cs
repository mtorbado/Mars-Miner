using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serializable class to save game progress
/// </summary>
[System.Serializable] public class GameData {

    public int lastLevelCompleted;
    public LevelData[] levelArray;
    public float timePlayed;

    /// <summary>
    /// Initializes a newly created GameData object 
    /// </summary>
    public GameData() {
        this.lastLevelCompleted = -1;
        this.levelArray = LeverArrayInitializer();
        this.timePlayed = 0;
    }

    /// <summary>
    /// Updates an entry in the LevelData list
    /// </summary>
    /// <param name="newLD"> LevelData to update</param>
    public void UpdateLevel(LevelData newLD) {
        levelArray[newLD.levelNumber] = newLD;
        if (newLD.levelNumber > lastLevelCompleted && newLD.passed) {
            lastLevelCompleted = newLD.levelNumber;
        } 
    }

    public LevelData GetLevel(int levelNumber) {
        return levelArray[levelNumber];
    }  

    public float GetTime() {
        return timePlayed;
    }

    public int GetLastLevelCompleted() {
        return lastLevelCompleted;
    }

    private LevelData[] LeverArrayInitializer() {
        LevelData[] levelArray = new LevelData[LevelLoader.GetNumOfLevels()];
        for (int i = 0; i < levelArray.Length; ++i) {
            levelArray[i] = new LevelData(i);
        }
        return levelArray;
    }
}