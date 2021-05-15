/// <summary>
/// Serializable class to save game progress
/// </summary>
[System.Serializable] public class GameData {

    public int lastLevelCompleted;
    public LevelData[] levelArray;

    /// <summary>
    /// Initializes a newly created GameData object 
    /// </summary>
    public GameData() {
        this.lastLevelCompleted = -1;
        this.levelArray = LeverArrayInitializer();
    }

    public void UpdateLevel(LevelData newLD) {
        levelArray[newLD.levelNumber] = newLD;
        if (newLD.levelNumber > lastLevelCompleted && newLD.passed) {
            lastLevelCompleted = newLD.levelNumber;
        } 
    }

    public LevelData GetLevel(int levelNumber) {
        return levelArray[levelNumber];
    }  

    public int LastLevelCompleted() {
        return lastLevelCompleted;
    }

    public int TotalScore() {
        int totalScore = 0;
        for (int i = 0; i < levelArray.Length; ++i) {
            totalScore += levelArray[i].points;
        }
        return totalScore;
    }

    private LevelData[] LeverArrayInitializer() {
        LevelData[] levelArray = new LevelData[LevelLoader.GetNumOfLevels()];
        for (int i = 0; i < levelArray.Length; ++i) {
            levelArray[i] = new LevelData(i);
        }
        return levelArray;
    }
}