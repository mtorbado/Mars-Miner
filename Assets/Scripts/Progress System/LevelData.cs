public class LevelData {

    public int levelNumber;
    public bool passed;
    public int attempts;
    public int points;
    
    public LevelData(int levelNumber) {
        this.levelNumber = levelNumber;
        this.passed = false;
        this.attempts = 0;
        this.points = 0;
    }
}