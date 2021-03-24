using System.Collections.Generic;

/// <summary>
/// Serializable class to save game progress
/// </summary>
[System.Serializable] public class GameData {

    public int lastLeveCompleted;
    public List<LevelData> levelScoreTable;
    public float timePlayed;
    
    public GameData() {}

    public GameData(int lastLeveCompleted, List<LevelData> scoreTable, float timePlayed) {
         this.lastLeveCompleted = lastLeveCompleted;
         this.levelScoreTable = scoreTable;
         this.timePlayed = timePlayed;
    }

    public void UpdateLevel(LevelData levelData) {
        levelScoreTable.Insert(levelData.levelNumber, levelData);
    }
}